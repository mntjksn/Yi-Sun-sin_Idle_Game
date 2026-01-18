using other;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    CircleFire = 0,
    Shotttt,
    Shot2ttt
}

public class BossWeapon : MonoBehaviour
{
    public float TurnSpeed;

    // 총알이 향할 대상
    public Transform Target;

    // 발사될 총알 오브젝트
    public GameObject Bullet;
    public GameObject Bullet2;

    public GameObject BossBulletPrefab;

    private BossHP bossHP;

    // 특정 조건에서 자동 생성 간격
    public float SpawnInterval = 0.5f;
    private float _spawnTimer;

    private Animator animator;

    // 회전 초기값
    [Range(0, 360)]
    public float Rotation;

    // 퍼짐 모양 꼭짓점 수
    [Range(3, 7)]
    public int Vertex = 3;

    // 퍼짐 형태 보정 값
    [Range(1, 5)]
    public float Subdivision = 3;

    // 탄 속도 배수
    public float Speed = 3f;

    // 퍼짐 계산용 내부 데이터
    private int _m;
    private float _a;
    private float _phi;
    private readonly List<float> _v = new List<float>();
    private readonly List<float> _xx = new List<float>();

    // 공격 사운드
    public AudioSource shootbgm1;
    public AudioSource shootbgm2;
    public AudioSource shootbgm3;
    public AudioSource shootbgm4;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossHP = GetComponent<BossHP>();

        ShapeInit();
    }

    private void Update()
    {
        // 효과음 설정 적용
        int effectSound = PlayerPrefs.GetInt("EFFECT");

        if (effectSound == 0)
        {
            shootbgm1.mute = false;
            shootbgm2.mute = false;
            shootbgm3.mute = false;
            shootbgm4.mute = false;
        }

        if (effectSound == 1)
        {
            shootbgm1.mute = true;
            shootbgm2.mute = true;
            shootbgm3.mute = true;
            shootbgm4.mute = true;
        }

        // 체력이 일정 이하일 때 자동 회전 발사 패턴 실행
        if (bossHP.CurrentHP <= bossHP.MaxHP * 0.6f)
        {
            // 기본 회전 처리
            transform.Rotate(Vector3.forward * (TurnSpeed * 100f * Time.deltaTime));

            // 생성 간격 처리
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer < SpawnInterval) return;

            _spawnTimer = 0f;

            // 총알 생성
            GameObject temp = Instantiate(Bullet);

            if (temp != null)
            {
                shootbgm3.Play();
            }

            // 위치와 회전 적용
            temp.transform.position = transform.position;
            temp.transform.rotation = transform.rotation;

            // 일정 시간 후 제거
            Destroy(temp, 1.25f);
        }
    }

    public void StartFiring(AttackType attackType)
    {
        // 공격 타입 이름과 같은 코루틴을 시작
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        // 공격 타입 이름과 같은 코루틴을 중지
        StopCoroutine(attackType.ToString());

        // InvokeRepeating 기반 공격은 별도로 중지 처리
        if (attackType == AttackType.Shotttt)
        {
            CancelInvoke("Shot");
        }

        if (attackType == AttackType.Shot2ttt)
        {
            CancelInvoke("Shot2");
        }
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 0.75f;
        int count = 30;
        float intervalAngle = 360f / count;
        float weightAngle = 0f;

        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(BossBulletPrefab, transform.position, Quaternion.identity);

                float angle = weightAngle + intervalAngle * i;

                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }

            weightAngle += 36f;

            // 체력에 따라 공격 간격 조정
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.8f) attackRate = 1.5f;
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.6f) attackRate = 1.75f;
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.4f) attackRate = 2f;

            shootbgm1.Play();
            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator Shotttt()
    {
        // 일정 주기로 Shot 실행
        InvokeRepeating("Shot", 1f, 4f);
        yield return null;
    }

    private void Shot()
    {
        shootbgm4.Play();

        // 생성한 총알들을 저장
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {
            // 총알 생성
            GameObject temp = Instantiate(Bullet);

            // 위치 적용
            temp.transform.position = transform.position;

            // 회전 적용
            temp.transform.rotation = Quaternion.Euler(0f, 0f, i);

            // 일정 시간 후 제거
            Destroy(temp, 1.25f);

            // 타겟을 바라보도록 처리할 목록에 추가
            bullets.Add(temp.transform);
        }

        // 일정 시간 후 타겟 방향으로 회전 처리
        StartCoroutine(BulletToTarget(bullets));
    }

    private IEnumerator BulletToTarget(IList<Transform> objects)
    {
        // 잠깐 대기 후 처리 시작
        yield return new WaitForSeconds(0.25f);

        while (true)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                // 현재 위치에서 타겟 방향 벡터 계산
                Vector3 targetDirection = Target.transform.position - objects[i].position;

                // 방향을 각도로 변환
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

                // 타겟 방향을 바라보도록 회전
                objects[i].rotation = Quaternion.Euler(0f, 0f, angle);
            }

            // 목록 비우기
            objects.Clear();

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Shot2ttt()
    {
        // 일정 주기로 Shot2 실행
        InvokeRepeating("Shot2", 1f, 2.5f);
        yield return null;
    }

    private void ShapeInit()
    {
        // 내부 리스트 초기화
        _v.Clear();
        _xx.Clear();

        // 내부 데이터 초기화
        _m = (int)Mathf.Floor(Subdivision / 2f);
        _a = 2f * Mathf.Sin(Mathf.PI / Vertex);
        _phi = ((Mathf.PI / 2f) * (Vertex - 2f)) / Vertex;

        _v.Add(0f);
        _xx.Add(0f);

        for (int i = 1; i <= _m; i++)
        {
            _v.Add(Mathf.Sqrt(Subdivision * Subdivision - 2f * _a * Mathf.Cos(_phi) * i * Subdivision + _a * _a * i * i));
        }

        for (int i = 1; i <= _m; i++)
        {
            _xx.Add(Mathf.Rad2Deg * (Mathf.Asin(_a * Mathf.Sin(_phi) * i / _v[i])));
        }
    }

    private void Shot2()
    {
        // 퍼짐 패턴 랜덤 설정
        Rotation = Random.Range(0f, 360f);
        Vertex = Random.Range(3, 7);
        Subdivision = Random.Range(1f, 5f);
        Speed = Random.Range(10f, 20f);

        shootbgm2.Play();

        // 회전에 영향을 주지 않도록 별도 방향 값 사용
        float direction = Rotation;

        // 꼭짓점 수 만큼 반복
        for (int r = 0; r < Vertex; r++)
        {
            for (int i = 1; i <= _m; i++)
            {
                // 첫번째 생성
                GameObject idx1 = Instantiate(Bullet2);
                idx1.transform.position = transform.position;
                idx1.transform.rotation = Quaternion.Euler(0f, 0f, direction + _xx[i]);
                idx1.GetComponent<Bullet>().Speed = _v[i] * Speed / Subdivision;
                Destroy(idx1, 2f);

                // 두번째 생성
                GameObject idx2 = Instantiate(Bullet2);
                idx2.transform.position = transform.position;
                idx2.transform.rotation = Quaternion.Euler(0f, 0f, direction - _xx[i]);
                idx2.GetComponent<Bullet>().Speed = _v[i] * Speed / Subdivision;
                Destroy(idx2, 2f);

                // 세번째 생성
                GameObject idx3 = Instantiate(Bullet2);
                idx3.transform.position = transform.position;
                idx3.transform.rotation = Quaternion.Euler(0f, 0f, direction);
                idx3.GetComponent<Bullet>().Speed = Speed;
                Destroy(idx3, 2f);

                // 다음 방향으로 진행
                direction += 360f / Vertex;
            }
        }
    }
}
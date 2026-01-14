using other;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public enum AttackType { CircleFire = 0, Shotttt, Shot2ttt }

public class BossWeapon : MonoBehaviour
{
    public float TurnSpeed;

    //총알을 생성후 Target에게 날아갈 변수
    public Transform Target;

    //발사될 총알 오브젝트
    public GameObject Bullet;
    public GameObject Bullet2;

    public GameObject BossBulletPrefab;

    private BossHP bossHP;

    public float SpawnInterval = 0.5f;
    private float _spawnTimer;

    Animator animator;


    //초기 중심 : 회전 되는 방향
    [Range(0, 360), Tooltip("퍼지기 전 회전을 줄 수 있음")]
    public float Rotation;

    [Range(3, 7), Tooltip("퍼지는 모양이 몇각형으로 퍼질지 정하는 것")] //->삼~칠각형이 그나마 이쁨 그 이상으로 가면 원으로 보임..
    public int Vertex = 3;

    [Range(1, 5), Tooltip("이 값을 조정하여 둥근 느낌, 납작한 느낌으로 표현 됨")]
    public float Subdivision = 3;

    //스피드
    public float Speed = 3; //speed

    //기타 데이터들
    private int _m;
    private float _a;
    private float _phi;
    private readonly List<float> _v = new List<float>();
    private readonly List<float> _xx = new List<float>();

    public AudioSource shootbgm1, shootbgm2, shootbgm3, shootbgm4;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossHP = GetComponent<BossHP>();

        ShapeInit();
    }

    private void Update()
    {
        int effect_sound = PlayerPrefs.GetInt("EFFECT");

        if (effect_sound == 0)
        {
            shootbgm1.mute = false;
            shootbgm2.mute = false;
            shootbgm3.mute = false;
            shootbgm4.mute = false;
        }

        if (effect_sound == 1)
        {
            shootbgm1.mute = true;
            shootbgm2.mute = true;
            shootbgm3.mute = true;
            shootbgm4.mute = true;
        }

        if (bossHP.CurrentHP <= bossHP.MaxHP * 0.6f)
        {
            //기본 회전
            transform.Rotate(Vector3.forward * (TurnSpeed * 100 * Time.deltaTime));

            //생성 간격 처리
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer < SpawnInterval)
                return;
            

            //초기화
            _spawnTimer = 0f;

            //총알 생성
            GameObject temp = Instantiate(Bullet);

            if (temp != null) { shootbgm3.Play(); }

            //2초후 자동 삭제
            Destroy(temp, 1.25f);

            //총알 생성 위치를 머즐 입구로 한다.
            temp.transform.position = transform.position;

            //총알의 방향을 오브젝트의 방향으로 한다.
            //->해당 오브젝트가 오브젝트가 360도 회전하고 있으므로, Rotation이 방향이 됨.
            temp.transform.rotation = transform.rotation;
        }
    }

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 0.75f;
        int count = 30;
        float intervalAngle = 360 / count;
        float weightAngle = 0;

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

            weightAngle += 36;

            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.8f)
                attackRate = 1.5f;

            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.6f)
                attackRate = 1.75f;

            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.4f)
                attackRate = 2f;

            shootbgm1.Play();
            yield return new WaitForSeconds(attackRate);
        }
    }

    private void Shotttt()
    {
        InvokeRepeating("Shot",1f, 4f);
    }

    private void Shot()
    {
        shootbgm4.Play();

        //Target방향으로 발사될 오브젝트 수록
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);

            //2초후 삭제
            Destroy(temp, 1.25f);

            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = transform.position;

            //?초후에 Target에게 날아갈 오브젝트 수록
            bullets.Add(temp.transform);

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        //총알을 Target 방향으로 이동시킨다.
        StartCoroutine(BulletToTarget(bullets));
    }

    private IEnumerator BulletToTarget(IList<Transform> objects)
    {
        //0.5초 후에 시작
        yield return new WaitForSeconds(0.25f);

        while (true)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                //현재 총알의 위치에서 플레이의 위치의 벡터값을 뻴셈하여 방향을 구함
                Vector3 targetDirection = Target.transform.position - objects[i].position;

                //x,y의 값을 조합하여 Z방향 값으로 변형함. -> ~도 단위로 변형
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

                //Target 방향으로 이동
                objects[i].rotation = Quaternion.Euler(0, 0, angle);
            }
            //데이터 해제
            objects.Clear();

            yield return new WaitForSeconds(1f);

        }
    }

    private void Shot2ttt()
    {
        InvokeRepeating("Shot2", 1f, 2.5f);
    }

    private void ShapeInit()
    {
        //요소들이 들어 있을 수 있으니 초기화 하기전에 Clear한다.
        _v.Clear();
        _xx.Clear();

        //데이터 초기화
        _m = (int)Mathf.Floor(Subdivision / 2);
        _a = 2 * Mathf.Sin(Mathf.PI / Vertex);
        _phi = ((Mathf.PI / 2f) * (Vertex - 2f)) / Vertex;
        _v.Add(0);
        _xx.Add(0);

        for (int i = 1; i <= _m; i++)
        {
            //list.Insert(위치,요소) -> 해당 위치에 값을 집어넣습니다.
            _v.Add(Mathf.Sqrt(Subdivision * Subdivision - 2 * _a * Mathf.Cos(_phi) * i * Subdivision + _a * _a * i * i));
        }

        for (int i = 1; i <= _m; i++)
        {
            _xx.Add(Mathf.Rad2Deg * (Mathf.Asin(_a * Mathf.Sin(_phi) * i / _v[i])));
        }
    }

    private void Shot2()
    {
        Rotation = Random.Range(0, 360);
        Vertex = Random.Range(3, 7);
        Subdivision = Random.Range(1, 5);
        Speed = Random.Range(10, 20);

        shootbgm2.Play();

        //rot값에 영향을 주지 않도록 별도로 dir값을 선언하였다.
        float direction = Rotation;

        //꼭짓점 수 만큼 실행
        for (int r = 0; r < Vertex; r++)
        {
            for (int i = 1; i <= _m; i++)
            {
                #region //1차 생성

                //총알 생성
                GameObject idx1 = Instantiate(Bullet2);

                //2초후 삭제
                Destroy(idx1, 2f);

                //총알 생성 위치를 (0,0) 좌표로 한다.
                idx1.transform.position = transform.position;

                //정밀한 회전 처리로 모양을 만들어 낸다.
                idx1.transform.rotation = Quaternion.Euler(0, 0, direction + _xx[i]);

                //정밀한 속도 처리로 모양을 만들어 낸다.
                idx1.GetComponent<Bullet>().Speed = _v[i] * Speed / Subdivision;

                #endregion

                #region //2차 생성

                //총알 생성
                GameObject idx2 = Instantiate(Bullet2);

                //2초후 삭제
                Destroy(idx2, 2f);

                //총알 생성 위치를 (0,0) 좌표로 한다.
                idx2.transform.position = transform.position;

                //정밀한 회전 처리로 모양을 만들어 낸다.
                idx2.transform.rotation = Quaternion.Euler(0, 0, direction - _xx[i]);

                //정밀한 속도 처리로 모양을 만들어 낸다.
                idx2.GetComponent<Bullet>().Speed = _v[i] * Speed / Subdivision;

                #endregion

                #region //3차 생성

                //총알 생성
                GameObject idx3 = Instantiate(Bullet2);

                //2초후 삭제
                Destroy(idx3, 2f);

                //총알 생성 위치를 (0,0) 좌표로 한다.
                idx3.transform.position = transform.position;

                //정밀한 회전 처리로 모양을 만들어 낸다.
                idx3.transform.rotation = Quaternion.Euler(0, 0, direction);

                //정밀한 속도 처리로 모양을 만들어 낸다.
                idx3.GetComponent<Bullet>().Speed = Speed;

                #endregion

                //모양을 완성한다.
                direction += 360 / Vertex;
            }
        }
    }
}

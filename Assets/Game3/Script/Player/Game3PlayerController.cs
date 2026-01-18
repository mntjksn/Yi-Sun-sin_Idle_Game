using System.Collections;
using UnityEngine;

public class Game3PlayerController : MonoBehaviour
{
    // 플레이어 탄환 프리팹
    public GameObject PlayerBullet;

    // 발사 사운드
    public AudioSource shootbgm;

    // 기본 공격 속도
    [SerializeField]
    private float attackRate = 3f;

    // 스테이지 이동 제한 데이터
    public Game3StageData Game3StageData;

    public float AttackRate
    {
        get => attackRate;
        set => attackRate = Mathf.Max(0f, value);
    }

    private void Awake()
    {
        // 공격 시도 코루틴 시작
        StartCoroutine(TryAttack());
    }

    private void Update()
    {
        // 효과음 설정 값 적용
        int effectSound = PlayerPrefs.GetInt("EFFECT");

        if (effectSound == 0)
        {
            shootbgm.mute = false;
        }

        if (effectSound == 1)
        {
            shootbgm.mute = true;
        }
    }

    private void FixedUpdate()
    {
        // 보스 방향을 바라보도록 회전 처리
        Vector3 dir = GameObject.FindGameObjectWithTag("Boss").transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    private void LateUpdate()
    {
        // 스테이지 범위를 벗어나지 않도록 위치 제한
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, Game3StageData.LimitMin.x, Game3StageData.LimitMax.x),
            Mathf.Clamp(transform.position.y, Game3StageData.LimitMin.y, Game3StageData.LimitMax.y),
            0f
        );
    }

    private IEnumerator TryAttack()
    {
        // 시작 전 대기 시간
        yield return new WaitForSeconds(1f);

        while (true)
        {
            // 저장된 공격 속도 값 불러오기
            float attr = PlayerPrefs.GetFloat("AttackRate");

            // 발사 사운드 재생
            shootbgm.Play();

            // 탄환 생성
            Instantiate(
                PlayerBullet,
                new Vector2(transform.position.x, transform.position.y),
                Quaternion.identity
            );

            // 다음 공격까지 대기
            yield return new WaitForSeconds(attr);
        }
    }
}
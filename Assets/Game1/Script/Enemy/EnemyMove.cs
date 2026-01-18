using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // 보스가 멈추는 위치 범위
    public float bossAppearPoint = 2.5f;
    public float minbossAppearPoint;
    public float maxbossAppearPoint;

    // 적 탄환 프리팹
    public GameObject enemybulletPrefab;

    // 공격 주기
    public float attackRate;

    // 이동을 담당하는 Movement2D 컴포넌트
    private Movement2D movement2D;

    // 공격 시 재생되는 사운드
    public AudioSource enemybgm;

    private void Awake()
    {
        // Movement2D 컴포넌트 가져오기
        movement2D = GetComponent<Movement2D>();

        // 등장 위치까지 이동하는 코루틴 시작
        StartCoroutine(MoveToAppearPoint());
    }

    private void Update()
    {
        // 효과음 설정 값 가져오기
        int effectSound = PlayerPrefs.GetInt("EFFECT");

        // 효과음 켜짐
        if (effectSound == 0)
        {
            enemybgm.mute = false;
        }

        // 효과음 꺼짐
        if (effectSound == 1)
        {
            enemybgm.mute = true;
        }
    }

    private IEnumerator MoveToAppearPoint()
    {
        // 아래 방향으로 이동 시작
        movement2D.MoveTo(Vector3.down);

        // 보스가 멈출 위치 랜덤 설정
        float targetAppearPoint = Random.Range(minbossAppearPoint, maxbossAppearPoint);

        while (true)
        {
            // 지정된 위치에 도달했을 경우
            if (transform.position.y <= targetAppearPoint)
            {
                // 이동 정지
                movement2D.MoveTo(Vector3.zero);

                // 공격 시작
                StartCoroutine(Attack());

                // 공격 유지 시간
                yield return new WaitForSeconds(3f);

                // 공격 중지
                StopCoroutine(Attack());

                // 다시 위로 이동
                movement2D.MoveTo(Vector3.up);
            }

            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            // 공격 사운드 재생
            enemybgm.Play();

            // 탄환 생성
            Instantiate(
                enemybulletPrefab,
                new Vector2(transform.position.x, transform.position.y - 0.8f),
                Quaternion.identity
            );

            // 다음 공격까지 대기
            yield return new WaitForSeconds(attackRate);
        }
    }
}
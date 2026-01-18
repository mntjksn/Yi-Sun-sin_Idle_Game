using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BossState
{
    MoveToAppearPoint = 0,
    Phase01,
    Phase02,
    Phase03
}

public class Boss : MonoBehaviour
{
    // 스테이지 범위 데이터
    public Game3StageData Game3StageData;

    // 플레이어 컨트롤러 참조
    public Game3PlayerController Game3PlayerController;

    // 클리어 패널
    public GameObject panel;

    // 보스가 멈출 등장 위치
    public float bossAppearPoint = 2.5f;

    // 현재 보스 상태
    private BossState bossStage = BossState.MoveToAppearPoint;

    // 컴포넌트 참조
    private Movement2D movement2D;
    private BossWeapon bossWeapon;
    private BossHP bossHP;
    private MergeItem MergeItem;

    private void Awake()
    {
        // 컴포넌트 가져오기
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
        MergeItem = GetComponent<MergeItem>();

        // 초기 상태 시작
        ChangeState(BossState.MoveToAppearPoint);
    }

    public void ChangeState(BossState newState)
    {
        // 기존 상태 코루틴 중지 후 새 상태 코루틴 시작
        StopCoroutine(bossStage.ToString());
        bossStage = newState;
        StartCoroutine(bossStage.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        // 아래로 이동해서 등장 위치까지 내려오기
        movement2D.MoveTo(Vector3.down);

        while (true)
        {
            if (transform.position.y <= bossAppearPoint)
            {
                // 등장 위치 도달 시 정지 후 페이즈 진입
                movement2D.MoveTo(Vector3.zero);
                ChangeState(BossState.Phase01);

                // 보스 이동 패턴 코루틴 시작
                StartCoroutine("MoveBoss");
            }

            yield return null;
        }
    }

    private IEnumerator MoveBoss()
    {
        // 초기 이동 방향 설정
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        movement2D.MoveTo(direction);

        while (true)
        {
            // 좌우 벽 반사
            if (transform.position.x <= Game3StageData.LimitMin.x + 1f ||
                transform.position.x >= Game3StageData.LimitMax.x - 1f)
            {
                direction.x *= -1f;
                movement2D.MoveTo(direction);
                movement2D.moveSpeed = Random.Range(3f, 5f);
            }

            // 상하 벽 반사
            if (transform.position.y <= Game3StageData.LimitMin.y + 1f ||
                transform.position.y >= Game3StageData.LimitMax.y - 1f)
            {
                direction.y *= -1f;
                movement2D.MoveTo(direction);
                movement2D.moveSpeed = Random.Range(3f, 5f);
            }

            yield return null;
        }
    }

    private IEnumerator Phase01()
    {
        // 첫 페이즈 공격 시작
        bossWeapon.StartFiring(AttackType.CircleFire);

        while (true)
        {
            // 체력이 일정 이하가 되면 다음 페이즈로 전환
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.8f)
            {
                ChangeState(BossState.Phase02);
            }

            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        // 두번째 페이즈 공격 시작
        bossWeapon.StartFiring(AttackType.Shot2ttt);

        while (true)
        {
            // 체력이 일정 이하가 되면 다음 페이즈로 전환
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.4f)
            {
                ChangeState(BossState.Phase03);
            }

            yield return null;
        }
    }

    private IEnumerator Phase03()
    {
        // 마지막 페이즈 공격 시작
        bossWeapon.StartFiring(AttackType.Shotttt);

        yield return null;
    }

    public void OnDie()
    {
        // 보스 코인 보상 지급
        int addBossCoin = Random.Range(1, 6);
        int BossCoin = PlayerPrefs.GetInt("BossCoin");
        BossCoin += addBossCoin;
        PlayerPrefs.SetInt("BossCoin", BossCoin);

        // 클리어 처리
        panel.SetActive(true);
        Time.timeScale = 0f;

        // 보스 제거
        Destroy(gameObject);
    }
}
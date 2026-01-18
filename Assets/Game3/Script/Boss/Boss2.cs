using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss2 : MonoBehaviour
{
    // 애니메이터
    private Animator animator;

    // 스테이지 범위 데이터
    public Game3StageData Game3StageData;

    // 플레이어 컨트롤러 참조
    public Game3PlayerController Game3PlayerController;

    // 플레이어 체력 참조
    public Game3PlayerHP playerHP;

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

    // 근접 공격 판정 박스 위치
    public Transform boxpos;

    // 근접 공격 판정 박스 크기
    public Vector2 boxSize;

    private void Awake()
    {
        // 컴포넌트 가져오기
        animator = GetComponent<Animator>();
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

                // 보스 이동 패턴 시작
                StartCoroutine("MoveBoss");
            }

            yield return null;
        }
    }

    private IEnumerator MoveBoss()
    {
        // 초기 이동 방향 설정
        Vector3 direction = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(0.5f, 1.5f), 0f);
        movement2D.MoveTo(direction);

        // 근접 공격 루프 시작
        StartCoroutine(crab());

        while (true)
        {
            // 좌우 벽 반사
            if (transform.position.x <= Game3StageData.LimitMin.x + 1f ||
                transform.position.x >= Game3StageData.LimitMax.x - 1f)
            {
                direction.x *= -1f;
                movement2D.MoveTo(direction);
                movement2D.moveSpeed = Random.Range(5f, 9f);
            }

            // 상하 벽 반사
            if (transform.position.y <= Game3StageData.LimitMin.y + 1f ||
                transform.position.y >= Game3StageData.LimitMax.y - 1f)
            {
                direction.y *= -1f;
                movement2D.MoveTo(direction);
                movement2D.moveSpeed = Random.Range(5f, 9f);
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
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.7f)
            {
                bossWeapon.StopFiring(AttackType.CircleFire);
                ChangeState(BossState.Phase02);
            }

            yield return null;
        }
    }

    private IEnumerator crab()
    {
        while (true)
        {
            // 공격 애니메이션 켜기
            animator.SetBool("isattack", true);

            // 공격 범위 안의 오브젝트 확인
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0f);

            foreach (Collider2D collider in collider2Ds)
            {
                // 플레이어에게 데미지 적용
                if (collider.CompareTag("Player"))
                {
                    playerHP.TakeDamage(30);
                    Debug.Log("1");
                }
            }

            // 공격 애니메이션 끄기
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("isattack", false);

            // 다음 공격까지 대기
            yield return new WaitForSeconds(3f);
        }
    }

    public void OnDie()
    {
        // 골드 보상 지급
        int bossclear = PlayerPrefs.GetInt("Gold");
        bossclear += 10000;
        PlayerPrefs.SetInt("Gold", bossclear);

        // 클리어 처리
        panel.SetActive(true);
        Time.timeScale = 0f;

        // 보스 제거
        Destroy(gameObject);
    }
}
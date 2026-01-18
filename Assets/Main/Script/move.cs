using UnityEngine;

public class move : MonoBehaviour
{
    // 물리 이동용 리지드바디
    private Rigidbody2D rigid;

    // 이동 방향 값
    public int nextMove;

    // 애니메이터
    private Animator ani;

    // 스프라이트 렌더러
    private SpriteRenderer sprite;

    // 스테이지 데이터 참조
    public StageData StageData;

    // 이동 속도용 랜덤 값
    private float x;
    private float y;

    private void Awake()
    {
        // 컴포넌트 참조
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        // 첫 이동 방향 설정
        Invoke("Think", 0f);
    }

    private void Update()
    {
        // 현재 위치를 뷰포트 좌표로 변환
        Vector3 worldpos = Camera.main.WorldToViewportPoint(transform.position);

        // 속도 적용
        rigid.velocity = new Vector2(nextMove * x, nextMove * y);

        // 왼쪽 경계 처리
        if (worldpos.x < 0.075f)
        {
            worldpos.x = 0.075f;
            rigid.velocity = new Vector2(rigid.velocity.x * -1f, rigid.velocity.y);
            CancelInvoke();
            Think();
        }

        // 아래쪽 경계 처리
        if (worldpos.y < 0.175f)
        {
            worldpos.y = 0.175f;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * -1f);
            CancelInvoke();
            Think();
        }

        // 오른쪽 경계 처리
        if (worldpos.x > 0.925f)
        {
            worldpos.x = 0.925f;
            rigid.velocity = new Vector2(rigid.velocity.x * -1f, rigid.velocity.y);
            CancelInvoke();
            Think();
        }

        // 위쪽 경계 처리
        if (worldpos.y > 0.7f)
        {
            worldpos.y = 0.7f;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * -1f);
            CancelInvoke();
            Think();
        }

        // 경계 보정된 위치를 월드 좌표로 다시 반영
        transform.position = Camera.main.ViewportToWorldPoint(worldpos);

        // 이동 방향에 따라 스프라이트 방향 전환
        if (x < 0f)
        {
            sprite.flipX = nextMove == -1;
        }

        if (x > 0f)
        {
            sprite.flipX = nextMove == 1;
        }
    }

    private void Think()
    {
        // x 이동 속도 랜덤 결정
        int rax = Random.Range(0, 100);
        if (rax < 50)
        {
            x = Random.Range(-0.5f, -0.1f);
        }
        else
        {
            x = Random.Range(0.1f, 0.5f);
        }

        // y 이동 속도 랜덤 결정
        int ray = Random.Range(0, 100);
        if (ray < 50)
        {
            y = Random.Range(-0.5f, -0.1f);
        }
        else
        {
            y = Random.Range(0.1f, 0.5f);
        }

        // 이동 방향 랜덤 결정
        int ram = Random.Range(0, 100);
        if (ram < 50)
        {
            nextMove = -1;
        }
        else
        {
            nextMove = 1;
        }

        // 다음 방향 변경 예약
        Invoke("Think", 5f);

        // 애니메이션 파라미터 갱신
        ani.SetInteger("WalkSpeed", nextMove);
    }
}
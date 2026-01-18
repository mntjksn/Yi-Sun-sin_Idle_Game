using UnityEngine;

public class castledata : MonoBehaviour
{
    // 성의 리지드바디
    private Rigidbody2D myrigidbody;

    // 게임 오버 시 표시할 패널
    public GameObject panel;

    // 현재 체력
    public float hp;

    // 최대 체력
    public float maxhp;

    // 애니메이터
    private Animator animator;

    private void Start()
    {
        // 최대 체력 초기 설정
        maxhp = hp;

        // 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 체력이 절반 이하일 경우 불타는 상태로 변경
        if (hp < maxhp / 2f)
        {
            animator.SetBool("isburning", true);
        }

        // 체력이 모두 소모되었을 경우 게임 종료 처리
        if (hp < 0f)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
        }
    }
}
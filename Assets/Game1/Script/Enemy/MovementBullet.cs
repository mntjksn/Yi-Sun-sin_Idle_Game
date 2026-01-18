using UnityEngine;

public class MovementBullet : MonoBehaviour
{
    // 플레이어 위치
    private Vector3 targetPos;

    // 탄환 생성 위치
    private Vector3 myPos;

    // 이동 방향 벡터
    private Vector3 newPos;

    private Rigidbody2D rigid;

    private void Awake()
    {
        // 리지드바디 컴포넌트 가져오기
        rigid = GetComponent<Rigidbody2D>();

        // 플레이어 현재 위치 저장
        targetPos = GameObject.Find("Player").transform.position;

        // 탄환 생성 위치 저장
        myPos = transform.position;

        // 플레이어를 향한 방향 벡터 계산
        newPos = (targetPos - myPos).normalized;
    }

    private void Update()
    {
        // 플레이어 방향으로 탄환 이동
        rigid.velocity = (targetPos - myPos) * 1.5f;
    }
}
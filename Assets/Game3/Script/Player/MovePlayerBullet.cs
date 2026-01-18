using UnityEngine;

public class MovePlayerBullet : MonoBehaviour
{
    // 보스 위치
    private Vector3 targetPos;

    // 탄환 생성 위치
    private Vector3 myPos;

    // 이동 방향 벡터
    private Vector3 newPos;

    // 리지드바디
    private Rigidbody2D rigid;

    private void Awake()
    {
        // 리지드바디 가져오기
        rigid = GetComponent<Rigidbody2D>();

        // 보스 위치 저장
        targetPos = GameObject.FindGameObjectWithTag("Boss").transform.position;

        // 탄환 생성 위치 저장
        myPos = transform.position;

        // 보스 방향으로의 방향 벡터 계산
        newPos = (targetPos - myPos).normalized * 0.5f;
    }

    private void Update()
    {
        // 보스 방향으로 탄환 이동
        rigid.velocity = (targetPos - myPos) * 1.8f;
    }
}
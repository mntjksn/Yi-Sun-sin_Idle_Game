using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    // 기준이 될 타겟 위치
    public Transform target;

    // 다시 위로 이동할 기준 거리
    public float scrollRange = 9.9f;

    // 이동 속도
    public float moveSpeed;

    // 이동 방향
    public Vector3 moveDirection = Vector3.down;

    private void Update()
    {
        // 지정된 방향으로 지속 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 특정 위치 아래로 내려가면 위쪽으로 위치 재설정
        if (transform.position.y <= -scrollRange)
        {
            transform.position = target.position + Vector3.up * scrollRange;
        }
    }
}
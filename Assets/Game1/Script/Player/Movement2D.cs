using UnityEngine;

public class Movement2D : MonoBehaviour
{
    // 이동 속도
    public float moveSpeed;

    // 현재 이동 방향
    public Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        // 설정된 방향과 속도로 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        // 이동 방향 설정
        moveDirection = direction;
    }
}
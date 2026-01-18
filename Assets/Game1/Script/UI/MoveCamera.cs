using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // 따라갈 대상 트랜스폼
    public Transform target;

    // 카메라 이동 속도
    public float speed;

    // 카메라 이동 제한 영역의 중심
    public Vector2 center;

    // 카메라 이동 제한 영역 크기
    public Vector2 size;

    // 카메라 세로 반경
    private float height;

    // 카메라 가로 반경
    private float width;

    private void Awake()
    {
        // 카메라의 화면 크기 계산
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        // 카메라 이동 가능 영역을 씬 뷰에 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    private void LateUpdate()
    {
        // 따라갈 대상이 있을 경우만 처리
        if (target == null) return;

        // 대상 위치로 부드럽게 이동
        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // 가로 이동 제한 계산
        float limitX = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(
            transform.position.x,
            -limitX + center.x,
            limitX + center.x
        );

        // 세로 이동 제한 계산
        float limitY = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(
            transform.position.y,
            -limitY + center.y,
            limitY + center.y
        );

        // 제한된 위치로 카메라 위치 설정
        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
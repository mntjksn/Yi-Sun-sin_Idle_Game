using UnityEngine;

public class Game3Destroy : MonoBehaviour
{
    // 스테이지 범위 데이터
    public Game3StageData Game3StageData;

    // 위쪽 여유 제거 범위
    public float YDesPos = 0f;

    private void LateUpdate()
    {
        // 스테이지 범위를 벗어난 오브젝트 제거
        if (transform.position.y < Game3StageData.LimitMin.y - 1f ||
            transform.position.y > Game3StageData.LimitMax.y + YDesPos ||
            transform.position.x < Game3StageData.LimitMin.x - 1f ||
            transform.position.x > Game3StageData.LimitMax.x + 1f)
        {
            Destroy(gameObject);
        }
    }
}
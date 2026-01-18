using UnityEngine;

public class Game1Destroy : MonoBehaviour
{
    // 스테이지 범위 데이터
    public Game1StageData Game1StageData;

    // 위쪽 파괴 여유 범위
    public float YDesPos = 2f;

    private void LateUpdate()
    {
        // 스테이지 범위를 벗어났는지 확인
        bool isOutOfY =
            transform.position.y < Game1StageData.LimitMin.y - 2f ||
            transform.position.y > Game1StageData.LimitMax.y + YDesPos;

        bool isOutOfX =
            transform.position.x < Game1StageData.LimitMin.x - 2f ||
            transform.position.x > Game1StageData.LimitMax.x + 2f;

        // 범위를 벗어난 경우 오브젝트 제거
        if (isOutOfY || isOutOfX)
        {
            Destroy(gameObject);
        }
    }
}
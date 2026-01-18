using UnityEngine;
using TMPro;

public class ScoreViewer : MonoBehaviour
{
    // 플레이어 점수 정보를 가져오기 위한 컨트롤러
    public Game1PlayerController Game1PlayerController;

    // 점수 표시용 텍스트
    private TextMeshProUGUI textScore;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        textScore = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // 현재 획득한 코인 수 표시
        textScore.text = "획득한 코인수 : " + Game1PlayerController.Score;
    }
}
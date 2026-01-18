using UnityEngine;
using TMPro;

public class GameGoldViewer : MonoBehaviour
{
    // 골드 표시용 텍스트
    private TextMeshProUGUI textScore;

    private void Awake()
    {
        // 같은 오브젝트에 있는 TextMeshProUGUI 컴포넌트 가져오기
        textScore = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // PlayerPrefs에 저장된 골드 값 불러오기
        int gameGold = PlayerPrefs.GetInt("GameGold");

        // 천 단위 콤마를 적용해 텍스트로 표시
        textScore.text = " : " + string.Format("{0:#,0}", gameGold);
    }
}
using UnityEngine;
using TMPro;

public class BossticketCount : MonoBehaviour
{
    // 보스 티켓 수를 표시할 텍스트
    private TextMeshProUGUI textScore;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        textScore = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // 저장된 보스 티켓 수 불러오기
        int bossTicket = PlayerPrefs.GetInt("BossTicket");

        // 천 단위 콤마 적용 후 표시
        textScore.text = string.Format("{0:#,0}", bossTicket) + "개";
    }
}
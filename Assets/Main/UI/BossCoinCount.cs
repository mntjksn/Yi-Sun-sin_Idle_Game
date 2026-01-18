using UnityEngine;
using TMPro;

public class BossCoinCount : MonoBehaviour
{
    // 진주 개수 표시 텍스트
    private TextMeshProUGUI textScore;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        textScore = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // 저장된 보스 코인 값 불러오기
        int bossCoin = PlayerPrefs.GetInt("BossCoin");

        // 텍스트 갱신
        textScore.text = "현재 진주 : " + string.Format("{0:#,0}", bossCoin) + "개";
    }
}
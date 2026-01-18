using UnityEngine;
using TMPro;

public class SpawnLimitText : MonoBehaviour
{
    // 스폰 횟수 표시용 텍스트
    private TextMeshProUGUI spawnCountText;

    void Start()
    {
        // 같은 오브젝트에 있는 TextMeshProUGUI 컴포넌트 가져오기
        spawnCountText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // 현재 스폰 횟수
        int currentCount = PlayerPrefs.GetInt("ClickNum");

        // 최대 스폰 가능 횟수
        int maxCount = PlayerPrefs.GetInt("ClickMax");

        // 현재 / 최대 형식으로 텍스트 표시
        spawnCountText.text = currentCount + "/" + maxCount;
    }
}
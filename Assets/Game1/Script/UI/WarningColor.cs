using System.Collections;
using UnityEngine;
using TMPro;

public class WarningColor : MonoBehaviour
{
    // 색상 전환에 걸리는 시간
    public float lerpTime = 0.5f;

    // 경고 텍스트 컴포넌트
    private TextMeshProUGUI textBossWarning;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        textBossWarning = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        // 색상 변화 반복 코루틴 시작
        StartCoroutine(ColorLerpLoop());
    }

    private IEnumerator ColorLerpLoop()
    {
        // 흰색과 빨간색을 반복하며 전환
        while (true)
        {
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
        }
    }

    private IEnumerator ColorLerp(Color startColor, Color endColor)
    {
        // 경과 시간
        float currentTime = 0f;

        // 진행 비율
        float percent = 0f;

        while (percent < 1f)
        {
            // 시간 누적
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            // 텍스트 색상 보간 처리
            textBossWarning.color = Color.Lerp(startColor, endColor, percent);

            yield return null;
        }
    }
}
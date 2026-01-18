using System.Collections;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    // 페이드 인 아웃에 걸리는 시간
    public float fadeTime = 0.1f;

    // 스프라이트 렌더러 참조
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // 스프라이트 렌더러 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 반짝임 반복 코루틴 시작
        StartCoroutine(TwinkleLoop());
    }

    private IEnumerator TwinkleLoop()
    {
        // 계속 반복되는 반짝임 효과
        while (true)
        {
            // 투명해지기
            yield return StartCoroutine(FadeEffect(1f, 0f));

            // 다시 보이기
            yield return StartCoroutine(FadeEffect(0f, 1f));
        }
    }

    private IEnumerator FadeEffect(float start, float end)
    {
        // 경과 시간
        float currentTime = 0f;

        // 진행 비율
        float percent = 0f;

        while (percent < 1f)
        {
            // 시간 누적
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // 알파 값 보간 처리
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent);
            spriteRenderer.color = color;

            yield return null;
        }
    }
}
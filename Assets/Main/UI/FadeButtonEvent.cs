using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeButtonEvent : MonoBehaviour
{
    [Header("Refs")]
    public Image FadePanel;     // 페이드에 사용할 UI 패널(이미지)
    public string SceneName;    // 페이드 완료 후 이동할 씬 이름

    [Header("Fade Setting")]
    [SerializeField] private float F_time = 1f; // 페이드에 걸리는 시간(초)

    private float time = 0f;    // 보간 진행값(0~1)

    public void Fade()
    {
        // 버튼 클릭 등에서 호출
        // 문자열로 코루틴 호출하는 방식은 유지
        StartCoroutine("FadeFlow");
    }

    private IEnumerator FadeFlow()
    {
        // 패널이 없으면 진행 불가
        if (FadePanel == null)
            yield break;

        // 페이드 패널 활성화
        FadePanel.gameObject.SetActive(true);

        // 페이드 시작 준비
        time = 0f;
        Color alpha = FadePanel.color;

        // 알파를 0에서 시작하도록 초기화(현재 색상 유지)
        alpha.a = 0f;
        FadePanel.color = alpha;

        // 0 -> 1 페이드 인
        while (alpha.a < 1f)
        {
            // F_time이 0이면 나눗셈 문제가 생기므로 최소값 보정
            float safeFadeTime = (F_time <= 0f) ? 0.0001f : F_time;

            time += Time.deltaTime / safeFadeTime;
            alpha.a = Mathf.Lerp(0f, 1f, time);
            FadePanel.color = alpha;

            yield return null;
        }

        // 페이드가 완전히 끝나도록 보정
        alpha.a = 1f;
        FadePanel.color = alpha;

        // 씬 로드 전 잠깐 대기
        yield return new WaitForSeconds(0.25f);

        // 씬 이름이 비어있으면 로드하지 않음
        if (string.IsNullOrEmpty(SceneName))
            yield break;

        // 씬 이동
        SceneManager.LoadScene(SceneName);
    }
}
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // 타이머 텍스트
    private TextMeshProUGUI timer;

    // 남은 시간
    private float time;

    // 시간 종료 시 표시할 패널
    public GameObject panel;

    // 플레이어 컨트롤러 참조
    private Game1PlayerController Game1PlayerController;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        timer = GetComponent<TextMeshProUGUI>();

        // 플레이어 컨트롤러 가져오기
        Game1PlayerController = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<Game1PlayerController>();

        // 초기 제한 시간 설정
        time = 60f;
    }

    private void Update()
    {
        // 남은 시간 텍스트 표시
        timer.text = "남은 시간 : " + string.Format("{0:N2}", time) + " 초";

        // 시간이 남아 있을 경우 감소 처리
        if (time > 0f)
        {
            time -= Time.deltaTime;

            // 시간이 모두 소모되었을 경우
            if (time < 0f)
            {
                // 결과 패널 표시
                panel.SetActive(true);

                // 게임 시간 정지
                Time.timeScale = 0f;
            }
        }
    }
}
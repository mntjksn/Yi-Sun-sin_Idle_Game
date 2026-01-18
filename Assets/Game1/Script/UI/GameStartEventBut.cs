using UnityEngine;

public class GameStartEventBut : MonoBehaviour
{
    // 게임 시작 전 표시되는 패널
    public GameObject panel;

    private void Start()
    {
        // 게임 시작 전 시간 정지
        Time.timeScale = 0f;
    }

    public void gamestart()
    {
        // 시작 패널 비활성화
        panel.SetActive(false);

        // 게임 시간 재개
        Time.timeScale = 1f;
    }
}
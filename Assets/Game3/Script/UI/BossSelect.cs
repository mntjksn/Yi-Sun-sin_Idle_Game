using UnityEngine;

public class BossSelect : MonoBehaviour
{
    // 보스1 오브젝트
    public GameObject boss1;

    // 보스2 오브젝트
    public GameObject boss2;

    // 선택 화면 패널
    public GameObject panel;

    // 보스1 선택 여부
    public bool boss1s;

    // 보스2 선택 여부
    public bool boss2s;

    private void Awake()
    {
        // 보스 선택 전 게임 정지
        Time.timeScale = 0f;
    }

    public void boss1select()
    {
        // 선택 화면 닫기
        panel.SetActive(false);

        // 보스1 활성화
        boss1.SetActive(true);
        boss1s = true;

        // 게임 시작
        Time.timeScale = 1f;
    }

    public void boss2select()
    {
        // 선택 화면 닫기
        panel.SetActive(false);

        // 보스2 활성화
        boss2.SetActive(true);
        boss2s = true;

        // 게임 시작
        Time.timeScale = 1f;
    }
}
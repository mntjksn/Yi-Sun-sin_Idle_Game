using UnityEngine;
using UnityEngine.UI;

public class PanelEvent : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panel;   // 열고 닫을 UI 패널

    [Header("Data")]
    public Game2Data dm;       // 게임 데이터 저장용 클래스
    public DataManager dm1;    // 공용 데이터 매니저

    public void OnPanel()
    {
        // 패널 활성화
        if (panel)
            panel.SetActive(true);

        // 게임 데이터 저장
        if (dm)
            dm.Game2save();

        if (dm1)
            dm1.SaveGameData();
    }

    public void OffPanel()
    {
        // 패널 비활성화
        if (panel)
            panel.SetActive(false);
    }
}
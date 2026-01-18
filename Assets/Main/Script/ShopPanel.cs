using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    // 표시할 패널
    public GameObject panel;

    // 데이터 패널 여부 체크
    public bool data;

    public void OnPanel()
    {
        // 패널 열기
        panel.SetActive(true);
    }

    public void OffPanel()
    {
        // 패널 닫기
        panel.SetActive(false);
    }

    public void Data()
    {
        // 데이터 모드일 때 패널 토글 처리
        if (data == true && panel.activeSelf == true)
        {
            panel.SetActive(false);
        }
        else if (data == true && panel.activeSelf == false)
        {
            panel.SetActive(true);
        }
    }
}
using UnityEngine;

public class BookPanel : MonoBehaviour
{
    // 전체 패널과 각 페이지 오브젝트
    public GameObject panel;
    public GameObject Page_1;
    public GameObject Page_2;
    public GameObject Page_3;

    public void OnPanel()
    {
        // 패널 열기와 첫 페이지 표시
        panel.SetActive(true);
        Page_1.SetActive(true);
        Page_2.SetActive(false);
        Page_3.SetActive(false);
    }

    public void OffPanel()
    {
        // 패널 닫기
        panel.SetActive(false);
    }

    public void leftbut()
    {
        // 오른쪽 페이지에서 가운데 페이지로 이동
        if (Page_3.activeSelf == true)
        {
            Page_1.SetActive(false);
            Page_2.SetActive(true);
            Page_3.SetActive(false);
        }
        // 가운데 페이지에서 첫 페이지로 이동
        else if (Page_2.activeSelf == true)
        {
            Page_1.SetActive(true);
            Page_2.SetActive(false);
            Page_3.SetActive(false);
        }
    }

    public void rightbut()
    {
        // 첫 페이지에서 가운데 페이지로 이동
        if (Page_1.activeSelf == true)
        {
            Page_1.SetActive(false);
            Page_2.SetActive(true);
            Page_3.SetActive(false);
        }
        // 가운데 페이지에서 오른쪽 페이지로 이동
        else if (Page_2.activeSelf == true)
        {
            Page_1.SetActive(false);
            Page_2.SetActive(false);
            Page_3.SetActive(true);
        }
    }
}
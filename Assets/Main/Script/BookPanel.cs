using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPanel : MonoBehaviour
{
    public GameObject panel, Page_1, Page_2, Page_3;

    public void OnPanel()
    {
        panel.SetActive(true);
        Page_1.SetActive(true);
        Page_2.SetActive(false);
        Page_3.SetActive(false);
    }

    public void OffPanel()
    {
        panel.SetActive(false);
    }

    public void leftbut()
    {
        if (Page_1.activeSelf == false && Page_2.activeSelf == false && Page_3.activeSelf == true)
        {
            Page_1.SetActive(false);
            Page_2.SetActive(true);
            Page_3.SetActive(false);
        }

        else if (Page_1.activeSelf == false && Page_2.activeSelf == true && Page_3.activeSelf == false)
        {
            Page_1.SetActive(true);
            Page_2.SetActive(false);
            Page_3.SetActive(false);
        }
    }

    public void rightbut()
    {
        if (Page_1.activeSelf == true && Page_2.activeSelf == false && Page_3.activeSelf == false)
        {
            Page_1.SetActive(false);
            Page_2.SetActive(true);
            Page_3.SetActive(false);
        }

        else if (Page_1.activeSelf == false && Page_2.activeSelf == true && Page_3.activeSelf == false)
        {
            Page_1.SetActive(false);
            Page_2.SetActive(false);
            Page_3.SetActive(true);
        }
    }
}

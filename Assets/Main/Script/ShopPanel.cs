using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public GameObject panel;

    public bool data;

    public void OnPanel()
    {
        panel.SetActive(true);
    }

    public void OffPanel()
    {
        panel.SetActive(false);
    }

    public void Data()
    {
        if (data == true && panel.activeSelf == true)
            panel.SetActive(false);

        else if (data == true && panel.activeSelf == false)
            panel.SetActive(true);
    }
}

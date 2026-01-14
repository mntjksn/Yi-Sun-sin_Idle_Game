using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop3_up_down : MonoBehaviour
{
    public Button btn1, btn2;

    int upch, count;

    void Update()
    {
        upch = PlayerPrefs.GetInt("UpCh");
        count = PlayerPrefs.GetInt("Count");

        if (upch == 0)
        {
            btn1.interactable = false;
            btn2.interactable = false;
        }

        else if (upch == count)
        {
            btn1.interactable = false;
            btn2.interactable = true;
        }

        else if (count != 0 && upch > count)
        {
            btn1.interactable = true;
            btn2.interactable = true;
        }

        else if (count == 0)
        {
            btn1.interactable = true;
            btn2.interactable = false;
        }
    }

    public void btn_up()
    {
        count += 1;
        PlayerPrefs.SetInt("Count", count);
    }

    public void btn_down()
    {
        count -= 1;
        PlayerPrefs.SetInt("Count", count);
    }
}

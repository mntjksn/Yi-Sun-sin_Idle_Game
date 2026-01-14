using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_con : MonoBehaviour
{
    public Button btn1, btn2, btn3, btn4;

    void Update()
    {
        int effect_sound = PlayerPrefs.GetInt("EFFECT");
        int main_bgm = PlayerPrefs.GetInt("BGM");

        if (main_bgm == 0)
        {
            btn1.interactable = false;
            btn2.interactable = true;
        }

        if (main_bgm == 1)
        {
            btn1.interactable = true;
            btn2.interactable = false;
        }

        if (effect_sound == 0)
        {
            btn3.interactable = false;
            btn4.interactable = true;
        }

        if (effect_sound == 1)
        {
            btn3.interactable = true;
            btn4.interactable = false;
        }
    }

    public void main_bgm_on()
    {
        PlayerPrefs.SetInt("BGM", 0);
    }

    public void main_bgm_off()
    {
        PlayerPrefs.SetInt("BGM", 1);
    }

    public void bgm_on()
    {
        PlayerPrefs.SetInt("EFFECT", 0);
    }

    public void bgm_off()
    {
        PlayerPrefs.SetInt("EFFECT", 1);
    }
}

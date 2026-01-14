using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Sound : MonoBehaviour
{
    public AudioSource bgm;
    public bool main, effect;

    // Update is called once per frame
    void Update()
    {
        int main_bgm = PlayerPrefs.GetInt("BGM");
        int effect_bgm = PlayerPrefs.GetInt("EFFECT");

        if (main == true)
        {
            if (main_bgm == 0)
            {
                bgm.mute = false;
            }

            if (main_bgm == 1)
            {
                bgm.mute = true;
            }
        }

        if (effect == true)
        {
            if (effect_bgm == 0)
            {
                bgm.mute = false;
            }

            if (effect_bgm == 1)
            {
                bgm.mute = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopBuyGoldViewer : MonoBehaviour
{
    private TextMeshProUGUI GoldViewer;

    public bool but_1, but_2, but_3, but_4, but_2_1, but_2_2, but_2_3, but_2_4, but_3_1, but_3_2;

    private void Awake()
    {
        GoldViewer = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (but_1 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_1");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 골드";
        }

        if (but_2 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_2");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 골드";

            float getGoldTime = PlayerPrefs.GetFloat("GetGoldTime");

            if (getGoldTime <= 1.1f)
                GoldViewer.text = "MAX";
        }

        if (but_3 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_3");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 골드";
        }

        if (but_4 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_4");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 골드";

            float spawnTime = PlayerPrefs.GetFloat("SpawnTime");

            if (spawnTime <= 1.1f)
                GoldViewer.text = "MAX";
        }

        if (but_2_1 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_5");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 엽전";
        }

        if (but_2_2 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_6");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 엽전";
        }

        if (but_2_3 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_7");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 엽전";

            float attr = PlayerPrefs.GetFloat("AttackRate");

            if (attr <= 0.4f)
                GoldViewer.text = "MAX";
        }

        if (but_2_4 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_8");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 엽전";

            float spd = PlayerPrefs.GetFloat("Speed");

            if (spd >= 15f)
                GoldViewer.text = "MAX";
        }

        if (but_3_1 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_9");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 진주";
        }

        if (but_3_2 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_10");

            GoldViewer.text = string.Format("{0:#,0}", gold) + " 진주";
        }
    }
}

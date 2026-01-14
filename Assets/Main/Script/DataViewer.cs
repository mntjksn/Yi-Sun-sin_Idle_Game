using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataViewer : MonoBehaviour
{
    public bool at, hp, attr, spd, hp_add, at_add, gold;

    private TextMeshProUGUI text;
    public GameObject panel;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {      
        if (hp == true)
        {
            float MHP = PlayerPrefs.GetFloat("MaxHP");

            text.text = "기본 체력 : " + MHP + " HP";
        }

        if (hp_add == true)
        {
            text.text = "(추가 선원 당 체력 + " + GameObject.Find("chp").GetComponent<chparent>().hp.ToString() + " HP)";
        }

        if (at == true)
        {
            float dmg = PlayerPrefs.GetFloat("Damage");

            text.text = "기본 공격력 : " + dmg + " ATT)";
        }

        if (at_add == true)
        {
            text.text = "(추가 선원 당 공격력 + " + GameObject.Find("chp").GetComponent<chparent>().at.ToString() + " ATT)";
        }

        if (attr == true)
        {
            float attr = PlayerPrefs.GetFloat("AttackRate");

            text.text = "기본 공격속도 : " + attr.ToString("F1") + " ATTR";
        }

        if (spd == true)
        {
            float spd = PlayerPrefs.GetFloat("Speed");

            text.text = "기본 이동속도 : " + spd.ToString("F2") + " SPD";
        }

        if (gold == true)
        {
            float getgoldTime = GameObject.Find("chp").GetComponent<chparent>().gold;
            int upgold = PlayerPrefs.GetInt("UpGold");

            text.text = "총 골드 획득량 : " + string.Format("{0:#,0}", getgoldTime * upgold);
        }
    }

    public void OffPanel()
    {
        panel.SetActive(false);
    }
}

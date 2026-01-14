using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopdataViewer : MonoBehaviour
{
    private TextMeshProUGUI data;

    public bool but_1_1, but_1_2, but_1_3, but_1_4, but_2_1, but_2_2, but_2_3, but_2_4;

    private void Awake()
    {
        data = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (but_1_1 == true)
        {
            int childMax = PlayerPrefs.GetInt("ChildMax");
            int nextchildMax = childMax + 1;

            data.text = childMax +"명" + " ⇒ " + nextchildMax + "명";
        }

        if (but_1_2 == true)
        {
            float getGoldTime = PlayerPrefs.GetFloat("GetGoldTime");
            float nextgetGoldTime = getGoldTime - 0.1f;

            data.text = getGoldTime.ToString("F1") + "초" + " ⇒ " + nextgetGoldTime.ToString("F1") + "초";

            if (getGoldTime <= 1.1f)
                data.text = "UPGRAGE MAX";
        }

        if (but_1_3 == true)
        {
            int clickMax = PlayerPrefs.GetInt("ClickMax");
            int nextclickMax = clickMax + 1;

            data.text = clickMax + "명" + " ⇒ " + nextclickMax + "명";
        }

        if (but_1_4 == true)
        {
            float spawnTime = PlayerPrefs.GetFloat("SpawnTime");
            float nextspawnTime = spawnTime - 0.1f;

            data.text = spawnTime.ToString("F1") + "초" + " ⇒ " + nextspawnTime.ToString("F1") + "초";

            if (spawnTime <= 1.1f)
                data.text = "UPGRAGE MAX";
        }

        if (but_2_1 == true)
        {
            float MHP = PlayerPrefs.GetFloat("MaxHP");
            float nextMHP = MHP + 10f;

            data.text = MHP.ToString("F0") + " HP" +  " ⇒ " + nextMHP.ToString("F0") + " HP";
        }

        if (but_2_2 == true)
        {
            float dmg = PlayerPrefs.GetFloat("Damage");
            float nextdmg = dmg + 1f;

            data.text = dmg.ToString("F0") + " ATT" + " ⇒ " + nextdmg.ToString("F0") + " ATT";
        }

        if (but_2_3 == true)
        {
            float attr = PlayerPrefs.GetFloat("AttackRate");
            float nextattr = attr - 0.1f;

            data.text = attr.ToString("F1") + " ATTR" + " ⇒ " + nextattr.ToString("F1") + " ATTR";

            if (attr <= 0.4f)
                data.text = "UPGRAGE MAX";
        }

        if (but_2_4 == true)
        {
            float spd = PlayerPrefs.GetFloat("Speed");
            float nextspd = spd + 0.25f;

            data.text = spd.ToString("F2") + " SPD" + " ⇒ " + nextspd.ToString("F2") + " SPD";

            if (spd >= 15f)
                data.text = "UPGRAGE MAX";
        }
    }
}

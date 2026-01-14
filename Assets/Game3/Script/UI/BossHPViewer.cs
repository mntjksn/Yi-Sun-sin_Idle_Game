using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    public BossHP bossHP;
    public BossHP boss2HP;
    public BossSelect Bs;

    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        if(Bs.boss1s == true)
        {
            sliderHP.value = bossHP.CurrentHP / bossHP.MaxHP;
        }
        else if(Bs.boss2s == true)
        {
            sliderHP.value = boss2HP.CurrentHP / boss2HP.MaxHP;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game3PlayerHPViewer : MonoBehaviour
{
    public Game3PlayerHP Game3PlayerHP;

    private Slider sliderHP;

    private float currentHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();

        float MHP = PlayerPrefs.GetFloat("MaxHP");
        MHP += GameObject.Find("chp").GetComponent<chparent>().hp;

        currentHP = MHP;
    }

    // Update is called once per frame
    void Update()
    {
        sliderHP.value = Game3PlayerHP.CurrentHP / currentHP;
    }
}

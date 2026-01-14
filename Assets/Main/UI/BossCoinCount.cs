using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossCoinCount : MonoBehaviour
{
    private TextMeshProUGUI textScore;

    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int BossCoin = PlayerPrefs.GetInt("BossCoin");

        textScore.text = "현재 진주 : " + string.Format("{0:#,0}", BossCoin) + "개";
    }
}

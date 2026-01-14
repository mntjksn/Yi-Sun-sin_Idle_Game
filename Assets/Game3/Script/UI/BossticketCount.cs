using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossticketCount : MonoBehaviour
{
    private TextMeshProUGUI textScore;

    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int bossTicket = PlayerPrefs.GetInt("BossTicket");

        textScore.text = string.Format("{0:#,0}", bossTicket) + "개";
    }
}

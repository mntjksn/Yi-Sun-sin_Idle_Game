using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnLimitText : MonoBehaviour
{
    private TextMeshProUGUI SpawnCount;

    void Start()
    {
        SpawnCount = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int num = PlayerPrefs.GetInt("ClickNum");
        int clickmax = PlayerPrefs.GetInt("ClickMax");

        SpawnCount.text =  num + "/" + clickmax;
    }
}

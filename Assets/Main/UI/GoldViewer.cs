using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldViewer : MonoBehaviour
{
    public MergeItem MergeItem;

    private TextMeshProUGUI textScore;
    

    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int gold = PlayerPrefs.GetInt("Gold");
        //string result;
        textScore.text = " : " + string.Format("{0:#,0}", gold);
        //textScore.text = "Gold : " + gold;
    }
}

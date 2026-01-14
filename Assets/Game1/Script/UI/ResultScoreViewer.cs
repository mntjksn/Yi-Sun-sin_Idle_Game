using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScoreViewer : MonoBehaviour
{
    public Game1PlayerController Game1PlayerController;

    private TextMeshProUGUI textScore;

    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int resultscore = PlayerPrefs.GetInt("GameGold");

        textScore.text = "획득한 코인수 : " + Game1PlayerController.Score + "\n현재 코인수 : " + resultscore;
    }
}

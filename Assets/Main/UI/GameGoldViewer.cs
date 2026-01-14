using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameGoldViewer : MonoBehaviour
{
    private Game1PlayerController Game1PlayerController;

    private TextMeshProUGUI textScore;


    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
        Game1PlayerController = GetComponent<Game1PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        int gamegold = PlayerPrefs.GetInt("GameGold");

        textScore.text = " : " + string.Format("{0:#,0}", gamegold);
    }
}

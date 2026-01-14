using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class game2result : MonoBehaviour
{
    public GameObject enemy;
    public int curcoin;
    private TextMeshProUGUI textScore;

    public bool cu, end;
    
    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        curcoin = PlayerPrefs.GetInt("GameGold");
    }

    // Update is called once per frame
    void Update()
    {
        if (end == true)
            textScore.text = "획득한 코인수 : " + enemy.GetComponent<enemySpawn>().dieenemy + "\n현재 코인수 : " + curcoin;

        if (cu == true)
            textScore.text = "죽인 적 : " + enemy.GetComponent<enemySpawn>().dieenemy;
    }
}

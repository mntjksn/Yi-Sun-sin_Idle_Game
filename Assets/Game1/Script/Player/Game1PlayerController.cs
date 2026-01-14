using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1PlayerController : MonoBehaviour
{
    public Game1StageData Game1StageData;

    private int score;
    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }

    private int gameGold;
    public int GameGold
    {
        set => gameGold = Mathf.Max(0, value);
        get => gameGold;
    }

    private int bossTicket;
    public int BossTicket
    {
        set => bossTicket = Mathf.Max(0, value);
        get => bossTicket;
    }

    public int scorePoint = 1;
    private int num = 1;

    public AudioSource goldbgm;

    private void Update()
    {
        int effect_sound = PlayerPrefs.GetInt("EFFECT");

        if (effect_sound == 0)
        {
            goldbgm.mute = false;
        }

        if (effect_sound == 1)
        {
            goldbgm.mute = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            goldbgm.Play();
            OnDie();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Ticket"))
        {
            goldbgm.Play();
            OnDie2();
            Destroy(collision.gameObject);
        }
    }

    public void OnDie()
    {
        Score += scorePoint;

        GameGold = PlayerPrefs.GetInt("GameGold");
        GameGold += num;
        PlayerPrefs.SetInt("GameGold", GameGold);
    }

    public void OnDie2()
    {
        //Score += scorePoint;

        BossTicket = PlayerPrefs.GetInt("BossTicket");
        BossTicket += num;
        PlayerPrefs.SetInt("BossTicket", BossTicket);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Game1StageData.LimitMin.x, Game1StageData.LimitMax.x),
                                                              Mathf.Clamp(transform.position.y, Game1StageData.LimitMin.y, Game1StageData.LimitMax.y));
    }
}

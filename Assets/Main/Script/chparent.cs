using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chparent : MonoBehaviour
{
    public DataManager dm;
    Item item;
    public float at;
    public float hp;
    public float gold;

    // Start is called before the first frame update
    private void Awake()
    {
        var objs = FindObjectsOfType<chparent>();
        if(objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Invoke("attack", 0.2f);
        Invoke("ch_hp", 0.2f);
        Invoke("ch_gold", 0.2f);

        int g1 = PlayerPrefs.GetInt("Buy_1");
        int g2 = PlayerPrefs.GetInt("Buy_2");
        int g3 = PlayerPrefs.GetInt("Buy_3");
        int g4 = PlayerPrefs.GetInt("Buy_4");

        int g5 = PlayerPrefs.GetInt("Buy_5");
        int g6 = PlayerPrefs.GetInt("Buy_6");
        int g7 = PlayerPrefs.GetInt("Buy_7");
        int g8 = PlayerPrefs.GetInt("Buy_8");

        int g9 = PlayerPrefs.GetInt("Buy_9");
        int g10 = PlayerPrefs.GetInt("Buy_10");

        int cm = PlayerPrefs.GetInt("ChildMax");
        float gt = PlayerPrefs.GetFloat("GetGoldTime");
        float st = PlayerPrefs.GetFloat("SpawnTime");
        int cn = PlayerPrefs.GetInt("ClickMax");

        float mhp = PlayerPrefs.GetFloat("MaxHP");
        float dmg = PlayerPrefs.GetFloat("Damage");
        float attr = PlayerPrefs.GetFloat("AttackRate");
        float spd = PlayerPrefs.GetFloat("Speed");

        if (g1 == 0 || g2 == 0 || g3 == 0 || g4 == 0 || g5 == 0 || g6 == 0 || g7 == 0 || g8 == 0|| cm == 0 || gt == 0 || cn == 0 || st == 0 || mhp == 0 || dmg == 0 || attr == 0 || spd == 0)
        {
            PlayerPrefs.SetInt("Buy_1", 374);
            PlayerPrefs.SetInt("Buy_2", 241);
            PlayerPrefs.SetInt("Buy_3", 421);
            PlayerPrefs.SetInt("Buy_4", 324);

            PlayerPrefs.SetInt("Buy_5", 1);
            PlayerPrefs.SetInt("Buy_6", 1);
            PlayerPrefs.SetInt("Buy_7", 1);
            PlayerPrefs.SetInt("Buy_8", 1);

            PlayerPrefs.SetInt("ChildMax", 5);
            PlayerPrefs.SetInt("ClickMax", 5);
            PlayerPrefs.SetInt("ClickNum", 1);

            PlayerPrefs.SetFloat("GetGoldTime", 5.0f);
            PlayerPrefs.SetFloat("SpawnTime", 5.0f);

            PlayerPrefs.SetFloat("MaxHP", 10f);
            PlayerPrefs.SetFloat("Damage", 1f);
            PlayerPrefs.SetFloat("AttackRate", 3f);
            PlayerPrefs.SetFloat("Speed", 3f);
        }
        
        if (g9 == 0 || g10 == 0)
        {
            PlayerPrefs.SetInt("Buy_9", 10);
            PlayerPrefs.SetInt("Buy_10", 20);

            PlayerPrefs.SetInt("UpGold", 1);
            PlayerPrefs.SetInt("UpCh", 0);
        }
    }

    public void attack()
    {
        at = 0;
        int chc = GameObject.Find("chp").transform.childCount;
        for (int i = 0; i < chc; i++)
        {
            at += transform.GetChild(i).GetComponent<MergeItem>().a1;
        }
        //Debug.Log(at);
    }

    public void ch_hp()
    {
        hp = 0;
        int chc = GameObject.Find("chp").transform.childCount;
        for (int i = 0; i < chc; i++)
        {
            hp += transform.GetChild(i).GetComponent<MergeItem>().a2;
        }
        //Debug.Log(hp);
    }

    public void ch_gold()
    {
        gold = 0;
        int chc = GameObject.Find("chp").transform.childCount;
        for (int i = 0; i < chc; i++)
        {
            gold += transform.GetChild(i).GetComponent<MergeItem>().a3;
        }
        //Debug.Log(hp);
    }
}

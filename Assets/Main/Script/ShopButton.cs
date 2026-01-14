using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Merge mg;
    public GameObject textWarning;

    public bool but_1_1, but_1_2, but_1_3, but_1_4, but_2_1, but_2_2, but_2_3, but_2_4, but_3_1, but_3_2, close;

    public AudioSource spawnbgm;

    private void Awake()
    {
        textWarning.SetActive(false);

        if (but_1_2 == true)
        {
            float getGoldTime = PlayerPrefs.GetFloat("GetGoldTime");

            if (getGoldTime <= 1.1f)
                gameObject.GetComponent<Button>().interactable = false;
        }

        if (but_1_4 == true)
        {
            float spawnTime = PlayerPrefs.GetFloat("SpawnTime");

            if (spawnTime <= 1.1f)
                gameObject.GetComponent<Button>().interactable = false;
        }

        if (but_2_3 == true)
        {
            float attr = PlayerPrefs.GetFloat("AttackRate");

            if (attr <= 0.4f)
                gameObject.GetComponent<Button>().interactable = false;
        }

        if (but_2_4 == true)
        {
            float spd = PlayerPrefs.GetFloat("Speed");

            if (spd >= 15f)
                gameObject.GetComponent<Button>().interactable = false;
        }

        int effect_sound = PlayerPrefs.GetInt("EFFECT");

        if (effect_sound == 0)
        {
            spawnbgm.mute = false;
        }

        if (effect_sound == 1)
        {
            spawnbgm.mute = true;
        }
    }

    public void text()
    {
        if (close == true)
        {
            textWarning.SetActive(false);
        }
    }

    public void but_event()
    {
        if (but_1_1 == true)
        {
            int gold = PlayerPrefs.GetInt("Gold");
            int childMax = PlayerPrefs.GetInt("ChildMax");
            float buy = PlayerPrefs.GetInt("Buy_1");

            if (gold >= buy)
            {
                spawnbgm.Play();

                childMax += 1;
                gold -= ((int)buy);
                buy = buy * 1.6f;

                PlayerPrefs.SetInt("Buy_1", ((int)buy));
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetInt("ChildMax", childMax);
            }
            else
                StartCoroutine("textGold");
        }

        if (but_1_2 == true)
        {
            int gold = PlayerPrefs.GetInt("Gold");
            float getGoldTime = PlayerPrefs.GetFloat("GetGoldTime");
            float buy = PlayerPrefs.GetInt("Buy_2");

            if (gold >= buy)
            {
                spawnbgm.Play();

                getGoldTime -= 0.1f;
                gold -= ((int)buy);
                buy = buy * 1.25f;

                PlayerPrefs.SetInt("Buy_2", ((int)buy));
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetFloat("GetGoldTime", getGoldTime);
            }
            else
                StartCoroutine("textGold");

            if (getGoldTime <= 1.1f)
                gameObject.GetComponent<Button>().interactable = false;
        }

        if (but_1_3 == true)
        {
            int gold = PlayerPrefs.GetInt("Gold");
            int clickMax = PlayerPrefs.GetInt("ClickMax");
            float buy = PlayerPrefs.GetInt("Buy_3");

            if (gold >= buy)
            {
                spawnbgm.Play();

                clickMax += 1;
                gold -= ((int)buy);
                buy = buy * 2.4f;

                PlayerPrefs.SetInt("Buy_3", ((int)buy));
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetInt("ClickMax", clickMax);
            }
            else
                StartCoroutine("textGold");
        }

        if (but_1_4 == true)
        {
            int gold = PlayerPrefs.GetInt("Gold");
            float spawnTime = PlayerPrefs.GetFloat("SpawnTime");
            float buy = PlayerPrefs.GetInt("Buy_4");

            if (gold >= buy)
            {
                spawnbgm.Play();

                spawnTime -= 0.1f;
                gold -= ((int)buy);
                buy = buy * 1.35f;

                PlayerPrefs.SetInt("Buy_4", ((int)buy));
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetFloat("SpawnTime", spawnTime);
            }
            else
                StartCoroutine("textGold");

            if (spawnTime <= 1.1f)
                gameObject.GetComponent<Button>().interactable = false;
        }

        if (but_2_1 == true)
        {
            int gamegold = PlayerPrefs.GetInt("GameGold");
            float MHP = PlayerPrefs.GetFloat("MaxHP");
            float buy = PlayerPrefs.GetInt("Buy_5");

            if (gamegold >= buy)
            {
                spawnbgm.Play();

                MHP += 10f;
                gamegold -= ((int)buy);
                buy += 13f;

                PlayerPrefs.SetInt("Buy_5", ((int)buy));
                PlayerPrefs.SetInt("GameGold", gamegold);
                PlayerPrefs.SetFloat("MaxHP", MHP);
            }
            else
                StartCoroutine("textGold");
        }

        if (but_2_2 == true)
        {
            int gamegold = PlayerPrefs.GetInt("GameGold");
            float dmg = PlayerPrefs.GetFloat("Damage");
            float buy = PlayerPrefs.GetInt("Buy_6");

            if (gamegold >= buy)
            {
                spawnbgm.Play();

                dmg += 1f;
                gamegold -= ((int)buy);
                buy += 15f;

                PlayerPrefs.SetInt("Buy_6", ((int)buy));
                PlayerPrefs.SetInt("GameGold", gamegold);
                PlayerPrefs.SetFloat("Damage", dmg);
            }
            else
                StartCoroutine("textGold");
        }

        if (but_2_3 == true)
        {
            int gamegold = PlayerPrefs.GetInt("GameGold");
            float attr = PlayerPrefs.GetFloat("AttackRate");
            float buy = PlayerPrefs.GetInt("Buy_7");

            if (gamegold >= buy)
            {
                spawnbgm.Play();

                attr -= 0.1f;
                gamegold -= ((int)buy);
                buy += 4f;

                PlayerPrefs.SetInt("Buy_7", ((int)buy));
                PlayerPrefs.SetInt("GameGold", gamegold);
                PlayerPrefs.SetFloat("AttackRate", attr);
            }
            else
                StartCoroutine("textGold");

            if (attr <= 0.4f)
                gameObject.GetComponent<Button>().interactable = false;
        }

        if (but_2_4 == true)
        {
            int gamegold = PlayerPrefs.GetInt("GameGold");
            float spd = PlayerPrefs.GetFloat("Speed");
            float buy = PlayerPrefs.GetInt("Buy_8");

            if (gamegold >= buy)
            {
                spawnbgm.Play();

                spd += 0.25f;
                gamegold -= ((int)buy);
                buy += 6f;

                PlayerPrefs.SetInt("Buy_8", ((int)buy));
                PlayerPrefs.SetInt("GameGold", gamegold);
                PlayerPrefs.SetFloat("Speed", spd);
            }
            else
                StartCoroutine("textGold");

            if (spd >= 15f)
                gameObject.GetComponent<Button>().interactable = false;
        }

        if (but_3_1 == true)
        {
            int BossCoin = PlayerPrefs.GetInt("BossCoin");
            int upgold = PlayerPrefs.GetInt("UpGold");
            float buy = PlayerPrefs.GetInt("Buy_9");

            if (BossCoin >= buy)
            {
                spawnbgm.Play();

                upgold *= 2;
                BossCoin -= ((int)buy);
                buy *= 2;

                PlayerPrefs.SetInt("Buy_9", ((int)buy));
                PlayerPrefs.SetInt("BossCoin", BossCoin);
                PlayerPrefs.SetInt("UpGold", upgold);
            }
            else
                StartCoroutine("textGold");
        }

        if (but_3_2 == true)
        {
            int BossCoin = PlayerPrefs.GetInt("BossCoin");
            int UpCh = PlayerPrefs.GetInt("UpCh");
            float buy = PlayerPrefs.GetInt("Buy_10");

            if (BossCoin >= buy)
            {
                spawnbgm.Play();

                UpCh += 1;
                BossCoin -= ((int)buy);
                buy *= 2;

                PlayerPrefs.SetInt("Buy_10", ((int)buy));
                PlayerPrefs.SetInt("BossCoin", BossCoin);
                PlayerPrefs.SetInt("UpCh", UpCh);
                PlayerPrefs.SetInt("Count", UpCh);
            }
            else
                StartCoroutine("textGold");
        }
    }

    private IEnumerator textGold()
    {
        textWarning.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        textWarning.SetActive(false);
    }
}

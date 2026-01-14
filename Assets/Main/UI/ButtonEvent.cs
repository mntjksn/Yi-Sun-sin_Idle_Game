using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public bool game1, game2, game3, etc;

    private void Update()
    {
        int gold = PlayerPrefs.GetInt("Gold");
        int bossticket = PlayerPrefs.GetInt("BossTicket");

        if (game1 == true)
        {
            if (gold >= 100)
                gameObject.GetComponent<Button>().interactable = true;
            else
                gameObject.GetComponent<Button>().interactable = false;
        }

        if (game2 == true)
        {
            if (gold >= 200)
                gameObject.GetComponent<Button>().interactable = true;
            else
                gameObject.GetComponent<Button>().interactable = false;
        }

        if (game3 == true)
        {
            if (bossticket >= 1)
                gameObject.GetComponent<Button>().interactable = true;
            else
                gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void SceneLoader(string sceneName)
    {
        int gold = PlayerPrefs.GetInt("Gold");
        int bossticket = PlayerPrefs.GetInt("BossTicket");

        if (gold >= 100 && game1 == true)
        {
            gold -= (100);

            PlayerPrefs.SetInt("Gold", gold);

            int x = SceneManager.GetActiveScene().buildIndex;
            int chc = GameObject.Find("chp").transform.childCount;

            SceneManager.LoadScene(sceneName);

            Time.timeScale = 1f;

            if (x == 2 || x == 3 || x == 4)
            {
                for (int i = 0; i < chc; i++)
                {
                    GameObject.Find("chp").transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < chc; i++)
                {
                    GameObject.Find("chp").transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (gold >= 200 && game2 == true)
        {
            gold -= (200);

            PlayerPrefs.SetInt("Gold", gold);

            int x = SceneManager.GetActiveScene().buildIndex;
            int chc = GameObject.Find("chp").transform.childCount;

            SceneManager.LoadScene(sceneName);

            Time.timeScale = 1f;

            if (x == 2 || x == 3 || x == 4)
            {
                for (int i = 0; i < chc; i++)
                {
                    GameObject.Find("chp").transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < chc; i++)
                {
                    GameObject.Find("chp").transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (bossticket >= 1 && game3 == true)
        {
            bossticket -= 1;

            PlayerPrefs.SetInt("BossTicket", bossticket);

            int x = SceneManager.GetActiveScene().buildIndex;
            int chc = GameObject.Find("chp").transform.childCount;

            SceneManager.LoadScene(sceneName);

            Time.timeScale = 1f;

            if (x == 2 || x == 3 || x == 4)
            {
                for (int i = 0; i < chc; i++)
                {
                    GameObject.Find("chp").transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < chc; i++)
                {
                    GameObject.Find("chp").transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (etc == true)
        {
            int x = SceneManager.GetActiveScene().buildIndex;
            int chc = GameObject.Find("chp").transform.childCount;

            SceneManager.LoadScene(sceneName);

            Time.timeScale = 1f;

            if (x == 2 || x == 3 || x == 4)
            {
                for (int i = 0; i < chc; i++)
                {
                    GameObject.Find("chp").transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < chc; i++)
                {
                    GameObject.Find("chp").transform.GetChild(i).gameObject.SetActive(false);
                }
            }

        }
    }
}

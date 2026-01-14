using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartEventBut : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        Time.timeScale = 0;
    }


    public void gamestart()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}

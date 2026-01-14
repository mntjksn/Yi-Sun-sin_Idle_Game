using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timer;
    private float time;

    public GameObject panel;
    private Game1PlayerController Game1PlayerController;

    private void Awake()
    {
        timer = GetComponent<TextMeshProUGUI>();
        Game1PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Game1PlayerController>();

        time = 60f;
    }

    private void Update()
    {
        timer.text = "남은 시간 : " + string.Format("{0:N2}", time) + " 초";

        if (time > 0)
        {
            time -= Time.deltaTime;

            if (time < 0)
            {
                panel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}

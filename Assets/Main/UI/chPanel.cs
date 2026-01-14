using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chPanel : MonoBehaviour
{
    public GameObject panel;

    public void Ch_Panel()
    {
        panel.SetActive(false);
        Destroy(panel);
        Time.timeScale = 1f;
    }
}

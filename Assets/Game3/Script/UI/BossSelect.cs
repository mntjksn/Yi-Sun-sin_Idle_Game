using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSelect : MonoBehaviour
{
    public GameObject boss1;
    public GameObject boss2;
    public GameObject panel;
    public bool boss1s;
    public bool boss2s;

    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 0f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void boss1select()
    {
        panel.SetActive(false);
        boss1.SetActive(true);
        boss1s = true;
        Time.timeScale = 1;
    }
    public void boss2select()
    {
        panel.SetActive(false);
        boss2.SetActive(true);
        boss2s = true;
        Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save1 : MonoBehaviour
{
    public DataManager dm;
    bool m_pause;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        if(GameObject.Find("chp").transform.childCount <= 0)
        {
            dm.LoadGameData();
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            dm.SaveGameData();
            m_pause = true;
        }
        else
        {
            if (m_pause)
            {
                m_pause = false;
            }
        }
        //dm.SaveGameData();
        //m_pause = pause;
    }

    private void OnApplicationQuit()
    {
        dm.SaveGameData();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dm.SaveGameData();
            Application.Quit();
        }
    }
}

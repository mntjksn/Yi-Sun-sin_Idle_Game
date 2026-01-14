using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelEvent : MonoBehaviour
{
    public GameObject panel;
    public Game2Data dm;
    public DataManager dm1;
    public void OnPanel()
    {
        panel.SetActive(true);
        dm.Game2save();
        dm1.SaveGameData();
    }

    public void OffPanel()
    {
        panel.SetActive(false);
    }
}

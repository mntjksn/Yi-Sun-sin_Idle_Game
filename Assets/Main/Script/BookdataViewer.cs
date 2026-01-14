using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookdataViewer : MonoBehaviour
{
    public Merge mg;
    private TextMeshProUGUI data;

    public int booknum;

    private void Update()
    {
        data = GetComponent<TextMeshProUGUI>();

        if (GameObject.Find("ItemData").transform.GetComponent<Merge>().itemdata[booknum].spawncheck == true)
        {
            data.text = mg.itemdata[booknum].name.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnText : MonoBehaviour
{
    private TextMeshProUGUI maxSpawn;
    public Merge mg;
    // Start is called before the first frame update
    void Start()
    {
        maxSpawn = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int childMax = PlayerPrefs.GetInt("ChildMax");

        maxSpawn.text = " : " + GameObject.Find("chp").transform.childCount + "/" + childMax.ToString();
    }
}

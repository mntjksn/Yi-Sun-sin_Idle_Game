using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Progress;
using System.IO;
using System.Linq;

[System.Serializable]
public class Item
{
    public string name;
    public int itemNum;
    public Sprite itemimg;
    public int itemgold;
    public float attack;
    public float hp;
    public GameObject panel = null;
    public bool spawncheck;
}

public class Merge : MonoBehaviour
{
    public List<Item> itemdata = new List<Item>();
    public GameObject itemPrefabs;
    public int ListMax;
    //public int childMax = 5;
    public int childCount;
    public chbool cb;

    public Vector3 objPosition1;
    public Vector3 mousePosition;

    public chData data = new chData();

    public AudioSource spawnbgm;
    public AudioSource panelbgm;

    private void Awake()
    {
        var objs = FindObjectsOfType<DataManager>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        string filePath = Path.Combine(Application.dataPath, "chData.json");
        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<chData>(FromJsonData);
            for (int q = 0; q < data.itemNum2.Count; q++)
            {
                GameObject.Find("ItemData").transform.GetComponent<Merge>().itemdata[data.itemNum2[q]].spawncheck = true;
            }
            Debug.Log("불러오기 완료.");
        }
    }

    private void Update()
    {
        int effect_sound = PlayerPrefs.GetInt("EFFECT");

        if (effect_sound == 0)
        {
            spawnbgm.mute = false;
            panelbgm.mute = false;
        }

        if (effect_sound == 1)
        {
            spawnbgm.mute = true;
            panelbgm.mute = true;
        }
    }

    public void itemCreate(int num)
    {
        int childMax = PlayerPrefs.GetInt("ChildMax");
        int UpCh = PlayerPrefs.GetInt("Count");

        if (num == UpCh && GameObject.Find("chp").transform.childCount < childMax && cb.save == false)
        {
            spawnbgm.Play();

            float RandomX = Random.Range(-2.2f, 2.2f);
            float RandomY = Random.Range(-3.5f, 2);

            Vector3 RandomPos = new Vector3(RandomX, RandomY, 0);

            GameObject go = Instantiate(itemPrefabs, RandomPos, Quaternion.identity);
            go.GetComponent<MergeItem>().InitItem(itemdata[num]);

            Invoke("attacko",0.2f);
            Invoke("hpo", 0.2f);
            Invoke("goldo", 0.2f);
        }

        else if (num < ListMax && num != UpCh && cb.save == false)
        {
            spawnbgm.Play();

            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;

            GameObject go = Instantiate(itemPrefabs, transform.position, Quaternion.identity);
            go.GetComponent<MergeItem>().InitItem(itemdata[num]);

            Invoke("attacko", 0.2f);
            Invoke("hpo", 0.2f);
            Invoke("goldo", 0.2f);
        }

        if (cb.save == true)
        {
            GameObject go = Instantiate(itemPrefabs, objPosition1, Quaternion.identity);
            go.GetComponent<MergeItem>().InitItem(itemdata[num]);
            cb.save = false;
        }

        //itemdata[num].spawncheck = true;

        if (itemdata[num].spawncheck == false)
        {
            panelbgm.Play();
            Instantiate(itemdata[num].panel, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
            itemdata[num].spawncheck = true;
        }
    }

    void attacko()
    {
        GameObject.Find("chp").GetComponent<chparent>().attack();
    }

    void hpo()
    {
        GameObject.Find("chp").GetComponent<chparent>().ch_hp();
    }

    void goldo()
    {
        GameObject.Find("chp").GetComponent<chparent>().ch_gold();
    }
}

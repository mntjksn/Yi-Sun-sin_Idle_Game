using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Itemimage
{
    public int itemNum;
    public Sprite itemimg;
    public int hp;
    public int atk;
    public float cooltime;
}
public class chnumdata : MonoBehaviour
{
    public List<Itemimage> itemdata = new List<Itemimage>();
    public GameObject chPrefab;
    public Vector3 chposition;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Save").GetComponent<Game2Data>().Game2load();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void spawn(int num)
    {
        GameObject chp = Instantiate(chPrefab, chposition, Quaternion.identity);
        chp.GetComponent<Spawn>().InitItem(itemdata[num]);
    }
    public void clickspawn(int num)
    {
        GameObject chp = Instantiate(chPrefab, GameObject.FindGameObjectWithTag("player").GetComponent<Spawn>().chpos, Quaternion.identity);
        chp.GetComponent<Spawn>().InitItem(itemdata[num]);
    }
}

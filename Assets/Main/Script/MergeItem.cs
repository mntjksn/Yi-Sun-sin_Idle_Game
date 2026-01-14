using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MergeItem : MonoBehaviour
{
    SpriteRenderer sr;
    Item item;
    bool isSelect;
    GameObject contactItem;
    Animator animator;
    public GameObject chpa;
    private int chgold;
    private Vector3 myPos;

    public int iN;
    public bool SC;
    public GameObject GoldImage;
    public float a1, a2, a3;

    private int gold;

    public int Gold
    {
        set => gold = Mathf.Max(0, value);
        get => gold;
    }

    private float getGoldTime = 5.0f;

    public float GetGoldTime
    {
        set => getGoldTime = Mathf.Max(0, value);
        get => getGoldTime;
    }

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        
        animator = GetComponent<Animator>();
        chpa = GameObject.FindGameObjectWithTag("chp");
        gameObject.transform.parent = chpa.transform;
        myPos = gameObject.transform.position;

        //StartCoroutine("test");

    }
    private void OnEnable()
    {
        StartCoroutine("test");
    }
    private IEnumerator test()
    {
        while (true)
        {
            float getGoldTime = PlayerPrefs.GetFloat("GetGoldTime");

            int x = SceneManager.GetActiveScene().buildIndex;
            if (x == 1)
            {
                Gold = PlayerPrefs.GetInt("Gold");
                Vector3 pos = new Vector3(myPos.x, myPos.y + 0.35f, myPos.z);
                GameObject goldimage = Instantiate(GoldImage, pos, Quaternion.identity);
                Destroy(goldimage, 0.125f);

                int upgold = PlayerPrefs.GetInt("UpGold");

                Gold += chgold * upgold;
                PlayerPrefs.SetInt("Gold", gold);

                Debug.Log(getGoldTime);
            }
            else
            {
                Gold = PlayerPrefs.GetInt("Gold");

                int upgold = PlayerPrefs.GetInt("UpGold");

                Gold += chgold * upgold;
                PlayerPrefs.SetInt("Gold", gold);
            }
            yield return new WaitForSeconds(getGoldTime);
        }
    }

    public void InitItem(Item i)
    {
        item = i;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.itemimg;
        chgold = item.itemgold;
    }

    private void OnMouseDown()
    {
        isSelect = true;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    private void OnMouseUp()
    {
        isSelect = false;
        if (contactItem != null)
        {
            Destroy(contactItem);
            Destroy(gameObject);
            GameObject.Find("ItemData").GetComponent<Merge>().itemCreate(item.itemNum + 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isSelect && item.itemNum == collision.GetComponent<MergeItem>().item.itemNum)
        {
            if (contactItem != null)
            {
                contactItem = null;
            }

            contactItem = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (item.itemNum == collision.GetComponent<MergeItem>().item.itemNum)
        {
            contactItem = null;
        }
    }

    private void Update()
    {
        iN = item.itemNum;
        SC = item.spawncheck;
        animator.SetInteger("chnum", item.itemNum);

        a1 = item.attack;
        a2 = item.hp;
        a3 = item.itemgold;
        //Debug.Log(iN);

        myPos = gameObject.transform.position;
    }
}
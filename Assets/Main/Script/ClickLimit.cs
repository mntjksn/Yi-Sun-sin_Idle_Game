using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ClickLimit : MonoBehaviour
{
    
    public Button btn;
    public Image image;
    public Merge mg;

    private float imageTime;
    private float timeLimit;

    private int clickNum = 5;



    public int ClickNum
    {
        set => clickNum = Mathf.Max(0, value);
        get => clickNum;
    }

    private float spawnTime;
    public float SpawnTime
    {
        set => spawnTime = Mathf.Max(0, value);
        get => spawnTime;
    }

    private int clickMax;
    public int ClickMax
    {
        set => clickMax = Mathf.Max(0, value);
        get => clickMax;
    }

    int upch;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        int resetcount = PlayerPrefs.GetInt("UpCh");
        PlayerPrefs.SetInt("Count", resetcount);

        mg = GameObject.Find("ItemData").GetComponent<Merge>();
        
        btn.onClick.AddListener(() => mg.itemCreate(upch));
        
        ClickNum = PlayerPrefs.GetInt("ClickNum");
    }
    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        
        int childMax = PlayerPrefs.GetInt("ChildMax");
        int cm = PlayerPrefs.GetInt("ClickMax");
        float st = PlayerPrefs.GetFloat("SpawnTime");
        upch = PlayerPrefs.GetInt("Count");

        if (cm > ClickNum && image.fillAmount > 0.0f)
        {

            timeLimit += Time.deltaTime;

            if (timeLimit > st)
            {
                ClickPlus();
                gameObject.GetComponent<Button>().interactable = true;
                timeLimit = 0f;

            }

            if (image.fillAmount > 0.0f)
            {
                imageTime = Time.deltaTime;
                float time = imageTime / st;
                image.fillAmount -= time;

                if (image.fillAmount == 0.0f)
                {
                    image.fillAmount = 1;
                }
            }
        }

        if (timeLimit > st && ClickMax == ClickNum)
        {
            timeLimit = 0f;
        }


        if (ClickNum == 0 || childMax == GameObject.Find("chp").transform.childCount)
            gameObject.GetComponent<Button>().interactable = false;
        else if (ClickNum != 0)
            gameObject.GetComponent<Button>().interactable = true;
        else
            gameObject.GetComponent<Button>().interactable = true;
    }
    
    public void Click()
    {
        
        imageTime = 0f;

        ClickNum = PlayerPrefs.GetInt("ClickNum");
        ClickNum -= 1;
        PlayerPrefs.SetInt("ClickNum", clickNum);

    }

    public void ClickPlus()
    {
        ClickNum = PlayerPrefs.GetInt("ClickNum");
        ClickNum += 1;
        PlayerPrefs.SetInt("ClickNum", clickNum);
        image.fillAmount = 1;
    }
}

using UnityEngine;
using TMPro;

public class ShopBuyGoldViewer : MonoBehaviour
{
    // 가격 표시 텍스트
    private TextMeshProUGUI goldViewer;

    // 어떤 버튼 가격을 표시할지 구분하는 플래그
    public bool but_1;
    public bool but_2;
    public bool but_3;
    public bool but_4;
    public bool but_2_1;
    public bool but_2_2;
    public bool but_2_3;
    public bool but_2_4;
    public bool but_3_1;
    public bool but_3_2;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        goldViewer = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // 1 1 가격 표시
        if (but_1 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_1");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 골드";
        }

        // 1 2 가격 표시
        if (but_2 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_2");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 골드";

            float getGoldTime = PlayerPrefs.GetFloat("GetGoldTime");
            if (getGoldTime <= 1.1f)
            {
                goldViewer.text = "MAX";
            }
        }

        // 1 3 가격 표시
        if (but_3 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_3");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 골드";
        }

        // 1 4 가격 표시
        if (but_4 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_4");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 골드";

            float spawnTime = PlayerPrefs.GetFloat("SpawnTime");
            if (spawnTime <= 1.1f)
            {
                goldViewer.text = "MAX";
            }
        }

        // 2 1 가격 표시
        if (but_2_1 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_5");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 엽전";
        }

        // 2 2 가격 표시
        if (but_2_2 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_6");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 엽전";
        }

        // 2 3 가격 표시
        if (but_2_3 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_7");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 엽전";

            float attackRate = PlayerPrefs.GetFloat("AttackRate");
            if (attackRate <= 0.4f)
            {
                goldViewer.text = "MAX";
            }
        }

        // 2 4 가격 표시
        if (but_2_4 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_8");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 엽전";

            float speed = PlayerPrefs.GetFloat("Speed");
            if (speed >= 15f)
            {
                goldViewer.text = "MAX";
            }
        }

        // 3 1 가격 표시
        if (but_3_1 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_9");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 진주";
        }

        // 3 2 가격 표시
        if (but_3_2 == true)
        {
            int gold = PlayerPrefs.GetInt("Buy_10");
            goldViewer.text = string.Format("{0:#,0}", gold) + " 진주";
        }
    }
}
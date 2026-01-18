using UnityEngine;
using TMPro;

public class DataViewer : MonoBehaviour
{
    // 어떤 정보를 보여줄지 선택하는 플래그
    public bool at;
    public bool hp;
    public bool attr;
    public bool spd;
    public bool hp_add;
    public bool at_add;
    public bool gold;

    // 표시 텍스트
    private TextMeshProUGUI text;

    // 닫을 패널
    public GameObject panel;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // 기본 체력 표시
        if (hp == true)
        {
            float mhp = PlayerPrefs.GetFloat("MaxHP");
            text.text = "기본 체력 : " + mhp + " HP";
        }

        // 추가 선원 체력 합 표시
        if (hp_add == true)
        {
            float addHp = GameObject.Find("chp").GetComponent<chparent>().hp;
            text.text = "(추가 선원 당 체력 + " + addHp + " HP)";
        }

        // 기본 공격력 표시
        if (at == true)
        {
            float dmg = PlayerPrefs.GetFloat("Damage");
            text.text = "기본 공격력 : " + dmg + " ATT)";
        }

        // 추가 선원 공격력 합 표시
        if (at_add == true)
        {
            float addAt = GameObject.Find("chp").GetComponent<chparent>().at;
            text.text = "(추가 선원 당 공격력 + " + addAt + " ATT)";
        }

        // 기본 공격속도 표시
        if (attr == true)
        {
            float attackRate = PlayerPrefs.GetFloat("AttackRate");
            text.text = "기본 공격속도 : " + attackRate.ToString("F1") + " ATTR";
        }

        // 기본 이동속도 표시
        if (spd == true)
        {
            float speed = PlayerPrefs.GetFloat("Speed");
            text.text = "기본 이동속도 : " + speed.ToString("F2") + " SPD";
        }

        // 골드 획득량 표시
        if (gold == true)
        {
            float goldValue = GameObject.Find("chp").GetComponent<chparent>().gold;
            int upgold = PlayerPrefs.GetInt("UpGold");

            text.text = "총 골드 획득량 : " + string.Format("{0:#,0}", goldValue * upgold);
        }
    }

    public void OffPanel()
    {
        // 패널 닫기
        panel.SetActive(false);
    }
}
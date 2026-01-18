using UnityEngine;
using TMPro;

public class ShopdataViewer : MonoBehaviour
{
    // 업그레이드 설명 표시 텍스트
    private TextMeshProUGUI data;

    // 어떤 업그레이드 설명을 표시할지 구분하는 플래그
    public bool but_1_1;
    public bool but_1_2;
    public bool but_1_3;
    public bool but_1_4;
    public bool but_2_1;
    public bool but_2_2;
    public bool but_2_3;
    public bool but_2_4;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        data = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // 1 1 최대 소환수 증가
        if (but_1_1 == true)
        {
            int childMax = PlayerPrefs.GetInt("ChildMax");
            int nextChildMax = childMax + 1;

            data.text = childMax + "명" + " 에서 " + nextChildMax + "명";
        }

        // 1 2 골드 획득 주기 감소
        if (but_1_2 == true)
        {
            float getGoldTime = PlayerPrefs.GetFloat("GetGoldTime");
            float nextGetGoldTime = getGoldTime - 0.1f;

            data.text = getGoldTime.ToString("F1") + "초" + " 에서 " + nextGetGoldTime.ToString("F1") + "초";

            if (getGoldTime <= 1.1f)
            {
                data.text = "UPGRAGE MAX";
            }
        }

        // 1 3 클릭 최대치 증가
        if (but_1_3 == true)
        {
            int clickMax = PlayerPrefs.GetInt("ClickMax");
            int nextClickMax = clickMax + 1;

            data.text = clickMax + "명" + " 에서 " + nextClickMax + "명";
        }

        // 1 4 생성 쿨타임 감소
        if (but_1_4 == true)
        {
            float spawnTime = PlayerPrefs.GetFloat("SpawnTime");
            float nextSpawnTime = spawnTime - 0.1f;

            data.text = spawnTime.ToString("F1") + "초" + " 에서 " + nextSpawnTime.ToString("F1") + "초";

            if (spawnTime <= 1.1f)
            {
                data.text = "UPGRAGE MAX";
            }
        }

        // 2 1 기본 체력 증가
        if (but_2_1 == true)
        {
            float mhp = PlayerPrefs.GetFloat("MaxHP");
            float nextMhp = mhp + 10f;

            data.text = mhp.ToString("F0") + " HP" + " 에서 " + nextMhp.ToString("F0") + " HP";
        }

        // 2 2 기본 공격력 증가
        if (but_2_2 == true)
        {
            float dmg = PlayerPrefs.GetFloat("Damage");
            float nextDmg = dmg + 1f;

            data.text = dmg.ToString("F0") + " ATT" + " 에서 " + nextDmg.ToString("F0") + " ATT";
        }

        // 2 3 공격속도 감소
        if (but_2_3 == true)
        {
            float attackRate = PlayerPrefs.GetFloat("AttackRate");
            float nextAttackRate = attackRate - 0.1f;

            data.text = attackRate.ToString("F1") + " ATTR" + " 에서 " + nextAttackRate.ToString("F1") + " ATTR";

            if (attackRate <= 0.4f)
            {
                data.text = "UPGRAGE MAX";
            }
        }

        // 2 4 이동속도 증가
        if (but_2_4 == true)
        {
            float speed = PlayerPrefs.GetFloat("Speed");
            float nextSpeed = speed + 0.25f;

            data.text = speed.ToString("F2") + " SPD" + " 에서 " + nextSpeed.ToString("F2") + " SPD";

            if (speed >= 15f)
            {
                data.text = "UPGRAGE MAX";
            }
        }
    }
}
using UnityEngine;

public class chparent : MonoBehaviour
{
    // 데이터 매니저 참조
    public DataManager dm;

    // 아이템 데이터
    private Item item;

    // 전체 공격력 합
    public float at;

    // 전체 체력 합
    public float hp;

    // 골드 보너스 합
    public float gold;

    private void Awake()
    {
        // 중복 생성 방지 처리
        chparent[] objs = FindObjectsOfType<chparent>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 스탯 계산 지연 호출
        Invoke("attack", 0.2f);
        Invoke("ch_hp", 0.2f);
        Invoke("ch_gold", 0.2f);

        // 저장된 업그레이드 값 불러오기
        int g1 = PlayerPrefs.GetInt("Buy_1");
        int g2 = PlayerPrefs.GetInt("Buy_2");
        int g3 = PlayerPrefs.GetInt("Buy_3");
        int g4 = PlayerPrefs.GetInt("Buy_4");
        int g5 = PlayerPrefs.GetInt("Buy_5");
        int g6 = PlayerPrefs.GetInt("Buy_6");
        int g7 = PlayerPrefs.GetInt("Buy_7");
        int g8 = PlayerPrefs.GetInt("Buy_8");
        int g9 = PlayerPrefs.GetInt("Buy_9");
        int g10 = PlayerPrefs.GetInt("Buy_10");

        int cm = PlayerPrefs.GetInt("ChildMax");
        int cn = PlayerPrefs.GetInt("ClickMax");

        float gt = PlayerPrefs.GetFloat("GetGoldTime");
        float st = PlayerPrefs.GetFloat("SpawnTime");

        float mhp = PlayerPrefs.GetFloat("MaxHP");
        float dmg = PlayerPrefs.GetFloat("Damage");
        float attr = PlayerPrefs.GetFloat("AttackRate");
        float spd = PlayerPrefs.GetFloat("Speed");

        // 기본 데이터가 없는 경우 초기값 설정
        if (g1 == 0 || g2 == 0 || g3 == 0 || g4 == 0 ||
            g5 == 0 || g6 == 0 || g7 == 0 || g8 == 0 ||
            cm == 0 || cn == 0 || gt == 0 || st == 0 ||
            mhp == 0 || dmg == 0 || attr == 0 || spd == 0)
        {
            PlayerPrefs.SetInt("Buy_1", 374);
            PlayerPrefs.SetInt("Buy_2", 241);
            PlayerPrefs.SetInt("Buy_3", 421);
            PlayerPrefs.SetInt("Buy_4", 324);

            PlayerPrefs.SetInt("Buy_5", 1);
            PlayerPrefs.SetInt("Buy_6", 1);
            PlayerPrefs.SetInt("Buy_7", 1);
            PlayerPrefs.SetInt("Buy_8", 1);

            PlayerPrefs.SetInt("ChildMax", 5);
            PlayerPrefs.SetInt("ClickMax", 5);
            PlayerPrefs.SetInt("ClickNum", 1);

            PlayerPrefs.SetFloat("GetGoldTime", 5f);
            PlayerPrefs.SetFloat("SpawnTime", 5f);

            PlayerPrefs.SetFloat("MaxHP", 10f);
            PlayerPrefs.SetFloat("Damage", 1f);
            PlayerPrefs.SetFloat("AttackRate", 3f);
            PlayerPrefs.SetFloat("Speed", 3f);
        }

        // 추가 업그레이드 기본값 설정
        if (g9 == 0 || g10 == 0)
        {
            PlayerPrefs.SetInt("Buy_9", 10);
            PlayerPrefs.SetInt("Buy_10", 20);

            PlayerPrefs.SetInt("UpGold", 1);
            PlayerPrefs.SetInt("UpCh", 0);
        }
    }

    public void attack()
    {
        // 자식 캐릭터 공격력 합산
        at = 0f;
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            at += transform.GetChild(i).GetComponent<MergeItem>().a1;
        }
    }

    public void ch_hp()
    {
        // 자식 캐릭터 체력 합산
        hp = 0f;
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            hp += transform.GetChild(i).GetComponent<MergeItem>().a2;
        }
    }

    public void ch_gold()
    {
        // 자식 캐릭터 골드 보너스 합산
        gold = 0f;
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            gold += transform.GetChild(i).GetComponent<MergeItem>().a3;
        }
    }
}
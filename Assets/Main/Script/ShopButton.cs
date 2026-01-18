using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    // 아이템 생성 관리
    public Merge mg;

    // 골드 부족 경고 텍스트
    public GameObject textWarning;

    // 어떤 버튼인지 구분하는 플래그
    public bool but_1_1;
    public bool but_1_2;
    public bool but_1_3;
    public bool but_1_4;
    public bool but_2_1;
    public bool but_2_2;
    public bool but_2_3;
    public bool but_2_4;
    public bool but_3_1;
    public bool but_3_2;
    public bool close;

    // 버튼 사운드
    public AudioSource spawnbgm;

    private void Awake()
    {
        // 경고 텍스트 초기 비활성화
        textWarning.SetActive(false);

        // 버튼 상한 도달 시 비활성화 처리
        if (but_1_2 == true)
        {
            float getGoldTime = PlayerPrefs.GetFloat("GetGoldTime");
            if (getGoldTime <= 1.1f) GetComponent<Button>().interactable = false;
        }

        if (but_1_4 == true)
        {
            float spawnTime = PlayerPrefs.GetFloat("SpawnTime");
            if (spawnTime <= 1.1f) GetComponent<Button>().interactable = false;
        }

        if (but_2_3 == true)
        {
            float attackRate = PlayerPrefs.GetFloat("AttackRate");
            if (attackRate <= 0.4f) GetComponent<Button>().interactable = false;
        }

        if (but_2_4 == true)
        {
            float speed = PlayerPrefs.GetFloat("Speed");
            if (speed >= 15f) GetComponent<Button>().interactable = false;
        }

        // 효과음 설정 적용
        int effectSound = PlayerPrefs.GetInt("EFFECT");
        if (effectSound == 0) spawnbgm.mute = false;
        if (effectSound == 1) spawnbgm.mute = true;
    }

    public void text()
    {
        // 닫기 용도일 때 경고 텍스트 숨김
        if (close == true)
        {
            textWarning.SetActive(false);
        }
    }

    public void but_event()
    {
        // 1 1 최대 소환수 증가
        if (but_1_1 == true)
        {
            int gold = PlayerPrefs.GetInt("Gold");
            int childMax = PlayerPrefs.GetInt("ChildMax");
            float buy = PlayerPrefs.GetInt("Buy_1");

            if (gold >= buy)
            {
                spawnbgm.Play();

                childMax += 1;
                gold -= (int)buy;
                buy = buy * 1.6f;

                PlayerPrefs.SetInt("Buy_1", (int)buy);
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetInt("ChildMax", childMax);
            }
            else
            {
                StartCoroutine("textGold");
            }
        }

        // 1 2 골드 획득 주기 감소
        if (but_1_2 == true)
        {
            int gold = PlayerPrefs.GetInt("Gold");
            float getGoldTime = PlayerPrefs.GetFloat("GetGoldTime");
            float buy = PlayerPrefs.GetInt("Buy_2");

            if (gold >= buy)
            {
                spawnbgm.Play();

                getGoldTime -= 0.1f;
                gold -= (int)buy;
                buy = buy * 1.25f;

                PlayerPrefs.SetInt("Buy_2", (int)buy);
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetFloat("GetGoldTime", getGoldTime);
            }
            else
            {
                StartCoroutine("textGold");
            }

            if (getGoldTime <= 1.1f) GetComponent<Button>().interactable = false;
        }

        // 1 3 클릭 최대치 증가
        if (but_1_3 == true)
        {
            int gold = PlayerPrefs.GetInt("Gold");
            int clickMax = PlayerPrefs.GetInt("ClickMax");
            float buy = PlayerPrefs.GetInt("Buy_3");

            if (gold >= buy)
            {
                spawnbgm.Play();

                clickMax += 1;
                gold -= (int)buy;
                buy = buy * 2.4f;

                PlayerPrefs.SetInt("Buy_3", (int)buy);
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetInt("ClickMax", clickMax);
            }
            else
            {
                StartCoroutine("textGold");
            }
        }

        // 1 4 생성 쿨타임 감소
        if (but_1_4 == true)
        {
            int gold = PlayerPrefs.GetInt("Gold");
            float spawnTime = PlayerPrefs.GetFloat("SpawnTime");
            float buy = PlayerPrefs.GetInt("Buy_4");

            if (gold >= buy)
            {
                spawnbgm.Play();

                spawnTime -= 0.1f;
                gold -= (int)buy;
                buy = buy * 1.35f;

                PlayerPrefs.SetInt("Buy_4", (int)buy);
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.SetFloat("SpawnTime", spawnTime);
            }
            else
            {
                StartCoroutine("textGold");
            }

            if (spawnTime <= 1.1f) GetComponent<Button>().interactable = false;
        }

        // 2 1 기본 체력 증가
        if (but_2_1 == true)
        {
            int gamegold = PlayerPrefs.GetInt("GameGold");
            float maxHP = PlayerPrefs.GetFloat("MaxHP");
            float buy = PlayerPrefs.GetInt("Buy_5");

            if (gamegold >= buy)
            {
                spawnbgm.Play();

                maxHP += 10f;
                gamegold -= (int)buy;
                buy += 13f;

                PlayerPrefs.SetInt("Buy_5", (int)buy);
                PlayerPrefs.SetInt("GameGold", gamegold);
                PlayerPrefs.SetFloat("MaxHP", maxHP);
            }
            else
            {
                StartCoroutine("textGold");
            }
        }

        // 2 2 기본 공격력 증가
        if (but_2_2 == true)
        {
            int gamegold = PlayerPrefs.GetInt("GameGold");
            float damage = PlayerPrefs.GetFloat("Damage");
            float buy = PlayerPrefs.GetInt("Buy_6");

            if (gamegold >= buy)
            {
                spawnbgm.Play();

                damage += 1f;
                gamegold -= (int)buy;
                buy += 15f;

                PlayerPrefs.SetInt("Buy_6", (int)buy);
                PlayerPrefs.SetInt("GameGold", gamegold);
                PlayerPrefs.SetFloat("Damage", damage);
            }
            else
            {
                StartCoroutine("textGold");
            }
        }

        // 2 3 공격속도 감소
        if (but_2_3 == true)
        {
            int gamegold = PlayerPrefs.GetInt("GameGold");
            float attackRate = PlayerPrefs.GetFloat("AttackRate");
            float buy = PlayerPrefs.GetInt("Buy_7");

            if (gamegold >= buy)
            {
                spawnbgm.Play();

                attackRate -= 0.1f;
                gamegold -= (int)buy;
                buy += 4f;

                PlayerPrefs.SetInt("Buy_7", (int)buy);
                PlayerPrefs.SetInt("GameGold", gamegold);
                PlayerPrefs.SetFloat("AttackRate", attackRate);
            }
            else
            {
                StartCoroutine("textGold");
            }

            if (attackRate <= 0.4f) GetComponent<Button>().interactable = false;
        }

        // 2 4 이동속도 증가
        if (but_2_4 == true)
        {
            int gamegold = PlayerPrefs.GetInt("GameGold");
            float speed = PlayerPrefs.GetFloat("Speed");
            float buy = PlayerPrefs.GetInt("Buy_8");

            if (gamegold >= buy)
            {
                spawnbgm.Play();

                speed += 0.25f;
                gamegold -= (int)buy;
                buy += 6f;

                PlayerPrefs.SetInt("Buy_8", (int)buy);
                PlayerPrefs.SetInt("GameGold", gamegold);
                PlayerPrefs.SetFloat("Speed", speed);
            }
            else
            {
                StartCoroutine("textGold");
            }

            if (speed >= 15f) GetComponent<Button>().interactable = false;
        }

        // 3 1 골드 배율 증가
        if (but_3_1 == true)
        {
            int bossCoin = PlayerPrefs.GetInt("BossCoin");
            int upgold = PlayerPrefs.GetInt("UpGold");
            float buy = PlayerPrefs.GetInt("Buy_9");

            if (bossCoin >= buy)
            {
                spawnbgm.Play();

                upgold *= 2;
                bossCoin -= (int)buy;
                buy *= 2;

                PlayerPrefs.SetInt("Buy_9", (int)buy);
                PlayerPrefs.SetInt("BossCoin", bossCoin);
                PlayerPrefs.SetInt("UpGold", upgold);
            }
            else
            {
                StartCoroutine("textGold");
            }
        }

        // 3 2 생성 가능한 캐릭터 단계 증가
        if (but_3_2 == true)
        {
            int bossCoin = PlayerPrefs.GetInt("BossCoin");
            int upCh = PlayerPrefs.GetInt("UpCh");
            float buy = PlayerPrefs.GetInt("Buy_10");

            if (bossCoin >= buy)
            {
                spawnbgm.Play();

                upCh += 1;
                bossCoin -= (int)buy;
                buy *= 2;

                PlayerPrefs.SetInt("Buy_10", (int)buy);
                PlayerPrefs.SetInt("BossCoin", bossCoin);
                PlayerPrefs.SetInt("UpCh", upCh);
                PlayerPrefs.SetInt("Count", upCh);
            }
            else
            {
                StartCoroutine("textGold");
            }
        }
    }

    private IEnumerator textGold()
    {
        // 골드 부족 경고 표시
        textWarning.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        textWarning.SetActive(false);
    }
}
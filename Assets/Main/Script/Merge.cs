using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;     // 캐릭터 이름
    public int itemNum; // 캐릭터 번호
    public Sprite itemimg;  // 캐릭터 이미지
    public int itemgold;    // 캐릭터 골드
    public float attack;    // 공격력
    public float hp;    // 체력
    public GameObject panel = null;  // 처음 획득 시 띄울 패널
    public bool spawncheck; // 도감 해금 여부
}

public class Merge : MonoBehaviour
{
    public List<Item> itemdata = new List<Item>();      // 캐릭터 데이터 목록

    public GameObject itemPrefabs;      // 생성할 캐릭터 프리팹
    public int ListMax;     // 캐릭터 리스트 최대 범위
    public int childCount;      // 현재 자식 수 용도였던 변수

    public chbool cb;       // 불러오기 상태 플래그
    public Vector3 objPosition1;        // 불러오기 때 사용할 생성 위치
    public Vector3 mousePosition;       // 마우스 위치 저장용

    public chData data = new chData();      // 저장 데이터

    // 사운드
    public AudioSource spawnbgm;
    public AudioSource panelbgm;

    private void Awake()
    {
        // 중복 생성 방지 처리
        DataManager[] objs = FindObjectsOfType<DataManager>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 초기 데이터 불러오기
        string filePath = Path.Combine(Application.dataPath, "chData.json");
        if (File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<chData>(fromJsonData);

            // 해금된 아이템 상태 복원
            for (int q = 0; q < data.itemNum2.Count; q++)
            {
                GameObject.Find("ItemData").GetComponent<Merge>().itemdata[data.itemNum2[q]].spawncheck = true;
            }

            Debug.Log("불러오기 완료.");
        }
    }

    private void Update()
    {
        // 효과음 설정 적용
        int effectSound = PlayerPrefs.GetInt("EFFECT");

        if (effectSound == 0)
        {
            spawnbgm.mute = false;
            panelbgm.mute = false;
        }

        if (effectSound == 1)
        {
            spawnbgm.mute = true;
            panelbgm.mute = true;
        }
    }

    public void itemCreate(int num)
    {
        int childMax = PlayerPrefs.GetInt("ChildMax");
        int upCh = PlayerPrefs.GetInt("Count");

        // 소환 버튼 클릭 시 기본 캐릭터 생성
        if (num == upCh && GameObject.Find("chp").transform.childCount < childMax && cb.save == false)
        {
            spawnbgm.Play();

            float randomX = Random.Range(-2.2f, 2.2f);
            float randomY = Random.Range(-3.5f, 2f);
            Vector3 randomPos = new Vector3(randomX, randomY, 0f);

            GameObject go = Instantiate(itemPrefabs, randomPos, Quaternion.identity);
            go.GetComponent<MergeItem>().InitItem(itemdata[num]);

            Invoke("attacko", 0.2f);
            Invoke("hpo", 0.2f);
            Invoke("goldo", 0.2f);
        }

        // 캐릭터 Merge 후 다음 캐릭터 생성
        else if (num < ListMax && num != upCh && cb.save == false)
        {
            spawnbgm.Play();

            // 마우스 위치를 월드 좌표로 변환해서 생성
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            transform.position = worldPos;

            GameObject go = Instantiate(itemPrefabs, transform.position, Quaternion.identity);
            go.GetComponent<MergeItem>().InitItem(itemdata[num]);

            Invoke("attacko", 0.2f);
            Invoke("hpo", 0.2f);
            Invoke("goldo", 0.2f);
        }

        // 저장 복원일 때는 지정된 위치에서 생성
        if (cb.save == true)
        {
            GameObject go = Instantiate(itemPrefabs, objPosition1, Quaternion.identity);
            go.GetComponent<MergeItem>().InitItem(itemdata[num]);
            cb.save = false;
        }

        // 처음 획득한 아이템이면 도감 패널 표시
        if (itemdata[num].spawncheck == false)
        {
            panelbgm.Play();

            Instantiate(
                itemdata[num].panel,
                Vector3.zero,
                Quaternion.identity,
                GameObject.Find("Canvas").transform
            );

            itemdata[num].spawncheck = true;
        }
    }

    private void attacko()
    {
        // 전체 공격력 합산 갱신
        GameObject.Find("chp").GetComponent<chparent>().attack();
    }

    private void hpo()
    {
        // 전체 체력 합산 갱신
        GameObject.Find("chp").GetComponent<chparent>().ch_hp();
    }

    private void goldo()
    {
        // 전체 골드 보너스 합산 갱신
        GameObject.Find("chp").GetComponent<chparent>().ch_gold();
    }
}
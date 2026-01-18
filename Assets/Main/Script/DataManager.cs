using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class chData
{
    // 저장할 캐릭터 아이템 번호 목록
    public List<int> itemNum1 = new List<int>();

    // 저장할 캐릭터 위치 목록
    public List<Vector3> Pos1 = new List<Vector3>();

    // 해금된 아이템 번호 목록
    public List<int> itemNum2 = new List<int>();

    // 사용하지 않지만 구조 유지용
    public List<int> itemNum3 = new List<int>();

    // 저장 당시 캐릭터 수
    public int chCount;

    // 저장 여부
    public bool save;
}

public class DataManager : MonoBehaviour
{
    // 저장 데이터
    public chData data = new chData();

    // 아이템 프리팹
    public GameObject itemPrefabs;

    // 캐릭터 정보 컴포넌트
    public MergeItem mg;

    // 아이템 생성 관리
    public Merge mg1;

    // 불러오기 상태 플래그
    public chbool cb;

    private void Start()
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

        // MergeItem 참조 정리
        mg = mg.GetComponent<MergeItem>();
    }

    public void LoadGameData()
    {
        // 저장 파일 경로
        string filePath = Path.Combine(Application.persistentDataPath, "chData.json");
        print(filePath);

        // 저장된 파일이 없으면 종료
        if (!File.Exists(filePath)) return;

        // 저장된 파일 읽고 Json을 데이터 클래스로 변환
        string fromJsonData = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<chData>(fromJsonData);

        // 해금된 아이템 복원
        for (int q = 0; q < data.itemNum2.Count; q++)
        {
            GameObject.Find("ItemData").GetComponent<Merge>().itemdata[data.itemNum2[q]].spawncheck = true;
        }

        // 캐릭터 복원
        for (int q = 0; q < data.itemNum1.Count; q++)
        {
            cb.save = true;

            // 생성 위치를 지정하고 생성 호출
            mg1.objPosition1 = data.Pos1[q];
            mg1.itemCreate(data.itemNum1[q]);
        }

        print("불러오기 완료");
    }

    public void SaveGameData()
    {
        // 현재 캐릭터 수
        int chc = GameObject.Find("chp").transform.childCount;

        // 저장 리스트 초기화
        data.itemNum1.Clear();
        data.itemNum2.Clear();
        data.Pos1.Clear();

        // 캐릭터 아이템 번호 저장
        for (int q = 0; q < chc; q++)
        {
            int itemNum = GameObject.Find("chp").transform.GetChild(q).GetComponent<MergeItem>().iN;
            data.itemNum1.Add(itemNum);
        }

        // 캐릭터 위치 저장
        for (int q = 0; q < chc; q++)
        {
            Vector3 pos = GameObject.Find("chp").transform.GetChild(q).transform.position;
            data.Pos1.Add(pos);
        }

        // 해금된 아이템 저장
        Merge merge = GameObject.Find("ItemData").GetComponent<Merge>();
        for (int q = 0; q < merge.itemdata.Count; q++)
        {
            if (merge.itemdata[q].spawncheck)
            {
                data.itemNum2.Add(merge.itemdata[q].itemNum);
            }
        }

        // 저장 정보 갱신
        data.chCount = chc;
        data.save = true;

        // Json으로 변환 후 저장
        string toJsonData = JsonUtility.ToJson(data, true);
        string filePath = Path.Combine(Application.persistentDataPath, "chData.json");
        File.WriteAllText(filePath, toJsonData);

        print("저장 완료");
    }
}
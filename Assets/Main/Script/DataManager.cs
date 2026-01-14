using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

[System.Serializable]
public class chData
{
    public List<int> itemNum1 = new List<int>();
    public List<Vector3> Pos1 = new List<Vector3>();
    public List<int> itemNum2 = new List<int>();
    public List<int> itemNum3 = new List<int>();
    public int chCount;
    public bool save;
}

public class DataManager : MonoBehaviour
{
    public GameObject itemPrefabs;
    public MergeItem mg;
    public Merge mg1;
    public chbool cb;
    public chData data = new chData();


    private void Start()
    {
        var objs = FindObjectsOfType<DataManager>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        mg = mg.GetComponent<MergeItem>();
    }

    // 불러오기
    public void LoadGameData()
    {
        int chc = GameObject.Find("chp").transform.childCount;
        string filePath = Path.Combine(Application.persistentDataPath, "chData.json");
        print(filePath);
        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<chData>(FromJsonData);

           for(int q= 0; q < data.itemNum2.Count; q++)
            {
                GameObject.Find("ItemData").transform.GetComponent<Merge>().itemdata[data.itemNum2[q]].spawncheck = true;
            }

            for(int q = 0; q < data.itemNum1.Count; q++)
            {
                cb.save = true;
                mg1.objPosition1 = data.Pos1[q];
                mg1.itemCreate(data.itemNum1[q]);
            }

            print("불러오기 완료");
        }
    }


    // 저장하기
    public void SaveGameData()
    {
        int chc = GameObject.Find("chp").transform.childCount;

        data.itemNum1.RemoveAll(chc => true);
        data.itemNum2.RemoveAll(chc => true);

        for (int q =0; q < chc; q++)
        {
            data.itemNum1.Add(GameObject.Find("chp").transform.GetChild(q).GetComponent<MergeItem>().iN);
        }

        data.Pos1.RemoveAll(chc => true);
        for (int q = 0; q < chc; q++)
        {
            data.Pos1.Add(GameObject.Find("chp").transform.GetChild(q).transform.position);
        }


        for(int q= 0; q < GameObject.Find("ItemData").transform.GetComponent<Merge>().itemdata.Count; q++)
        {
            if (GameObject.Find("ItemData").transform.GetComponent<Merge>().itemdata[q].spawncheck)
            {
                data.itemNum2.Add(GameObject.Find("ItemData").transform.GetComponent<Merge>().itemdata[q].itemNum);
            }
        }

        data.chCount = GameObject.Find("chp").transform.childCount;
        data.save = true;
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Path.Combine(Application.persistentDataPath, "chData.json");

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);

        // 올바르게 저장됐는지 확인 (자유롭게 변형)
        print("저장 완료");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

[System.Serializable]
public class Game2data1
{
    public List<int> itemNum3 = new List<int>();
}

public class Game2Data : MonoBehaviour
{
    public Game2data1 data = new Game2data1();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Game2save()
    {
        int chc = GameObject.Find("chp").transform.childCount;

        data.itemNum3.RemoveAll(chc => true);

        for (int q = 0; q < chc; q++)
        {
            data.itemNum3.Add(GameObject.Find("chp").transform.GetChild(q).GetComponent<MergeItem>().iN);
        }

        data.itemNum3 = data.itemNum3.Distinct().ToList();
        data.itemNum3.Sort();
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Path.Combine(Application.persistentDataPath, "Game2Data.json");

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);

        // 올바르게 저장됐는지 확인 (자유롭게 변형)
        print("저장 완료");
    }
    public void Game2load()
    {
        int chc = GameObject.Find("chp").transform.childCount;
        string filePath = Path.Combine(Application.persistentDataPath, "Game2Data.json");
        print(filePath);
        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Game2data1>(FromJsonData);

            if(data.itemNum3.Count < 12)
            {
                for (int q = 0; q < data.itemNum3.Count; q++)
                {
                    GameObject.Find("chdata").GetComponent<chnumdata>().spawn(data.itemNum3[q]);
                    GameObject.Find("chdata").GetComponent<chnumdata>().chposition.x += 1.1f;
                    if (q == 5)
                    {
                        GameObject.Find("chdata").GetComponent<chnumdata>().chposition.x = -2.8f;
                        GameObject.Find("chdata").GetComponent<chnumdata>().chposition.y = -5.3f;
                        break;
                    }
                }
                for (int q = 6; q < data.itemNum3.Count; q++)
                {
                    GameObject.Find("chdata").GetComponent<chnumdata>().spawn(data.itemNum3[q]);
                    GameObject.Find("chdata").GetComponent<chnumdata>().chposition.x += 1.1f;
                }
            }
            else
            {
                for (int q = data.itemNum3.Count - 12; q < data.itemNum3.Count; q++)
                {
                    GameObject.Find("chdata").GetComponent<chnumdata>().spawn(data.itemNum3[q]);
                    GameObject.Find("chdata").GetComponent<chnumdata>().chposition.x += 1.1f;
                    if (q == data.itemNum3.Count - 7)
                    {
                        GameObject.Find("chdata").GetComponent<chnumdata>().chposition.x = -2.8f;
                        GameObject.Find("chdata").GetComponent<chnumdata>().chposition.y = -5.3f;
                        break;
                    }
                }
                for (int q = data.itemNum3.Count - 6; q < data.itemNum3.Count; q++)
                {
                    GameObject.Find("chdata").GetComponent<chnumdata>().spawn(data.itemNum3[q]);
                    GameObject.Find("chdata").GetComponent<chnumdata>().chposition.x += 1.1f;
                }
            }
            

            print("불러오기 완료");
        }
    }
}

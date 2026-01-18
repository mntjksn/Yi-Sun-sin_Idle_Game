using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Game2data1
{
    // 도감에 표시할 캐릭터 번호 목록
    public List<int> itemNum3 = new List<int>();
}

public class Game2Data : MonoBehaviour
{
    public Game2data1 data = new Game2data1();

    public void Game2save()
    {
        // 현재 캐릭터 수
        int chc = GameObject.Find("chp").transform.childCount;

        // 리스트 초기화
        data.itemNum3.Clear();

        // 캐릭터 번호 저장
        for (int q = 0; q < chc; q++)
        {
            int itemNum = GameObject.Find("chp").transform.GetChild(q).GetComponent<MergeItem>().iN;
            data.itemNum3.Add(itemNum);
        }

        // 중복 제거 후 정렬
        data.itemNum3 = data.itemNum3.Distinct().ToList();
        data.itemNum3.Sort();

        // Json 변환 후 저장
        string toJsonData = JsonUtility.ToJson(data, true);
        string filePath = Path.Combine(Application.persistentDataPath, "Game2Data.json");
        File.WriteAllText(filePath, toJsonData);

        print("저장 완료");
    }

    public void Game2load()
    {
        // 저장 파일 경로
        string filePath = Path.Combine(Application.persistentDataPath, "Game2Data.json");
        print(filePath);

        // 저장 파일이 없으면 종료
        if (!File.Exists(filePath)) return;

        // 저장된 파일 읽고 Json을 데이터 클래스로 변환
        string fromJsonData = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<Game2data1>(fromJsonData);

        // 캐릭터 표시용 오브젝트 참조
        GameObject chdataObj = GameObject.Find("chdata");
        chnumdata chData = chdataObj.GetComponent<chnumdata>();

        // 최근 12개까지만 화면에 배치
        if (data.itemNum3.Count < 12)
        {
            for (int q = 0; q < data.itemNum3.Count; q++)
            {
                chData.spawn(data.itemNum3[q]);
                chData.chposition.x += 1.1f;

                // 첫 줄 6개 배치 후 줄바꿈 처리
                if (q == 5)
                {
                    chData.chposition.x = -2.8f;
                    chData.chposition.y = -5.3f;
                    break;
                }
            }

            for (int q = 6; q < data.itemNum3.Count; q++)
            {
                chData.spawn(data.itemNum3[q]);
                chData.chposition.x += 1.1f;
            }
        }
        else
        {
            for (int q = data.itemNum3.Count - 12; q < data.itemNum3.Count; q++)
            {
                chData.spawn(data.itemNum3[q]);
                chData.chposition.x += 1.1f;

                // 첫 줄 6개 배치 후 줄바꿈 처리
                if (q == data.itemNum3.Count - 7)
                {
                    chData.chposition.x = -2.8f;
                    chData.chposition.y = -5.3f;
                    break;
                }
            }

            for (int q = data.itemNum3.Count - 6; q < data.itemNum3.Count; q++)
            {
                chData.spawn(data.itemNum3[q]);
                chData.chposition.x += 1.1f;
            }
        }

        print("불러오기 완료");
    }
}
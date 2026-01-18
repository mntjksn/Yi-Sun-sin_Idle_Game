using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Itemimage
{
    // 아이템 번호
    public int itemNum;

    // 아이템 이미지
    public Sprite itemimg;

    // 체력 증가 값
    public int hp;

    // 공격력 증가 값
    public int atk;

    // 쿨타임
    public float cooltime;
}

public class chnumdata : MonoBehaviour
{
    // 아이템 데이터 목록
    public List<Itemimage> itemdata = new List<Itemimage>();

    // 캐릭터 프리팹
    public GameObject chPrefab;

    // 기본 생성 위치
    public Vector3 chposition;

    private void Start()
    {
        // 게임 데이터 불러오기
        GameObject.Find("Save")
            .GetComponent<Game2Data>()
            .Game2load();
    }

    public void spawn(int num)
    {
        // 지정된 위치에 캐릭터 생성
        GameObject chp = Instantiate(
            chPrefab,
            chposition,
            Quaternion.identity
        );

        // 아이템 데이터 초기화
        chp.GetComponent<Spawn>().InitItem(itemdata[num]);
    }

    public void clickspawn(int num)
    {
        // 플레이어 기준 위치에 캐릭터 생성
        Vector3 spawnPos = GameObject
            .FindGameObjectWithTag("player")
            .GetComponent<Spawn>()
            .chpos;

        GameObject chp = Instantiate(
            chPrefab,
            spawnPos,
            Quaternion.identity
        );

        // 아이템 데이터 초기화
        chp.GetComponent<Spawn>().InitItem(itemdata[num]);
    }
}
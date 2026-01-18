using UnityEngine;
using TMPro;

public class BookdataViewer : MonoBehaviour
{
    // 병합 데이터 관리 스크립트
    public Merge mg;

    // 텍스트 컴포넌트
    private TextMeshProUGUI data;

    // 책 번호
    public int booknum;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        data = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // 해당 아이템이 해금된 경우 이름 표시
        if (GameObject.Find("ItemData").GetComponent<Merge>().itemdata[booknum].spawncheck == true)
        {
            data.text = mg.itemdata[booknum].name;
        }
    }
}
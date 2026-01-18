using UnityEngine;
using TMPro;

public class SpawnText : MonoBehaviour
{
    // 최대 스폰 수 표시용 텍스트
    private TextMeshProUGUI maxSpawnText;

    // 머지 시스템 참조(현재 스크립트에서는 사용하지 않음)
    public Merge mg;

    void Start()
    {
        // 같은 오브젝트에 있는 TextMeshProUGUI 컴포넌트 가져오기
        maxSpawnText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // 최대 스폰 가능 수치
        int childMax = PlayerPrefs.GetInt("ChildMax");

        // "chp" 오브젝트의 현재 자식 개수
        GameObject chpObject = GameObject.Find("chp");
        int currentCount = chpObject != null ? chpObject.transform.childCount : 0;

        // 현재 / 최대 형식으로 텍스트 표시
        maxSpawnText.text = " : " + currentCount + "/" + childMax;
    }
}
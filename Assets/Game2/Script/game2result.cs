using UnityEngine;
using TMPro;

public class game2result : MonoBehaviour
{
    // 적 스폰 관리 오브젝트
    public GameObject enemy;

    // 현재 보유 코인 수
    public int curcoin;

    // 결과 텍스트
    private TextMeshProUGUI textScore;

    // 결과 화면 표시 여부
    public bool cu;
    public bool end;

    private void Awake()
    {
        // TextMeshPro 컴포넌트 가져오기
        textScore = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // 저장된 코인 수 불러오기
        curcoin = PlayerPrefs.GetInt("GameGold");
    }

    private void Update()
    {
        // 게임 종료 결과 표시
        if (end == true)
        {
            textScore.text =
                "획득한 코인수 : " + enemy.GetComponent<enemySpawn>().dieenemy +
                "\n현재 코인수 : " + curcoin;
        }

        // 처치한 적 수만 표시
        if (cu == true)
        {
            textScore.text =
                "죽인 적 : " + enemy.GetComponent<enemySpawn>().dieenemy;
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    // 어떤 버튼인지 구분
    public bool game1;
    public bool game2;
    public bool game3;
    public bool etc;

    private Button button;

    private void Awake()
    {
        // 버튼 캐시
        button = GetComponent<Button>();
    }

    private void Update()
    {
        // 현재 재화 불러오기
        int gold = PlayerPrefs.GetInt("Gold");
        int bossTicket = PlayerPrefs.GetInt("BossTicket");

        // 버튼 활성 조건 처리
        if (game1 == true)
        {
            button.interactable = gold >= 100;
        }

        if (game2 == true)
        {
            button.interactable = gold >= 200;
        }

        if (game3 == true)
        {
            button.interactable = bossTicket >= 1;
        }
    }

    public void SceneLoader(string sceneName)
    {
        // 현재 재화 불러오기
        int gold = PlayerPrefs.GetInt("Gold");
        int bossTicket = PlayerPrefs.GetInt("BossTicket");

        // 게임 1 입장
        if (game1 == true && gold >= 100)
        {
            gold -= 100;
            PlayerPrefs.SetInt("Gold", gold);

            LoadSceneAndHandleChp(sceneName);
            return;
        }

        // 게임 2 입장
        if (game2 == true && gold >= 200)
        {
            gold -= 200;
            PlayerPrefs.SetInt("Gold", gold);

            LoadSceneAndHandleChp(sceneName);
            return;
        }

        // 게임 3 입장
        if (game3 == true && bossTicket >= 1)
        {
            bossTicket -= 1;
            PlayerPrefs.SetInt("BossTicket", bossTicket);

            LoadSceneAndHandleChp(sceneName);
            return;
        }

        // 기타 씬 이동
        if (etc == true)
        {
            LoadSceneAndHandleChp(sceneName);
        }
    }

    private void LoadSceneAndHandleChp(string sceneName)
    {
        // 현재 씬 정보와 자식 수를 먼저 확보
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        GameObject chp = GameObject.Find("chp");
        int childCount = 0;
        if (chp != null)
        {
            childCount = chp.transform.childCount;
        }

        // 씬 이동
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;

        // 기존 코드 흐름 그대로 유지
        // 특정 씬에서만 chp 자식들을 활성화하고 그 외에는 비활성화
        bool shouldEnable = (currentSceneIndex == 2 || currentSceneIndex == 3 || currentSceneIndex == 4);

        if (chp == null) return;

        for (int i = 0; i < childCount; i++)
        {
            chp.transform.GetChild(i).gameObject.SetActive(shouldEnable);
        }
    }
}
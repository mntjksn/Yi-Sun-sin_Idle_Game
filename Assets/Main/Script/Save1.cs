using UnityEngine;

public class Save1 : MonoBehaviour
{
    // 데이터 저장 관리 클래스
    public DataManager dm;

    // 일시정지 상태 체크
    private bool m_pause;

    private void Start()
    {
        // 자식 캐릭터가 없을 경우에만 데이터 불러오기
        if (GameObject.Find("chp").transform.childCount <= 0)
        {
            dm.LoadGameData();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        // 앱이 백그라운드로 갈 때 저장
        if (pause)
        {
            dm.SaveGameData();
            m_pause = true;
        }
        else
        {
            // 복귀 시 상태 초기화
            if (m_pause)
            {
                m_pause = false;
            }
        }
    }

    private void OnApplicationQuit()
    {
        // 앱 종료 시 저장
        dm.SaveGameData();
    }

    private void Update()
    {
        // 뒤로가기 키 입력 시 저장 후 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dm.SaveGameData();
            Application.Quit();
        }
    }
}
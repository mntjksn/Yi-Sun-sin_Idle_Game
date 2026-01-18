using UnityEngine;
using UnityEngine.UI;

public class BGM_con : MonoBehaviour
{
    // 메인 배경음 버튼
    public Button btn1;
    public Button btn2;

    // 효과음 버튼
    public Button btn3;
    public Button btn4;

    private void Update()
    {
        // 저장된 사운드 설정 값 불러오기
        int effectSound = PlayerPrefs.GetInt("EFFECT");
        int mainBgm = PlayerPrefs.GetInt("BGM");

        // 메인 배경음 버튼 상태 갱신
        if (mainBgm == 0)
        {
            btn1.interactable = false;
            btn2.interactable = true;
        }

        if (mainBgm == 1)
        {
            btn1.interactable = true;
            btn2.interactable = false;
        }

        // 효과음 버튼 상태 갱신
        if (effectSound == 0)
        {
            btn3.interactable = false;
            btn4.interactable = true;
        }

        if (effectSound == 1)
        {
            btn3.interactable = true;
            btn4.interactable = false;
        }
    }

    public void main_bgm_on()
    {
        // 메인 배경음 켜기
        PlayerPrefs.SetInt("BGM", 0);
    }

    public void main_bgm_off()
    {
        // 메인 배경음 끄기
        PlayerPrefs.SetInt("BGM", 1);
    }

    public void bgm_on()
    {
        // 효과음 켜기
        PlayerPrefs.SetInt("EFFECT", 0);
    }

    public void bgm_off()
    {
        // 효과음 끄기
        PlayerPrefs.SetInt("EFFECT", 1);
    }
}
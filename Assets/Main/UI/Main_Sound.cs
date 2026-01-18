using UnityEngine;

public class Main_Sound : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource bgm;   // 제어할 오디오 소스

    [Header("Type")]
    public bool main;         // 메인 BGM 여부
    public bool effect;       // 효과음 여부

    void Update()
    {
        // PlayerPrefs에 저장된 사운드 설정 값 가져오기
        // 0 = 켜짐, 1 = 꺼짐
        int mainBgm = PlayerPrefs.GetInt("BGM");
        int effectBgm = PlayerPrefs.GetInt("EFFECT");

        // 메인 BGM 설정 처리
        if (main)
        {
            if (mainBgm == 0)
                bgm.mute = false;
            else if (mainBgm == 1)
                bgm.mute = true;
        }

        // 효과음 설정 처리
        if (effect)
        {
            if (effectBgm == 0)
                bgm.mute = false;
            else if (effectBgm == 1)
                bgm.mute = true;
        }
    }
}
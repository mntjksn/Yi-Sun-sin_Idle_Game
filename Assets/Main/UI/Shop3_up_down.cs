using UnityEngine;
using UnityEngine.UI;

public class Shop3_up_down : MonoBehaviour
{
    [Header("Buttons")]
    public Button btn1;    // 감소 버튼
    public Button btn2;    // 증가 버튼

    private int upch;      // 최대 업그레이드 수치
    private int count;    // 현재 선택된 수치

    void Update()
    {
        // PlayerPrefs에서 값 불러오기
        upch = PlayerPrefs.GetInt("UpCh");
        count = PlayerPrefs.GetInt("Count");

        // 업그레이드가 하나도 없을 경우
        if (upch == 0)
        {
            btn1.interactable = false;
            btn2.interactable = false;
        }
        // 최대치에 도달했을 경우
        else if (upch == count)
        {
            btn1.interactable = false;
            btn2.interactable = true;
        }
        // 중간 단계일 경우
        else if (count != 0 && upch > count)
        {
            btn1.interactable = true;
            btn2.interactable = true;
        }
        // 최소값일 경우
        else if (count == 0)
        {
            btn1.interactable = true;
            btn2.interactable = false;
        }
    }

    public void btn_up()
    {
        // 수치 증가
        count += 1;
        PlayerPrefs.SetInt("Count", count);
    }

    public void btn_down()
    {
        // 수치 감소
        count -= 1;
        PlayerPrefs.SetInt("Count", count);
    }
}
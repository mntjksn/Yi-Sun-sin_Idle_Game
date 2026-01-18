using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameButtonDelay : MonoBehaviour
{
    [Header("Refs")]
    public Image image;     // 쿨타임을 표시할 이미지(fillAmount 사용)
    public Button button;   // 쿨타임이 끝나면 다시 활성화될 버튼

    [Header("Cool Time")]
    public float coolTime = 10.0f; // 전체 쿨타임(초)
    public bool isClicked = false; // 쿨타임 진행 여부

    private float leftTime = 0f;   // 남은 쿨타임
    private float speed = 1.0f;    // 쿨타임 진행 속도 배율

    void Update()
    {
        // 쿨타임이 시작된 상태가 아니면 처리하지 않음
        if (!isClicked)
            return;

        // 남은 시간이 있을 때만 감소 처리
        if (leftTime > 0f)
        {
            leftTime -= Time.deltaTime * speed;

            // 시간이 0 이하로 내려가지 않도록 보정
            if (leftTime <= 0f)
            {
                leftTime = 0f;

                // 버튼 다시 활성화
                if (button)
                    button.enabled = true;

                // 쿨타임 종료
                isClicked = false;
            }

            // 남은 시간 비율을 이용해 fillAmount 계산
            float ratio = 1.0f - (leftTime / coolTime);

            if (image)
                image.fillAmount = ratio;
        }
    }

    public void StartCoolTime()
    {
        // 쿨타임 초기화
        leftTime = coolTime;
        isClicked = true;

        // 버튼 비활성화
        if (button)
            button.enabled = false;
    }
}
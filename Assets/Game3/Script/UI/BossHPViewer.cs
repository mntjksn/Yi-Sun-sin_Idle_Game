using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    // 보스1 체력 정보
    public BossHP bossHP;

    // 보스2 체력 정보
    public BossHP boss2HP;

    // 현재 선택된 보스 정보
    public BossSelect Bs;

    // 체력 표시 슬라이더
    private Slider sliderHP;

    private void Awake()
    {
        // 슬라이더 컴포넌트 가져오기
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        // 보스1이 활성화된 경우
        if (Bs.boss1s == true)
        {
            sliderHP.value = bossHP.CurrentHP / bossHP.MaxHP;
        }
        // 보스2가 활성화된 경우
        else if (Bs.boss2s == true)
        {
            sliderHP.value = boss2HP.CurrentHP / boss2HP.MaxHP;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class Game3PlayerHPViewer : MonoBehaviour
{
    // 플레이어 체력 정보
    public Game3PlayerHP Game3PlayerHP;

    // 체력 표시 슬라이더
    private Slider sliderHP;

    // 최대 체력 기준 값
    private float maxHP;

    private void Awake()
    {
        // 슬라이더 컴포넌트 가져오기
        sliderHP = GetComponent<Slider>();

        // 저장된 최대 체력 불러오기
        float mhp = PlayerPrefs.GetFloat("MaxHP");

        // 캐릭터 추가 체력 반영
        mhp += GameObject.Find("chp").GetComponent<chparent>().hp;

        // 체력 바 기준 최대 체력 설정
        maxHP = mhp;
    }

    private void Update()
    {
        // 현재 체력을 최대 체력 비율로 슬라이더에 반영
        sliderHP.value = Game3PlayerHP.CurrentHP / maxHP;
    }
}
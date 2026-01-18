using System.Collections;
using UnityEngine;

public class Game3PlayerHP : MonoBehaviour
{
    // 최대 체력
    [SerializeField]
    private float maxHP;

    // 현재 체력
    private float currentHP;

    // 스프라이트 렌더러
    private SpriteRenderer spriteRenderer;

    // 사망 시 표시할 패널
    public GameObject panel;

    public float MaxHP
    {
        get => maxHP;
        set => maxHP = Mathf.Max(0f, value);
    }

    public float CurrentHP
    {
        get => currentHP;
        set => currentHP = Mathf.Clamp(value, 0f, MaxHP);
    }

    private void Awake()
    {
        // 저장된 최대 체력 불러오기
        float mhp = PlayerPrefs.GetFloat("MaxHP");

        // 캐릭터 추가 체력 반영
        mhp += GameObject.Find("chp").GetComponent<chparent>().hp;

        // 현재 체력 초기화
        currentHP = mhp;

        // 스프라이트 렌더러 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        // 체력 감소
        currentHP -= damage;

        // 피격 색상 연출 재시작
        StopCoroutine(HitColorAnimation());
        StartCoroutine(HitColorAnimation());

        // 체력이 모두 소모되었을 경우 게임 종료 처리
        if (currentHP <= 0f)
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private IEnumerator HitColorAnimation()
    {
        // 피격 시 빨간색으로 변경
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        // 원래 색상으로 복구
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 보스와 직접 충돌했을 경우 데미지 처리
        if (collision.CompareTag("Boss"))
        {
            TakeDamage(5f);
        }
    }
}
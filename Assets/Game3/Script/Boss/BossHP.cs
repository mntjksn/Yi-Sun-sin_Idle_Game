using System.Collections;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    // 보스 최대 체력
    public float maxHP = 1000f;

    // 현재 체력
    private float currentHP;

    // 스프라이트 렌더러
    private SpriteRenderer spriteRenderer;

    // 보스 스크립트 참조
    private Boss boss;
    private Boss2 boss2;

    // 외부 접근용 최대 체력
    public float MaxHP => maxHP;

    // 외부 접근용 현재 체력
    public float CurrentHP => currentHP;

    private void Awake()
    {
        // 체력 초기화
        currentHP = maxHP;

        // 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
        boss2 = GetComponent<Boss2>();
    }

    public void TakeDamage(float damage)
    {
        // 체력 감소
        currentHP -= damage;

        // 피격 색상 연출 재시작
        StopCoroutine(HitColorAnimation());
        StartCoroutine(HitColorAnimation());

        // 체력이 모두 소모되었을 경우 사망 처리
        if (currentHP <= 0f)
        {
            if (boss != null)
            {
                boss.OnDie();
            }

            if (boss2 != null)
            {
                boss2.OnDie();
            }
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
}
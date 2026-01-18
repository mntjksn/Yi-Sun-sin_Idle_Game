using UnityEngine;

public class Game3PlayerBullet : MonoBehaviour
{
    // 기본 데미지 값
    [SerializeField]
    private float damage;

    public float Damage
    {
        get => damage;
        set => damage = Mathf.Max(0f, value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 저장된 플레이어 기본 데미지 불러오기
        float dmg = PlayerPrefs.GetFloat("Damage");

        // 캐릭터 공격력 추가
        dmg += GameObject.Find("chp").GetComponent<chparent>().at;

        // 보스와 충돌했을 경우
        if (collision.CompareTag("Boss"))
        {
            // 보스 체력 감소 처리
            collision.GetComponent<BossHP>().TakeDamage(dmg);

            // 탄환 제거
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    // 탄환 데미지
    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 경우
        if (collision.CompareTag("Player"))
        {
            // 플레이어 체력 감소 처리
            collision.GetComponent<Game3PlayerHP>().TakeDamage(damage);

            // 탄환 제거
            Destroy(gameObject);
        }
    }

    public void OnDie()
    {
        // 탄환 제거 처리
        Destroy(gameObject);
    }
}
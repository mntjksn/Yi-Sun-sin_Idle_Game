using UnityEngine;

public class Rock : MonoBehaviour
{
    // 플레이어에게 줄 데미지 값
    public int damage;

    // 파괴 시 생성될 폭발 이펙트 프리팹
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 경우
        if (collision.CompareTag("Player"))
        {
            // 바위 제거 처리
            OnDie();
        }
    }

    public void OnDie()
    {
        // 바위 오브젝트 제거
        Destroy(gameObject);
    }
}
using UnityEngine;

public class Game1PlayerCollider : MonoBehaviour
{
    // 사망 시 표시될 패널
    public GameObject panel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌했을 경우
        if (collision.CompareTag("Enemy"))
        {
            // 충돌한 적 제거
            Destroy(collision.gameObject);

            // 플레이어 사망 처리
            OnDie();
        }
    }

    public void OnDie()
    {
        // 사망 패널 표시
        panel.SetActive(true);

        // 게임 시간 정지
        Time.timeScale = 0f;

        // 플레이어 오브젝트 제거
        Destroy(gameObject);
    }
}
using UnityEngine;

public class BossTi : MonoBehaviour
{
    // 이동을 담당하는 Movement2D 컴포넌트 참조
    private Movement2D movement2D;

    private void Awake()
    {
        // 현재 오브젝트에 붙어있는 Movement2D 컴포넌트 가져오기
        movement2D = GetComponent<Movement2D>();

        // 생성 시 랜덤한 위치 값 생성
        float x = Random.Range(-2.0f, 2.0f);
        float y = Random.Range(-2.0f, 2.0f);

        // 랜덤 위치로 이동시키기
        movement2D.MoveTo(new Vector3(x, y, 0f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 경우 자신을 제거
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
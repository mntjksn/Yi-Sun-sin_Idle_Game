using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // 코인 이동을 담당하는 Movement2D 컴포넌트
    private Movement2D movement2D;

    // 일정 시간이 지나면 자동으로 삭제되기까지의 시간
    public float Destroytime = 3.0f;

    private void Awake()
    {
        // Movement2D 컴포넌트 가져오기
        movement2D = GetComponent<Movement2D>();

        // 생성 시 랜덤한 위치 값 생성
        float x = Random.Range(-2.0f, 2.0f);
        float y = Random.Range(-2.0f, 2.0f);

        // 랜덤 위치로 이동
        movement2D.MoveTo(new Vector3(x, y, 0f));

        // 일정 시간 후 자동 삭제 코루틴 시작
        StartCoroutine(AutoDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 경우 코인 삭제
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator AutoDestroy()
    {
        // 설정된 시간이 지나면 오브젝트 삭제
        yield return new WaitForSeconds(Destroytime);

        Destroy(gameObject);
    }
}
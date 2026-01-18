using System.Collections;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    // 생성할 적 프리팹
    public GameObject enemyPrefabs;

    // 지금까지 생성한 누적 수
    public int max;

    // 최대 동시 존재 수로 쓰려던 값으로 보임
    public int maxcount;

    // 현재 존재 수로 쓰려던 값으로 보임
    public int cur;

    // 리지드바디
    private Rigidbody2D myrigidbody;

    // 게임 오버 패널로 보이는 오브젝트
    public GameObject panel;

    // 적 능력치 기준으로 쓰는 오브젝트
    public GameObject enemyat;

    // 처치한 적 수
    public int dieenemy;

    // 스폰 간격
    private float spawntime;

    private void Start()
    {
        // 초기 공격력 설정
        enemyat.GetComponent<enemydata>().atk = 5;

        // 카운트 초기화
        dieenemy = 0;
        max = 0;
        cur = maxcount;

        // 스폰 코루틴 시작
        StartCoroutine(spawn());

        // 리지드바디 가져오기
        myrigidbody = GetComponent<Rigidbody2D>();
    }

    private void spawn2()
    {
        // 랜덤 위치에 적 생성
        float randomX = Random.Range(-2.9f, 2.9f);
        Vector3 enemypos = new Vector3(randomX, 6.6f, 1f);
        Instantiate(enemyPrefabs, enemypos, Quaternion.identity);

        // 누적 생성 수 증가
        max += 1;

        // 일정 횟수마다 적 능력치 증가
        if (max % 3 == 0)
        {
            enemyat.GetComponent<enemydata>().hp += 1f;
            enemyat.GetComponent<enemydata>().atk += 1;
        }
    }

    private IEnumerator spawn()
    {
        // 시작 전 대기
        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            // 랜덤 위치에 적 생성
            float randomX = Random.Range(-2.9f, 2.9f);
            Vector3 enemypos = new Vector3(randomX, 6.6f, 1f);
            Instantiate(enemyPrefabs, enemypos, Quaternion.identity);

            // 누적 생성 수 증가
            max += 1;

            // 일정 횟수마다 적 체력 증가
            if (max % 5 == 0)
            {
                enemyat.GetComponent<enemydata>().hp += 0.3f;
            }

            // 다음 스폰 시간 설정
            spawntime = Random.Range(0.5f, 1.6f);

            // 디버그 출력
            Debug.Log(spawntime);

            // 다음 스폰까지 대기
            yield return new WaitForSeconds(spawntime);
        }
    }
}
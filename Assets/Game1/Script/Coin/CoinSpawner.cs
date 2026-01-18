using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // 코인 프리팹
    public GameObject coinPrefab;

    // 보스 아이템 프리팹
    public GameObject bosstiPrefab;

    // 스테이지 범위 데이터
    public Game1StageData Game1StageData;

    private void Awake()
    {
        // 아이템 생성 코루틴 시작
        StartCoroutine(SpawnCoin());
    }

    private IEnumerator SpawnCoin()
    {
        // 시작 전 대기 시간
        yield return new WaitForSeconds(1f);

        while (true)
        {
            // 생성할 아이템 종류 결정
            int spawnitem = Random.Range(0, 100);

            // 스폰 위치 계산
            float positionX = Random.Range(
                Game1StageData.LimitMin.x + 1.0f,
                Game1StageData.LimitMax.x - 1.0f
            );

            float positionY = Random.Range(
                Game1StageData.LimitMin.y + 1.0f,
                Game1StageData.LimitMax.y - 1.0f
            );

            Vector3 spawnPosition = new Vector3(positionX, positionY, 0f);

            // 대부분은 코인 생성
            if (spawnitem < 97)
            {
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            }
            // 낮은 확률로 보스 아이템 생성
            else
            {
                Instantiate(bosstiPrefab, spawnPosition, Quaternion.identity);
            }

            // 다음 생성까지 대기
            float spawnTime = Random.Range(0.5f, 1f);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
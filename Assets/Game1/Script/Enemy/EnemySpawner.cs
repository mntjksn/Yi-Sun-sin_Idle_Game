using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 스테이지 범위 데이터
    public Game1StageData Game1StageData;

    // 적 프리팹
    public GameObject enemyPrefab;

    // 경고 텍스트 오브젝트
    public GameObject textWarning;

    // 적 생성 시간 범위
    public float minSpawnTime;
    public float maxSpawnTime;

    // 첫 생성까지 대기 시간
    public int StartSapwnTime = 5;

    private void Awake()
    {
        // 경고 텍스트 비활성화
        textWarning.SetActive(false);

        // 적 생성 코루틴 시작
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        // 첫 적 생성 전 대기
        yield return new WaitForSeconds(StartSapwnTime);

        while (true)
        {
            // 적 등장 경고 표시
            textWarning.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            textWarning.SetActive(false);

            // 스폰 위치 계산
            float positionX = Random.Range(
                Game1StageData.LimitMin.x,
                Game1StageData.LimitMax.x
            );

            Vector3 enemyPosition = new Vector3(
                positionX,
                Game1StageData.LimitMax.y + 5.0f,
                0f
            );

            // 적 생성
            Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);

            // 다음 생성까지 대기
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
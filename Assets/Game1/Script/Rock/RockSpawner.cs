using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    // 스테이지 범위 데이터
    public Game1StageData Game1StageData;

    // 낙하 경고 라인 프리팹
    public GameObject alertLinePrefab;

    // 바위 프리팹
    public GameObject rockPrefab;

    // 바위 생성 시간 범위
    public float minSpawnTime;
    public float maxSpawnTime;

    // 첫 생성까지 대기 시간
    public int StartSapwnTime = 5;

    private void Awake()
    {
        // 바위 생성 코루틴 시작
        StartCoroutine(SpawnRock());
    }

    private IEnumerator SpawnRock()
    {
        // 시작 전 대기 시간
        yield return new WaitForSeconds(StartSapwnTime);

        while (true)
        {
            // 스폰 위치 계산
            float positionX = Random.Range(
                Game1StageData.LimitMin.x,
                Game1StageData.LimitMax.x
            );

            // 낙하 위치 경고 라인 생성
            GameObject alertLineClone = Instantiate(
                alertLinePrefab,
                new Vector3(positionX, 0f, 0f),
                Quaternion.identity
            );

            // 경고 표시 시간
            yield return new WaitForSeconds(1.0f);

            // 경고 라인 제거
            Destroy(alertLineClone);

            // 바위 생성 위치 계산
            Vector3 rockPosition = new Vector3(
                positionX,
                Game1StageData.LimitMax.y + 1.0f,
                0f
            );

            // 바위 생성
            Instantiate(rockPrefab, rockPosition, Quaternion.identity);

            // 다음 생성까지 대기
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Game1StageData Game1StageData;
    public GameObject enemyPrefab;
    public GameObject textWarning;

    public float minSpawnTime;
    public float maxSpawnTime;
    public int StartSapwnTime = 5;

    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
        textWarning.SetActive(false);
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(StartSapwnTime);

        while (true)
        {
            textWarning.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            textWarning.SetActive(false);

            float positionX = Random.Range(Game1StageData.LimitMin.x, Game1StageData.LimitMax.x);
            Vector3 enemyPosition = new Vector3(positionX, Game1StageData.LimitMax.y + 5.0f, 0);

            Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}

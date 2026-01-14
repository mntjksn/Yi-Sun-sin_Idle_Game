using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public Game1StageData Game1StageData;
    public GameObject alertLinePrefab;
    public GameObject rockPrefab;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int StartSapwnTime = 5;

    private void Awake()
    {
        StartCoroutine("SpawnRock");
    }

    private IEnumerator SpawnRock()
    {
        yield return new WaitForSeconds(StartSapwnTime);

        while (true)
        {
            float positionX = Random.Range(Game1StageData.LimitMin.x, Game1StageData.LimitMax.x);

            GameObject alertLineClone = Instantiate(alertLinePrefab, new Vector3(positionX, 0, 0), Quaternion.identity);

            yield return new WaitForSeconds(1.0f);

            Destroy(alertLineClone);

            Vector3 enemyPosition = new Vector3(positionX, Game1StageData.LimitMax.y + 1.0f, 0);

            Instantiate(rockPrefab, enemyPosition, Quaternion.identity);

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}

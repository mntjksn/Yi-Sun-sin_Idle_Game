using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab, bosstiPrefab;

    public Game1StageData Game1StageData;

    private void Awake()
    {
        StartCoroutine("SpawnCoin");
    }

    private IEnumerator SpawnCoin()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            int spawnitem = Random.Range(0, 100);

            if (spawnitem < 97)
            {
                float positionX = Random.Range(Game1StageData.LimitMin.x + 1.0f, Game1StageData.LimitMax.x - 1.0f);
                float positionY = Random.Range(Game1StageData.LimitMin.y + 1.0f, Game1StageData.LimitMax.y - 1.0f);

                Vector3 coinPosition = new Vector3(positionX, positionY, 0);

                Instantiate(coinPrefab, coinPosition, Quaternion.identity);

            }

            else
            {
                float positionX = Random.Range(Game1StageData.LimitMin.x + 1.0f, Game1StageData.LimitMax.x - 1.0f);
                float positionY = Random.Range(Game1StageData.LimitMin.y + 1.0f, Game1StageData.LimitMax.y - 1.0f);

                Vector3 coinPosition = new Vector3(positionX, positionY, 0);

                Instantiate(bosstiPrefab, coinPosition, Quaternion.identity);
            }

            float spawnTime = Random.Range(0.5f, 1f);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Destroy : MonoBehaviour
{
    public Game1StageData Game1StageData;
    public float YDesPos = 2;

    private void LateUpdate()
    {
        if (transform.position.y < Game1StageData.LimitMin.y - 2 || transform.position.y > Game1StageData.LimitMax.y + YDesPos ||
           transform.position.x < Game1StageData.LimitMin.x - 2 || transform.position.x > Game1StageData.LimitMax.x + 2)
        {
            Destroy(gameObject);
        }
    }
}

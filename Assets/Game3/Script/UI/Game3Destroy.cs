using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3Destroy : MonoBehaviour
{
    public Game3StageData Game3StageData;
    public float YDesPos = 0;

    private void LateUpdate()
    {
        if (transform.position.y < Game3StageData.LimitMin.y - 1 || transform.position.y > Game3StageData.LimitMax.y + YDesPos ||
           transform.position.x < Game3StageData.LimitMin.x - 1 || transform.position.x > Game3StageData.LimitMax.x + 1)
        {
            Destroy(gameObject);
        }
    }
}

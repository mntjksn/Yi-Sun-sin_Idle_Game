using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBullet : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 myPos;
    Vector3 newPos;

    private void Awake()
    {
        targetPos = GameObject.Find("Player").transform.position;
        myPos = transform.position;

        newPos = (targetPos - myPos).normalized;
        //newPos *= 0.5f;
    }

    void Update()
    {
        //transform.position = transform.position + newPos;
        gameObject.GetComponent<Rigidbody2D>().velocity = (targetPos - myPos) * 1.5f;
    }
}

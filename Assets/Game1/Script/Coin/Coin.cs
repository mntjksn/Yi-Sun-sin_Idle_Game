using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Movement2D movement2D;
    public float Destroytime = 3.0f;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();

        float x = Random.Range(-2.0f, 2.0f);
        float y = Random.Range(-2.0f, 2.0f);

        movement2D.MoveTo(new Vector3(x, y, 0));

        StartCoroutine("AutoDestroy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // Use(collision.gameObject);

            Destroy(gameObject);
        }
    }

    private IEnumerator AutoDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Destroytime);

            Destroy(gameObject);
        }
    }
}

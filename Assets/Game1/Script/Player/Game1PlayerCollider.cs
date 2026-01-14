using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1PlayerCollider : MonoBehaviour
{
    public GameObject panel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            OnDie();
        }
    }

    public void OnDie()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        Destroy(gameObject);
    }
}

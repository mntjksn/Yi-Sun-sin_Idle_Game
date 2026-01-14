using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float bossAppearPoint = 2.5f;
    public float minbossAppearPoint;
    public float maxbossAppearPoint;

    public GameObject enemybulletPrefab;
    public float attackRate;

    private Movement2D movement2D;

    public AudioSource enemybgm;


    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();

        StartCoroutine("MoveToAppearPoint");
    }

    private void Update()
    {
        int effect_sound = PlayerPrefs.GetInt("EFFECT");

        if (effect_sound == 0)
        {
            enemybgm.mute = false;
        }

        if (effect_sound == 1)
        {
            enemybgm.mute = true;
        }
    }

    private IEnumerator MoveToAppearPoint()
    {
        movement2D.MoveTo(Vector3.down);

        float bossAppearPoint = Random.Range(minbossAppearPoint, maxbossAppearPoint);

        while (true)
        {
            if (transform.position.y <= bossAppearPoint)
            {
                movement2D.MoveTo(Vector3.zero);
                
                StartCoroutine("Attack");
                yield return new WaitForSeconds(3f);
                StopCoroutine("Attack");
             
                movement2D.MoveTo(Vector3.up);
            }
            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            enemybgm.Play();
            Instantiate(enemybulletPrefab, new Vector2(transform.position.x, transform.position.y - 0.8f), Quaternion.identity);
            yield return new WaitForSeconds(attackRate);
        }
    }
}

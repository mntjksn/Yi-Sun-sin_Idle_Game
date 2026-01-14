using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3PlayerController : MonoBehaviour
{
    public GameObject PlayerBullet;

    public AudioSource shootbgm;

    [SerializeField]
    private float attackRate = 3f;

    public Game3StageData Game3StageData;

    public float AttackRate
    {
        set => attackRate = Mathf.Max(0, value);
        get => attackRate;
    }

    private void Awake()
    {
        StartCoroutine("TryAttack");
    }

    private void Update()
    {
        int effect_sound = PlayerPrefs.GetInt("EFFECT");

        if (effect_sound == 0)
        {
            shootbgm.mute = false;
        }

        if (effect_sound == 1)
        {
            shootbgm.mute = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = GameObject.FindGameObjectWithTag("Boss").transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Game3StageData.LimitMin.x, Game3StageData.LimitMax.x),
                                                              Mathf.Clamp(transform.position.y, Game3StageData.LimitMin.y, Game3StageData.LimitMax.y));
    }

    private IEnumerator TryAttack()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            float attr = PlayerPrefs.GetFloat("AttackRate");

            shootbgm.Play();
            Instantiate(PlayerBullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            
            //Debug.Log(attr);
            yield return new WaitForSeconds(attr);
        }
    }
}

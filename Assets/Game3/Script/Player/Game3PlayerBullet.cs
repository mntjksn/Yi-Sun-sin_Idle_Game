using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float damage;

    public float Damage
    {
        set => damage = Mathf.Max(0, value);
        get => damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float dmg = PlayerPrefs.GetFloat("Damage");

        dmg += GameObject.Find("chp").GetComponent<chparent>().at;

        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossHP>().TakeDamage(dmg);

            //Debug.Log(dmg);
            Destroy(gameObject);
        } 
    }
    private void Update()
    {
        //Debug.Log(item.attack);
    }
}

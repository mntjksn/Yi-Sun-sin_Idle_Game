using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    private float currentHP;
    private SpriteRenderer spriteRenderer;
    public GameObject panel;

    public float MaxHP
    {
        set => maxHP = Mathf.Max(0, value);
        get => maxHP;
    }

    //public float MaxHP => maxHP;
    public float CurrentHP
    {
        set => currentHP = Mathf.Clamp(value, 0, MaxHP);
        get => currentHP;
    }

    private void Awake()
    {
        //PlayerPrefs.SetFloat("MaxHP", MaxHP);
        float MHP = PlayerPrefs.GetFloat("MaxHP");

        MHP += GameObject.Find("chp").GetComponent<chparent>().hp;

        currentHP = MHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        Debug.Log(currentHP);

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if (currentHP <= 0)
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            TakeDamage(5);
        }
    }
}

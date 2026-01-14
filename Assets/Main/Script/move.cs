using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator ani;
    SpriteRenderer sprite;

    public StageData StageData;

    private float x, y;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        Invoke("Think", 0);
    }

    private void Update()
    {
        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
        rigid.velocity = new Vector2(nextMove * x, nextMove * y);

        if (worldpos.x < 0.075f)
        {
            worldpos.x = 0.075f;
            rigid.velocity = new Vector2(rigid.velocity.x * -1, rigid.velocity.y);
            CancelInvoke();
            Think();
        }

        if (worldpos.y < 0.175f)
        {
            worldpos.y = 0.175f;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * -1);
            CancelInvoke();
            Think();
        }

        if (worldpos.x > 0.925f)
        {
            worldpos.x = 0.925f;
            rigid.velocity = new Vector2(rigid.velocity.x * -1, rigid.velocity.y);
            CancelInvoke();
            Think();
        }

        if (worldpos.y > 0.7f)
        {
            worldpos.y = 0.7f;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * -1);
            CancelInvoke();
            Think();
        }

        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);

        if (x < 0)
            sprite.flipX = nextMove == -1;
        if (x > 0)
            sprite.flipX = nextMove == 1;
    }

    void Think()
    {
        int rax = Random.Range(0, 100);
        if (rax < 50)
            x = Random.Range(-0.5f, -0.1f);
        else
            x = Random.Range(0.1f, 0.5f);

        int ray = Random.Range(0, 100);
        if (ray < 50)
            y = Random.Range(-0.5f, -0.1f);
        else
            y = Random.Range(0.1f, 0.5f);

        int ram = Random.Range(0, 100);
        if (ram < 50)
            nextMove = -1;
        else
            nextMove = 1;

        Invoke("Think", 5);

        ani.SetInteger("WalkSpeed", nextMove);
    }
}

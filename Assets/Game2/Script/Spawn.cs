using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawn : MonoBehaviour
{
    Itemimage item;
    SpriteRenderer sr;
    public GameObject enemy;
    public float speed = 1f;
    public Vector3 chpos;
    public Vector3 chpos1;
    public List<GameObject> FoundObjects;
    public float shortDis;
    Animator animator;
    Rigidbody2D myrigidbody;
    SpriteRenderer sprite;
    public Transform boxpos;
    public Vector2 boxSize;

    public int hp;

    public float Spawntime;
    public float Spawntime1;
    public void InitItem(Itemimage i)
    {
        item = i;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.itemimg;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        chpos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 20f, gameObject.transform.position.z);
        chpos1 = new Vector3(0, -1.5f, 0);
        sprite = GetComponent<SpriteRenderer>();
        myrigidbody = GetComponent<Rigidbody2D>();
        hp = item.hp;
        Spawntime = 0;
        Spawntime1 = item.cooltime;
        
    }
    void shortD()
    {
        try
        {
            FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("enemy"));
            shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);
            enemy = FoundObjects[0];
            foreach (GameObject found in FoundObjects)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    enemy = found;
                    shortDis = Distance;
                }
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Spawntime >= 0f && Spawntime <= Spawntime1)
        {
            Spawntime += Time.deltaTime;
        }
        if(Spawntime >= Spawntime1 && gameObject.transform.position.y < -3.65)
        {
            sprite.color = new Color(1, 1, 1, 1f);
        }
        else if(Spawntime <= Spawntime1 && gameObject.transform.position.y < -3.65)
        {
            sprite.color = new Color(1, 1, 1, 0.5f);
        }
        if (hp <= 0 && gameObject.transform.position.y > -3.65)
        {
            GameObject.FindGameObjectWithTag("enemy").GetComponent<enemydata>().sprite.color = Color.white;
            Destroy(gameObject);
        }
        animator.SetInteger("chnum", item.itemNum);
        shortD();
        if (gameObject.transform.position.y > -3.65)
        {
            animator.SetInteger("WalkSpeed", 0);
        }
        if(Vector2.Distance(gameObject.transform.position, enemy.transform.position) <= 8f && Vector2.Distance(gameObject.transform.position, enemy.transform.position) > 1f && gameObject.transform.position.y > -3.65)
        {
            animator.SetBool("attack", false);
            speed = 1.0f;
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, Time.deltaTime * speed);

            DirectionEnemy(enemy.transform.position.x, transform.position.x);

        }
        if(Vector2.Distance(gameObject.transform.position, enemy.transform.position) < 1.1f && gameObject.transform.position.y > -3.65)
        {
            //speed = 0;
            //animator.SetInteger("WalkSpeed", 0);
           
            animator.SetBool("attack", true);
            //Debug.Log("1");
        }
        
        //Debug.Log(Vector2.Distance(gameObject.transform.position, enemy.transform.position));
    }

    private void OnMouseUp()
    {
        if(gameObject.transform.position.y < -3.65 && Spawntime >= Spawntime1)
        {
            GameObject chp = Instantiate(gameObject, chpos1, Quaternion.identity);
            chp.gameObject.GetComponent<Spawn>().InitItem(item);
            chp.tag = "player";
            
            Spawntime = 0;
        }
    }


    public void DirectionEnemy(float target, float baseobj)
    {
        if (target < baseobj)
        {
            animator.SetInteger("WalkSpeed", -1);
            sprite.flipX = true;
            if(boxpos.localPosition.x > 0)
            {
                boxpos.localPosition = new Vector2(boxpos.localPosition.x * -1, boxpos.localPosition.y);
            }
        }

        else
        {
            animator.SetInteger("WalkSpeed", 1);
            sprite.flipX = false;
            if(boxpos.localPosition.x < 0)
            {
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x), boxpos.localPosition.y);
            }
        }
    }
    public void attack()
    {
        if (animator.GetInteger("WalkSpeed") == 0)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "enemy")
                {
                    
                    enemy.GetComponent<enemydata>().hp -= item.atk;
                    if(enemy.GetComponent<enemydata>().hp > 0)
                    {
                        StartCoroutine(GameObject.FindGameObjectWithTag("enemy").GetComponent<enemydata>().HitColorAnimation());
                    }
                    
                    //StopCoroutine(GameObject.FindGameObjectWithTag("enemy").GetComponent<enemydata>().HitColorAnimation());


                    //Debug.Log(enemy.GetComponent<enemydata>().hp);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxpos.position, boxSize);
    }

    public IEnumerator HitColorAnimation()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(1f);
        sprite.color = Color.white;
    }
}

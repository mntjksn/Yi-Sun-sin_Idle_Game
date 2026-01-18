using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // 적용할 아이템 데이터
    private Itemimage item;

    // 스프라이트 렌더러
    private SpriteRenderer sr;

    // 가장 가까운 적 오브젝트
    public GameObject enemy;

    // 이동 속도
    public float speed = 1f;

    // 복제 생성 위치
    public Vector3 chpos;
    public Vector3 chpos1;

    // 적 검색 결과 목록
    public List<GameObject> FoundObjects;

    // 가장 가까운 적까지 거리
    public float shortDis;

    // 애니메이터
    private Animator animator;

    // 리지드바디
    private Rigidbody2D myrigidbody;

    // 피격 색상 연출용 스프라이트
    private SpriteRenderer sprite;

    // 공격 판정 박스 위치
    public Transform boxpos;

    // 공격 판정 박스 크기
    public Vector2 boxSize;

    // 현재 체력
    public int hp;

    // 쿨타임 진행 시간
    public float Spawntime;

    // 쿨타임 목표 시간
    public float Spawntime1;

    public void InitItem(Itemimage i)
    {
        // 아이템 데이터 적용
        item = i;

        // 이미지 적용
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.itemimg;
    }

    private void Start()
    {
        // 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        myrigidbody = GetComponent<Rigidbody2D>();

        // 위치 값 초기화
        chpos = new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z);
        chpos1 = new Vector3(0f, -1.5f, 0f);

        // 체력 초기화
        hp = item.hp;

        // 쿨타임 초기화
        Spawntime = 0f;
        Spawntime1 = item.cooltime;
    }

    private void Update()
    {
        // 쿨타임 시간 누적
        if (Spawntime >= 0f && Spawntime <= Spawntime1)
        {
            Spawntime += Time.deltaTime;
        }

        // 쿨타임 상태에 따른 표시 처리
        if (Spawntime >= Spawntime1 && transform.position.y < -3.65f)
        {
            sprite.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (Spawntime <= Spawntime1 && transform.position.y < -3.65f)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.5f);
        }

        // 전투 중 사망 처리
        if (hp <= 0 && transform.position.y > -3.65f)
        {
            GameObject.FindGameObjectWithTag("enemy").GetComponent<enemydata>().sprite.color = Color.white;
            Destroy(gameObject);
            return;
        }

        // 애니메이션 번호 적용
        animator.SetInteger("chnum", item.itemNum);

        // 가장 가까운 적 찾기
        shortD();

        // 대기 위치에서는 이동 애니메이션 정지
        if (transform.position.y > -3.65f)
        {
            animator.SetInteger("WalkSpeed", 0);
        }

        // 적이 일정 거리 안에 있으면 이동
        if (enemy != null &&
            Vector2.Distance(transform.position, enemy.transform.position) <= 8f &&
            Vector2.Distance(transform.position, enemy.transform.position) > 1f &&
            transform.position.y > -3.65f)
        {
            animator.SetBool("attack", false);

            speed = 1.0f;
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, Time.deltaTime * speed);

            DirectionEnemy(enemy.transform.position.x, transform.position.x);
        }

        // 적이 가까우면 공격 상태
        if (enemy != null &&
            Vector2.Distance(transform.position, enemy.transform.position) < 1.1f &&
            transform.position.y > -3.65f)
        {
            animator.SetBool("attack", true);
        }
    }

    private void OnMouseUp()
    {
        // 대기 위치에서 쿨타임이 끝난 경우만 소환
        if (transform.position.y < -3.65f && Spawntime >= Spawntime1)
        {
            GameObject chp = Instantiate(gameObject, chpos1, Quaternion.identity);
            chp.GetComponent<Spawn>().InitItem(item);
            chp.tag = "player";

            // 소환 후 쿨타임 초기화
            Spawntime = 0f;
        }
    }

    private void shortD()
    {
        // 적 태그 오브젝트 중 가장 가까운 대상 선택
        try
        {
            FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("enemy"));
            if (FoundObjects == null || FoundObjects.Count == 0)
            {
                enemy = null;
                shortDis = 0f;
                return;
            }

            enemy = FoundObjects[0];
            shortDis = Vector3.Distance(transform.position, enemy.transform.position);

            foreach (GameObject found in FoundObjects)
            {
                float distance = Vector3.Distance(transform.position, found.transform.position);
                if (distance < shortDis)
                {
                    enemy = found;
                    shortDis = distance;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void DirectionEnemy(float target, float baseobj)
    {
        // 적 위치에 따라 방향 전환
        if (target < baseobj)
        {
            animator.SetInteger("WalkSpeed", -1);
            sprite.flipX = true;

            // 공격 판정 박스 방향 맞추기
            if (boxpos.localPosition.x > 0f)
            {
                boxpos.localPosition = new Vector2(boxpos.localPosition.x * -1f, boxpos.localPosition.y);
            }
        }
        else
        {
            animator.SetInteger("WalkSpeed", 1);
            sprite.flipX = false;

            // 공격 판정 박스 방향 맞추기
            if (boxpos.localPosition.x < 0f)
            {
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x), boxpos.localPosition.y);
            }
        }
    }

    public void attack()
    {
        // 정지 상태에서만 공격 판정 처리
        if (animator.GetInteger("WalkSpeed") == 0)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0f);

            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.CompareTag("enemy"))
                {
                    enemydata enemyData = enemy.GetComponent<enemydata>();
                    enemyData.hp -= item.atk;

                    // 적이 살아있으면 피격 연출
                    if (enemyData.hp > 0)
                    {
                        StartCoroutine(GameObject.FindGameObjectWithTag("enemy").GetComponent<enemydata>().HitColorAnimation());
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // 공격 판정 범위 표시
        Gizmos.color = Color.blue;
        if (boxpos != null)
        {
            Gizmos.DrawWireCube(boxpos.position, boxSize);
        }
    }

    public IEnumerator HitColorAnimation()
    {
        // 피격 색상 연출
        sprite.color = Color.red;
        yield return new WaitForSeconds(1f);
        sprite.color = Color.white;
    }
}
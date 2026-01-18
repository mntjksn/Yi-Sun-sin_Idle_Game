using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydata : MonoBehaviour
{
    // 적 체력
    public float hp;

    // 적 공격력
    public int atk;

    // 가장 가까운 플레이어 오브젝트
    public GameObject Player;

    // 성 오브젝트
    public GameObject castle;

    // 스폰 매니저 오브젝트
    public GameObject enemyspawn;

    // 플레이어 검색 결과 목록
    public List<GameObject> FoundObjects;

    // 가장 가까운 플레이어까지 거리
    public float shortDis;

    // 이동 속도
    public float speed;

    // 공격 판정 박스 위치
    public Transform boxpos;

    // 공격 판정 박스 크기
    public Vector2 boxSize;

    // 스프라이트 렌더러
    public SpriteRenderer sprite;

    // 현재 코인 수
    public int curcoin;

    // 공격 사운드
    public AudioSource shootbgm;

    // 내부 컴포넌트
    private Animator animator;
    private Rigidbody2D myrigidbody;

    // 스폰 매니저 컴포넌트 캐시
    private enemySpawn enemySpawnComp;

    private void Start()
    {
        // 컴포넌트 가져오기
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();

        // 참조 오브젝트 찾기
        castle = GameObject.Find("castle");
        enemyspawn = GameObject.Find("enemySpawn");

        // 스폰 매니저 컴포넌트 캐시
        if (enemyspawn != null)
        {
            enemySpawnComp = enemyspawn.GetComponent<enemySpawn>();
        }
    }

    private void Update()
    {
        // 효과음 설정 적용
        int effectSound = PlayerPrefs.GetInt("EFFECT");
        if (effectSound == 0) shootbgm.mute = false;
        if (effectSound == 1) shootbgm.mute = true;

        // 사망 처리
        if (hp < 0f)
        {
            if (enemySpawnComp != null)
            {
                enemySpawnComp.cur -= 1;
                enemySpawnComp.dieenemy += 1;
            }

            curcoin = PlayerPrefs.GetInt("GameGold");
            curcoin += 1;
            PlayerPrefs.SetInt("GameGold", curcoin);

            Destroy(gameObject);
            return;
        }

        // 가장 가까운 플레이어 찾기
        shortD();

        // 타겟에 따라 이동과 공격 상태 변경
        if (Player == false)
        {
            if (castle == null) return;

            float distToCastle = Vector2.Distance(transform.position, castle.transform.position);

            if (distToCastle < 13f && distToCastle >= 2.0f)
            {
                animator.SetBool("isattack", false);
                transform.position = Vector2.MoveTowards(transform.position, castle.transform.position, Time.deltaTime * speed);
            }
            else if (distToCastle < 3.8f)
            {
                animator.SetBool("isattack", true);
            }
        }
        else
        {
            float distToPlayer = Vector2.Distance(transform.position, Player.transform.position);

            if (distToPlayer < 13f && distToPlayer >= 1f)
            {
                animator.SetBool("isattack", false);
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * speed);
            }
            else if (distToPlayer < 1f)
            {
                animator.SetBool("isattack", true);
            }
        }
    }

    private void shortD()
    {
        // 플레이어 태그 오브젝트 중 가장 가까운 대상 선택
        try
        {
            FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("player"));
            if (FoundObjects == null || FoundObjects.Count == 0)
            {
                Player = null;
                shortDis = 0f;
                return;
            }

            Player = FoundObjects[0];
            shortDis = Vector3.Distance(transform.position, Player.transform.position);

            foreach (GameObject found in FoundObjects)
            {
                float distance = Vector3.Distance(transform.position, found.transform.position);
                if (distance < shortDis)
                {
                    Player = found;
                    shortDis = distance;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    private void attack()
    {
        // 공격 사운드 재생
        shootbgm.Play();

        // 공격 범위 안의 오브젝트 확인
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0f);

        foreach (Collider2D collider in collider2Ds)
        {
            // 플레이어 피격 처리
            if (collider.CompareTag("player"))
            {
                Spawn playerSpawn = GameObject.FindGameObjectWithTag("player").GetComponent<Spawn>();
                playerSpawn.hp -= atk;

                if (playerSpawn.hp > 0)
                {
                    StartCoroutine(playerSpawn.HitColorAnimation());
                }
            }

            // 성 피격 처리
            if (collider.CompareTag("castle"))
            {
                castledata castleData = castle.GetComponent<castledata>();
                castleData.hp -= atk;
                Debug.Log(castleData.hp);
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
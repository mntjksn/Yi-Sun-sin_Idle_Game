using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Boss2 : MonoBehaviour
{
    Animator animator;
    public Game3StageData Game3StageData;
    //public GameObject BossexplosionPrefab;
    public Game3PlayerController Game3PlayerController;
    public Game3PlayerHP playerHP;
    public GameObject panel;
    public float bossAppearPoint = 2.5f;

    private BossState bossStage = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;
    private BossHP bossHP;
    private MergeItem MergeItem;

    public Transform boxpos;
    public Vector2 boxSize;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
        MergeItem = GetComponent<MergeItem>();

        ChangeState(BossState.MoveToAppearPoint);
    }
    private void Update()
    {

    }

    public void ChangeState(BossState newState)
    {
        StopCoroutine(bossStage.ToString());
        bossStage = newState;
        StartCoroutine(bossStage.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        movement2D.MoveTo(Vector3.down);

        while (true)
        {
            if (transform.position.y <= bossAppearPoint)
            {
                movement2D.MoveTo(Vector3.zero);
                ChangeState(BossState.Phase01);
                StartCoroutine("MoveBoss");
            }
            yield return null;
        }
    }

    private IEnumerator MoveBoss()
    {
        
        Vector3 direction = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(0.5f, 1.5f), 0);
        movement2D.MoveTo(direction);
        StartCoroutine(crab());
        while (true)
        {
            
            if (transform.position.x <= Game3StageData.LimitMin.x + 1 || transform.position.x >= Game3StageData.LimitMax.x - 1)
            {
                direction.x *= -1;
                movement2D.MoveTo(direction);
                movement2D.moveSpeed = Random.Range(5, 9);
            }

            if (transform.position.y <= Game3StageData.LimitMin.y + 1 || transform.position.y >= Game3StageData.LimitMax.y - 1)
            {
                direction.y *= -1;
                movement2D.MoveTo(direction);
                movement2D.moveSpeed = Random.Range(5, 9);
            }
            yield return null;
        }
    }

    private IEnumerator Phase01()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);

        while (true)
        {
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.7f)
            {
                bossWeapon.StopFiring(AttackType.CircleFire);
                ChangeState(BossState.Phase02);
            }
            yield return null;
        }
    }

    private IEnumerator crab()
    {
        while (true)
        {
            animator.SetBool("isattack", true);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "Player")
                {
                    playerHP.TakeDamage(30);
                    Debug.Log("1");
                }
            }
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("isattack", false);
            yield return new WaitForSeconds(3f);
        }
    }

    public void OnDie()
    {
        //GameObject clone = Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity);
        //clone.GetComponent<BossExplosion>().Setup(playerController, nextSceneName);
        //SceneManager.LoadScene("Main");

        int bossclear = PlayerPrefs.GetInt("Gold");
        bossclear += 10000;
        PlayerPrefs.SetInt("Gold", bossclear);

        panel.SetActive(true);
        Time.timeScale = 0f;
        Destroy(gameObject);
    }
}

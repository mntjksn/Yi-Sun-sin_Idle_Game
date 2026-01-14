using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum BossState { MoveToAppearPoint = 0, Phase01, Phase02, Phase03 }

public class Boss : MonoBehaviour
{
    public Game3StageData Game3StageData;
    //public GameObject BossexplosionPrefab;
    public Game3PlayerController Game3PlayerController;
    public GameObject panel;
    public float bossAppearPoint = 2.5f;

    private BossState bossStage = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;
    private BossHP bossHP;
    private MergeItem MergeItem;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
        MergeItem = GetComponent<MergeItem>();

        ChangeState(BossState.MoveToAppearPoint);
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
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        movement2D.MoveTo(direction);

        while (true)
        {
            if (transform.position.x <= Game3StageData.LimitMin.x + 1 || transform.position.x >= Game3StageData.LimitMax.x - 1)
            {
                direction.x *= -1;
                movement2D.MoveTo(direction);
                movement2D.moveSpeed = Random.Range(3, 5);
            }

            if (transform.position.y <= Game3StageData.LimitMin.y + 1 || transform.position.y >= Game3StageData.LimitMax.y - 1)
            {
                direction.y *= -1;
                movement2D.MoveTo(direction);
                movement2D.moveSpeed = Random.Range(3, 5);
            }

            yield return null;
        }
    }

    private IEnumerator Phase01()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);

        while (true)
        {
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.8f)
            {
                //bossWeapon.StopFiring(AttackType.CircleFire);
                ChangeState(BossState.Phase02);
            }
            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        bossWeapon.StartFiring(AttackType.Shot2ttt);

        while (true)
        {
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.4f)
            {
                //bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);
                ChangeState(BossState.Phase03);
            }
            yield return null;
        }
    }

    private IEnumerator Phase03()
    {
        bossWeapon.StartFiring(AttackType.Shotttt);

        yield return null;
    }

    public void OnDie()
    {
        // bossclear = PlayerPrefs.GetInt("Gold");
        //bossclear += 100000;
        //PlayerPrefs.SetInt("Gold", bossclear);

        int addBossCoin = Random.Range(1, 6);
        int BossCoin = PlayerPrefs.GetInt("BossCoin");
        BossCoin += addBossCoin;
        PlayerPrefs.SetInt("BossCoin", BossCoin);

        panel.SetActive(true);
        Time.timeScale = 0f;
        Destroy(gameObject);
    }
}

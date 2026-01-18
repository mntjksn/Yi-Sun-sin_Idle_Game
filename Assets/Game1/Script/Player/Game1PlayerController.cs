using UnityEngine;

public class Game1PlayerController : MonoBehaviour
{
    // 스테이지 이동 제한 데이터
    public Game1StageData Game1StageData;

    // 점수 값
    private int score;
    public int Score
    {
        get => score;
        set => score = Mathf.Max(0, value);
    }

    // 게임 골드
    private int gameGold;
    public int GameGold
    {
        get => gameGold;
        set => gameGold = Mathf.Max(0, value);
    }

    // 보스 티켓 수량
    private int bossTicket;
    public int BossTicket
    {
        get => bossTicket;
        set => bossTicket = Mathf.Max(0, value);
    }

    // 점수 증가량
    public int scorePoint = 1;

    // 아이템 획득 시 증가 수치
    private int num = 1;

    // 골드 및 티켓 획득 사운드
    public AudioSource goldbgm;

    private void Update()
    {
        // 효과음 설정 값 확인
        int effectSound = PlayerPrefs.GetInt("EFFECT");

        // 효과음 켜짐
        if (effectSound == 0)
        {
            goldbgm.mute = false;
        }

        // 효과음 꺼짐
        if (effectSound == 1)
        {
            goldbgm.mute = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 코인 획득 처리
        if (collision.CompareTag("Coin"))
        {
            goldbgm.Play();
            OnDie();
            Destroy(collision.gameObject);
        }

        // 보스 티켓 획득 처리
        if (collision.CompareTag("Ticket"))
        {
            goldbgm.Play();
            OnDie2();
            Destroy(collision.gameObject);
        }
    }

    public void OnDie()
    {
        // 점수 증가
        Score += scorePoint;

        // 골드 저장 및 증가
        GameGold = PlayerPrefs.GetInt("GameGold");
        GameGold += num;
        PlayerPrefs.SetInt("GameGold", GameGold);
    }

    public void OnDie2()
    {
        // 보스 티켓 저장 및 증가
        BossTicket = PlayerPrefs.GetInt("BossTicket");
        BossTicket += num;
        PlayerPrefs.SetInt("BossTicket", BossTicket);
    }

    private void LateUpdate()
    {
        // 플레이어 이동 범위 제한
        transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                Game1StageData.LimitMin.x,
                Game1StageData.LimitMax.x
            ),
            Mathf.Clamp(
                transform.position.y,
                Game1StageData.LimitMin.y,
                Game1StageData.LimitMax.y
            )
        );
    }
}
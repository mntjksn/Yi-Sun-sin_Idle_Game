using UnityEngine;
using UnityEngine.UI;

public class ClickLimit : MonoBehaviour
{
    // 클릭 버튼
    public Button btn;

    // 쿨타임 표시 이미지
    public Image image;

    // 아이템 생성 관리
    public Merge mg;

    // 쿨타임 진행 시간
    private float imageTime;

    // 클릭 회복까지 누적 시간
    private float timeLimit;

    // 현재 남은 클릭 수
    private int clickNum = 5;

    public int ClickNum
    {
        get => clickNum;
        set => clickNum = Mathf.Max(0, value);
    }

    // 사용처가 없지만 구조 유지용으로 유지
    private float spawnTime;
    public float SpawnTime
    {
        get => spawnTime;
        set => spawnTime = Mathf.Max(0f, value);
    }

    // 현재 최대 클릭 수
    private int clickMax;
    public int ClickMax
    {
        get => clickMax;
        set => clickMax = Mathf.Max(0, value);
    }

    // 생성할 캐릭터 번호로 쓰는 값
    private int upch;

    private void Awake()
    {
        // 멀티터치 비활성화
        Input.multiTouchEnabled = false;

        // 업그레이드 선택 값을 카운트로 초기화
        int resetcount = PlayerPrefs.GetInt("UpCh");
        PlayerPrefs.SetInt("Count", resetcount);

        // Merge 참조 가져오기
        mg = GameObject.Find("ItemData").GetComponent<Merge>();

        // 버튼 클릭 이벤트 등록
        btn.onClick.AddListener(() => mg.itemCreate(upch));

        // 남은 클릭 수 불러오기
        ClickNum = PlayerPrefs.GetInt("ClickNum");
    }

    private void Update()
    {
        // 저장된 값들 불러오기
        int childMax = PlayerPrefs.GetInt("ChildMax");
        int cm = PlayerPrefs.GetInt("ClickMax");
        float st = PlayerPrefs.GetFloat("SpawnTime");
        upch = PlayerPrefs.GetInt("Count");

        // 클릭 수가 최대치보다 적고 쿨타임이 진행 중일 때 회복 처리
        if (cm > ClickNum && image.fillAmount > 0f)
        {
            timeLimit += Time.deltaTime;

            // 회복 시간이 지나면 클릭 수 증가
            if (timeLimit > st)
            {
                ClickPlus();
                GetComponent<Button>().interactable = true;
                timeLimit = 0f;
            }

            // 쿨타임 게이지 감소 처리
            if (image.fillAmount > 0f)
            {
                imageTime = Time.deltaTime;
                float time = imageTime / st;
                image.fillAmount -= time;

                // 게이지가 0이 되면 다시 1로 되돌림
                if (image.fillAmount == 0f)
                {
                    image.fillAmount = 1f;
                }
            }
        }

        // 최대치일 때 불필요한 누적 방지
        if (timeLimit > st && ClickMax == ClickNum)
        {
            timeLimit = 0f;
        }

        // 클릭 수가 없거나 자식 캐릭터가 가득 차면 버튼 비활성화
        if (ClickNum == 0 || childMax == GameObject.Find("chp").transform.childCount)
        {
            GetComponent<Button>().interactable = false;
        }
        else if (ClickNum != 0)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }

    public void Click()
    {
        // 클릭 시 쿨타임 게이지용 시간 초기화
        imageTime = 0f;

        // 클릭 수 감소 후 저장
        ClickNum = PlayerPrefs.GetInt("ClickNum");
        ClickNum -= 1;
        PlayerPrefs.SetInt("ClickNum", clickNum);
    }

    public void ClickPlus()
    {
        // 클릭 수 증가 후 저장
        ClickNum = PlayerPrefs.GetInt("ClickNum");
        ClickNum += 1;
        PlayerPrefs.SetInt("ClickNum", clickNum);

        // 게이지 초기화
        image.fillAmount = 1f;
    }
}
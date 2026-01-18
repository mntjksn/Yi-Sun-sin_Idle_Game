using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MergeItem : MonoBehaviour
{
    // 렌더러와 아이템 데이터
    private SpriteRenderer sr;
    private Item item;

    // 드래그 선택 여부
    private bool isSelect;

    // 겹쳐진 아이템
    private GameObject contactItem;

    // 애니메이터
    private Animator animator;

    // chp 부모 오브젝트
    public GameObject chpa;

    // 아이템이 주는 골드 값
    private int chgold;

    // 현재 위치
    private Vector3 myPos;

    // 외부에서 확인용 값들
    public int iN;
    public bool SC;
    public float a1;
    public float a2;
    public float a3;

    // 골드 획득 이펙트
    public GameObject GoldImage;

    // 현재 골드
    private int gold;

    public int Gold
    {
        get => gold;
        set => gold = Mathf.Max(0, value);
    }

    // 골드 획득 주기 기본값
    private float getGoldTime = 5.0f;

    public float GetGoldTime
    {
        get => getGoldTime;
        set => getGoldTime = Mathf.Max(0f, value);
    }

    private void Awake()
    {
        // 컴포넌트 참조
        animator = GetComponent<Animator>();

        // 부모 지정
        chpa = GameObject.FindGameObjectWithTag("chp");
        transform.parent = chpa.transform;

        // 초기 위치 저장
        myPos = transform.position;
    }

    private void OnEnable()
    {
        // 골드 획득 코루틴 시작
        StartCoroutine(getgold());
    }

    private IEnumerator getgold()
    {
        while (true)
        {
            // 저장된 골드 획득 주기 불러오기
            float interval = PlayerPrefs.GetFloat("GetGoldTime");

            // 현재 씬 인덱스 확인
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            // 현재 골드 불러오기
            Gold = PlayerPrefs.GetInt("Gold");

            // 골드 배율
            int upgold = PlayerPrefs.GetInt("UpGold");

            // 특정 씬에서는 골드 이미지 이펙트 생성
            if (sceneIndex == 1)
            {
                Vector3 pos = new Vector3(myPos.x, myPos.y + 0.35f, myPos.z);
                GameObject goldimage = Instantiate(GoldImage, pos, Quaternion.identity);
                Destroy(goldimage, 0.125f);

                Debug.Log(interval);
            }

            // 골드 증가 후 저장
            Gold += chgold * upgold;
            PlayerPrefs.SetInt("Gold", gold);

            yield return new WaitForSeconds(interval);
        }
    }

    public void InitItem(Item i)
    {
        // 아이템 데이터 설정
        item = i;

        // 스프라이트 적용
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.itemimg;

        // 골드 값 저장
        chgold = item.itemgold;
    }

    private void OnMouseDown()
    {
        // 드래그 시작
        isSelect = true;
    }

    private void OnMouseDrag()
    {
        // 마우스 위치로 이동
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = worldPos;
    }

    private void OnMouseUp()
    {
        // 드래그 종료
        isSelect = false;

        // 같은 아이템에 겹쳐져 있으면 합성 처리
        if (contactItem != null)
        {
            Destroy(contactItem);
            Destroy(gameObject);

            GameObject.Find("ItemData").GetComponent<Merge>().itemCreate(item.itemNum + 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 드래그 중이고 같은 등급 아이템과 겹친 경우만 저장
        MergeItem other = collision.GetComponent<MergeItem>();
        if (other == null) return;

        if (isSelect && item.itemNum == other.item.itemNum)
        {
            contactItem = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 같은 등급 아이템에서 벗어나면 겹침 해제
        MergeItem other = collision.GetComponent<MergeItem>();
        if (other == null) return;

        if (item.itemNum == other.item.itemNum)
        {
            contactItem = null;
        }
    }

    private void Update()
    {
        // 현재 아이템 정보를 외부에서 확인할 수 있게 갱신
        iN = item.itemNum;
        SC = item.spawncheck;

        a1 = item.attack;
        a2 = item.hp;
        a3 = item.itemgold;

        // 애니메이션 파라미터 갱신
        animator.SetInteger("chnum", item.itemNum);

        // 위치 갱신
        myPos = transform.position;
    }
}
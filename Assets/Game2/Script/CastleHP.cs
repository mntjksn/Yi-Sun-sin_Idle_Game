using UnityEngine;
using UnityEngine.UI;

public class CastleHP : MonoBehaviour
{
    // 체력을 표시할 성 오브젝트
    public GameObject castle;

    // 체력 슬라이더
    [SerializeField]
    private Slider hpbar;

    private void Start()
    {
        // 시작 시 체력 바 초기화
        UpdateHPBar();
    }

    private void Update()
    {
        // 매 프레임 체력 바 갱신
        UpdateHPBar();
    }

    private void UpdateHPBar()
    {
        // 성의 현재 체력 비율을 슬라이더에 반영
        castledata data = castle.GetComponent<castledata>();
        hpbar.value = data.hp / data.maxhp;
    }
}
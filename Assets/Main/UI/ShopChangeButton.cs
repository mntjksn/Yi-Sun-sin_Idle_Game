using UnityEngine;

public class ShopChangeButton : MonoBehaviour
{
    [Header("Panels")]
    public GameObject offpanel;   // 비활성화할 패널
    public GameObject onpanel;    // 활성화할 패널

    public void change_shop()
    {
        // 이전 상점 패널 비활성화
        if (offpanel)
            offpanel.SetActive(false);

        // 변경할 상점 패널 활성화
        if (onpanel)
            onpanel.SetActive(true);
    }
}
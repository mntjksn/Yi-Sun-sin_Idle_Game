using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopChangeButton : MonoBehaviour
{
    public GameObject offpanel, onpanel;

    public void change_shop()
    {
        offpanel.SetActive(false);
        onpanel.SetActive(true);
    }
}

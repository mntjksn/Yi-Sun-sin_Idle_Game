using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class BookButton : MonoBehaviour
{
    public Merge mg;
    public GameObject image;

    public int booknum;

    private void Awake()
    {
        Button tmp = gameObject.GetComponent<Button>();
        ColorBlock cb = tmp.colors;

        Color newcolor = new Color(0, 0, 0, 0);

        cb.normalColor = newcolor;
        cb.selectedColor = newcolor;
        cb.disabledColor = newcolor;
        cb.pressedColor = newcolor;

        if (GameObject.Find("ItemData").transform.GetComponent<Merge>().itemdata[booknum].spawncheck == true)
        {
            tmp.colors = cb;

            image.SetActive(true);
        }
    }

    public void but_event()
    {
        if (GameObject.Find("ItemData").transform.GetComponent<Merge>().itemdata[booknum].spawncheck == true)
        {
            Instantiate(mg.itemdata[booknum].panel, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        }
    }
}

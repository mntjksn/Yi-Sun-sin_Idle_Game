using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CastleHP : MonoBehaviour
{
    public GameObject castle;
    [SerializeField]
    private Slider hpbar;

    private float maxHP;
    private float curHP;


    // Start is called before the first frame update
    void Start()
    {
        hpbar.value = castle.GetComponent<castledata>().hp / castle.GetComponent<castledata>().maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(castle.GetComponent<castledata>().maxhp);
        //Debug.Log(castle.GetComponent<castledata>().hp);
        HandleHP();
    }
    private void HandleHP()
    {
        hpbar.value = castle.GetComponent<castledata>().hp / castle.GetComponent<castledata>().maxhp;
    }
}

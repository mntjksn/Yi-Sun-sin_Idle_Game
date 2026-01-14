using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castledata : MonoBehaviour
{
    Rigidbody2D myrigidbody;
    public GameObject panel;
    public float hp;
    public float maxhp;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        maxhp = hp;
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (hp < maxhp / 2)
        {
            animator.SetBool("isburning", true);
        }
        if (hp < 0)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
        }
    }
}

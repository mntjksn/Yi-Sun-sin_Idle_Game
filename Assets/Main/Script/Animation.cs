using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Animator animator;
    Item item;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}

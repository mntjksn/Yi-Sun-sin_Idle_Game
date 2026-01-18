using UnityEngine;

public class Animation : MonoBehaviour
{
    // 애니메이터 컴포넌트
    public Animator animator;

    // 아이템 데이터 참조
    private Item item;

    private void Awake()
    {
        // 애니메이터 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }
}
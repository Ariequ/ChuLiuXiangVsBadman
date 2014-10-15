using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    [HideInInspector]
    public bool isPressed;
    Animator animator;
    AnimatorStateInfo info;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        info = animator.GetCurrentAnimatorStateInfo(0);

        isPressed = info.IsName("Pressed");
    }
}

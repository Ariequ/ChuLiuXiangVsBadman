using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    AnimatorStateInfo state;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsTag("Jump"))
        {
            if (animator.GetFloat("Height") > 0.5f && Input.GetKeyDown(KeyCode.J))
            {
                animator.SetInteger("ActionCMD", 1);
            }
        }
    }
}

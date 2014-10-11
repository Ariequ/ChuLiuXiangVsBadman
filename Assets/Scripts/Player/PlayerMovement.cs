using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private float speed = 0;
    private Vector3 direction;         
    private int m_SpeedId = 0;
    
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        m_SpeedId = Animator.StringToHash("Speed");     
    }
    
    void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Idle") || state.IsName("Locomotion"))
        {
            JoystickToEvents.Do(transform, Camera.main.transform, ref speed, ref direction);           
            animator.SetFloat(m_SpeedId, speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, MathUtils.LookRotationXZ(direction), Time.deltaTime * 10); 
        }
        else
        {
            animator.SetFloat(m_SpeedId, 0);
        }

		if (state.IsName("Idle"))
		{
			animator.SetBool("Attacked", false);
		}
    }
}

using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private float speed = 0;
    private Vector3 direction;
    private Vector3 movement;                   // The vector to store the direction of the player's movement.
    private Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    private int m_SpeedId = 0;
    
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent <Rigidbody> ();
        m_SpeedId = Animator.StringToHash("Speed");     
    }
    
    void FixedUpdate()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (animator && Camera.main)
        {   
            JoystickToEvents.Do(transform, Camera.main.transform, ref speed, ref direction);

//            bool inIdle = state.IsName("Idle");
//            float speedDampTime = inIdle ? 0 : 0.1f;
            
            animator.SetFloat(m_SpeedId, speed);//, 0.1f, Time.deltaTime);   
        }   
      
        if (state.IsName("Idle") || state.IsName("Move") && !state.IsTag("Attack"))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, MathUtils.LookRotationXZ(direction), Time.deltaTime * 10); 
            Move(direction.x, direction.z);
        }
    }

    void Move (float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set (h, 0f, v);
        
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime * 6;
        
        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition (transform.position + movement);
    }
}

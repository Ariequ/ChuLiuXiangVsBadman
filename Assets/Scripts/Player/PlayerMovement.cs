using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private float speed = 0;
    private Vector3 direction;
    private Locomotion locomotion = null;
    private Vector3 movement;                   // The vector to store the direction of the player's movement.
    private Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
        playerRigidbody = GetComponent <Rigidbody> ();
    }
    
    void FixedUpdate()
    {
        if (animator && Camera.main)
        {
            JoystickToEvents.Do(transform, Camera.main.transform, ref speed, ref direction);
            locomotion.Do(speed * 6, direction);
        }   
      
        transform.rotation = MathUtils.LookRotationXZ(direction);

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Move"))
        {
//            transform.Translate(velocity, Space.World);
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

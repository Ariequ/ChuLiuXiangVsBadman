using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class HeroController : MonoBehaviour
{
    private Animator animator;
    private float speed = 0;
    private Vector3 direction;
    private Locomotion locomotion = null;
    private float lastAttackTime;
    private int currentAttackType;
    private bool lastStateIsAttack;
    private bool firstInIdleOrMove = true;
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

        checkAttack();
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

    private void checkAttack()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        bool inIdle = state.IsName("Idle");

        if (Input.GetKey(KeyCode.J))
        {   
            animator.SetBool("AttackKeyDown", true);
        }
        else
        {
            animator.SetBool("AttackKeyDown", false);
        }

        if (inIdle)
        {
            if (Input.GetKey(KeyCode.J) && firstInIdleOrMove)
            {   
                firstInIdleOrMove = false;
                lastAttackTime = Time.time; 
                currentAttackType++;

                if (currentAttackType > 3)
                {
                    currentAttackType = 1;
                }

                animator.SetInteger("AttackState", currentAttackType);
            }
            else if (Time.time - lastAttackTime >= 2f)
            {
                currentAttackType = 0;
                animator.SetInteger("AttackState", currentAttackType);
            }

            animator.SetBool("Attacked", false);
        }
        else
        {
            firstInIdleOrMove = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            bool attacking = state.IsTag("Attack");
            
            if (attacking)
            {
//                Animator playerAnimatior = other.gameObject.GetComponent<Animator>();
//                playerAnimatior.SetBool("Attacked", true);

                EnemyHealth enemyHealth = other.GetComponent <EnemyHealth> ();
                if(enemyHealth != null)
                {
                    enemyHealth.TakeDamage (100, Vector3.zero);
                }

            }
        }
    }
}

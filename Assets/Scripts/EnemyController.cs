using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private GameObject target;
    private Vector3 direction;
    private float speed = 1f;
    private float enemyDistance;
    private float updateGap;
    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("ChuLiuXiang");
        animator = GetComponent<Animator>();
    }


    
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        bool inIdle = state.IsName("Idle");

        if (inIdle)
        {
            animator.SetBool("Attacked", false);
        }
    }

    void FixedUpdate()
    {
        updateGap -= Time.deltaTime;

        if (updateGap < 0)
        {
            updateGap = Random.value * 10;
            direction = MathUtils.XZVector((target.transform.position - transform.position)).normalized;
            transform.rotation = MathUtils.LookRotationXZ(direction);
        }

        enemyDistance = MathUtils.XZDistance(target.transform.position, transform.position);
       
        if (enemyDistance > 1.2f)
        {
            speed = 1f;
            rigidbody.AddForce(direction, ForceMode.VelocityChange);  
        }
        else
        {
            speed = 0f;  

            if (Random.value < 0.1f)
            {
                animator.SetInteger("AttackState", MathUtils.ChooseOneFromTwo(1, 2));
            }
            else
            {
                animator.SetInteger("AttackState", 0);
            }
        }

        animator.SetFloat("EnemyDistance", enemyDistance);
        animator.SetFloat("Speed", speed);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            bool attacking = state.IsTag("Attack");

            if (attacking)
            {
                Animator playerAnimatior = other.gameObject.GetComponent<Animator>();
                playerAnimatior.SetBool("Attacked", true);
            }
        }
    }
}

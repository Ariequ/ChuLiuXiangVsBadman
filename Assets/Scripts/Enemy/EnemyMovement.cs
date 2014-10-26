using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
    NavMeshObstacle obstacle;
    Animator animator;
    AnimatorStateInfo state;
    float moveTime;
    public float movePeriodTime = 5f;
    CharacterController characterController;

    public bool navEnabled { get; set; }

    void Awake()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent <PlayerHealth>();
        enemyHealth = GetComponent <EnemyHealth>();
        nav = GetComponent <NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        navEnabled = true;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        state = animator.GetCurrentAnimatorStateInfo(0);

        if (!state.IsName("Move"))
        {
            nav.enabled = false;
            obstacle.enabled = true;
            moveTime = 0;
            animator.SetFloat("MovedTime", moveTime);
            return;
        }       

        if (!characterController.isGrounded)
        {
            characterController.Move(Physics.gravity * Time.deltaTime * 5);
        }

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.enabled = true;
            obstacle.enabled = false;
            nav.SetDestination(player.transform.position);
        } else
        {
            nav.enabled = false;
            obstacle.enabled = true;
        }

        moveTime += Time.deltaTime;

        animator.SetFloat("MovedTime", moveTime);
    }
}

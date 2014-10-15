using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
	Animator animator;
    AnimatorStateInfo state;
    float moveTime;
    public float movePeriodTime = 5f;

    public bool navEnabled { get; set; }

    void Awake()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent <PlayerHealth>();
        enemyHealth = GetComponent <EnemyHealth>();
        nav = GetComponent <NavMeshAgent>();
        navEnabled = true;
		animator = GetComponent<Animator>();
    }

    void Update()
    {
        state = animator.GetCurrentAnimatorStateInfo(0);

        if (!state.IsName("Move"))
        {
            nav.enabled = false;
            moveTime = 0;
            animator.SetFloat("MovedTime", moveTime);
            return;
        }

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
        }
        else
        {
            nav.enabled = false;
        }

        moveTime += Time.deltaTime;

        animator.SetFloat("MovedTime", moveTime);
    }
}

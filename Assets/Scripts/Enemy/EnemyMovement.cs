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

        if (state.IsTag("Hurt"))
        {
            nav.enabled = false;
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

		animator.SetFloat("Speed", nav.velocity.magnitude);
    }
}

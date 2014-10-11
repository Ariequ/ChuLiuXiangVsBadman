using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
	Animator animator;

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

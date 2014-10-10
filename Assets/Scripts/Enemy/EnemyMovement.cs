using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;

    public bool navEnabled { get; set; }

    Animator anim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent <PlayerHealth>();
        enemyHealth = GetComponent <EnemyHealth>();
        nav = GetComponent <NavMeshAgent>();
        navEnabled = true;
        anim = GetComponent<Animator>();
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
    }
}

using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    EnemyMovement enemyMovement;


    void Awake ()
    {
        player = GameObject.Find ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
        anim = GetComponent <Animator> ();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
            enemyMovement.navEnabled = false;
            anim.SetBool("PlayerInRange", true);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
            enemyMovement.navEnabled = true;
            anim.SetBool("PlayerInRange", false);
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

		AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

		if (state.IsTag("Hurt"))
		{
			anim.SetInteger("HurtType", 0);
		}

        if (timer >= timeBetweenAttacks &&  enemyHealth.currentHealth > 0 && playerInRange)
        {
			Vector3 targetDirection = MathUtils.XZVector(player.transform.position) - MathUtils.XZVector(gameObject.transform.position);
			Quaternion r = Quaternion.LookRotation(targetDirection);
			transform.rotation = r;
            anim.SetBool("Attacking", true);
            timer = 0f;
        }
        else
        {
            anim.SetBool("Attacking", false);
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    public void EnemyAttackEvent ()    // called in animation clipe
    {
        Debug.Log("EnemyAttackEvent be called");

        if(playerHealth.currentHealth > 0  && playerInRange)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}

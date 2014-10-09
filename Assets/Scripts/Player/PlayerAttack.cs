using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 2f;
    public int attackDamage = 10;
    Animator anim;
    PlayerHealth playerHealth;
    float timer;
	private float lastAttackTime;
	private int currentAttackType;
	private bool lastStateIsAttack;
	private bool firstInIdleOrMove = true;
	private Queue inputQueue;

    void Awake ()
    {
        playerHealth = GetComponent <PlayerHealth> ();
        anim = GetComponent <Animator> ();
		inputQueue = new Queue();
    }

    void Update ()
    {
        timer += Time.deltaTime;

		checkAttack();
    }

	private void checkAttack()
	{
		AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
		AnimatorTransitionInfo transition = anim.GetAnimatorTransitionInfo(0);
		bool inIdle = state.IsName("Idle");
		bool inAttack = state.IsTag("Attack");
		bool isAttackToIdle = transition.IsName("AttackToIdle");

		if ((isAttackToIdle || inAttack) && Input.GetKeyDown(KeyCode.J))
		{   
			inputQueue.Enqueue(1);
		}
		
		if (inIdle)
		{
			inputQueue.Clear();

			if (Input.GetKeyDown(KeyCode.J))
			{   
				anim.SetBool("HasAttackCommond", true);
			}

			anim.SetBool("Attacked", false);
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Enemy")
		{
			AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
			bool attacking = state.IsTag("Attack");
			
			if (attacking)
			{
				EnemyHealth enemyHealth = other.GetComponent <EnemyHealth> ();
				if(enemyHealth != null)
				{
					enemyHealth.TakeDamage (100, Vector3.zero);
				}
				
			}
		}
	}

	void OnAnimationComplete(int type)
	{
		if (type == 3)
		{
			inputQueue.Clear();
		}

		if (inputQueue.Count > 0)
		{
			inputQueue.Dequeue();
			anim.SetBool("HasAttackCommond", true);
		}
		else
		{
			anim.SetBool("HasAttackCommond", false);
		}
	}
}

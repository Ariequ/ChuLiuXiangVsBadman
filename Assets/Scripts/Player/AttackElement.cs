using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackElement : MonoBehaviour
{

	public GameObject player;
	public string[] attackNames;
	public int m_attackType;

	Animator animator;
	List<int> m_nameHash;
	PlayerAttack playerAttack;
	// Use this for initialization
	void Awake()
	{
		animator = player.GetComponent<Animator>();
        m_nameHash = new List<int>();

        for (int i=0; i<attackNames.Length; i++)
        {
            Debug.Log("Animator.StringToHash(attackNames[i]) : " + Animator.StringToHash("Attack." + attackNames[i]));
            m_nameHash.Add(Animator.StringToHash("Attack." + attackNames[i]));
        }

		playerAttack = player.GetComponent<PlayerAttack>();
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Enemy")
		{
			AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
			bool attacking = state.IsTag("Attack");
			
			if (attacking)
			{
				EnemyHealth enemyHealth = other.GetComponent <EnemyHealth>();

				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);



				Debug.Log("stateInfo.nameHash: " + stateInfo.nameHash );
				Debug.Log("m_nameHash : " + m_nameHash[0]);

				if (enemyHealth != null && m_nameHash.Contains(stateInfo.nameHash))
				{
					Debug.Log("enemy take damage");
					playerAttack.canMakeDamage = false;
					enemyHealth.TakeDamage(10, Vector3.zero, m_attackType);
				}
			}
		}
	}
}

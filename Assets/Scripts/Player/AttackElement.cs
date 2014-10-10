using UnityEngine;
using System.Collections;

public class AttackElement : MonoBehaviour
{

	public GameObject player;
	public string attackName;
	public int m_attackType;

	Animator animator;
	int m_nameHash;
	PlayerAttack playerAttack;
	// Use this for initialization
	void Awake()
	{
		animator = player.GetComponent<Animator>();
		m_nameHash = Animator.StringToHash("Base Layer." + attackName);
		playerAttack = player.GetComponent<PlayerAttack>();
	}

	void OnTriggerStay(Collider other)
	{
		Debug.Log("OnTriggerEnter===");

		if (other.tag == "Enemy")
		{
			Debug.Log("@@@@@");
			AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
			bool attacking = state.IsTag("Attack");
			
			if (attacking)
			{

				Debug.Log("#####");
				EnemyHealth enemyHealth = other.GetComponent <EnemyHealth>();

				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

				Debug.Log("stateInfo.nameHash: " + stateInfo.nameHash );
				Debug.Log("m_nameHash : " + m_nameHash);

				if (enemyHealth != null && stateInfo.nameHash == m_nameHash)
				{
					Debug.Log("enemy take damage");
					playerAttack.canMakeDamage = false;
					enemyHealth.TakeDamage(10, Vector3.zero, m_attackType);
				}
			}
		}
	}
}

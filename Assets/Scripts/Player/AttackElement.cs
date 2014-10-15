using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackElement : MonoBehaviour
{

	public GameObject player;
	public string[] attackNames;
    public float cameraShakeTime = 0.1f;
	public int m_attackType;
	Animator animator;
	AnimatorStateInfo state;
	List<int> m_nameHash;
	private PlayerAttack playerAttack;
	EnemyHealth enemyHealth;
	// Use this for initialization
	void Awake()
	{
		animator = player.GetComponent<Animator>();
		m_nameHash = new List<int>();
		playerAttack = player.GetComponent<PlayerAttack>();

		for (int i=0; i<attackNames.Length; i++)
		{
			m_nameHash.Add(Animator.StringToHash(attackNames [i]));
		}
	}

	void OnTriggerStay(Collider other)
	{
		state = animator.GetCurrentAnimatorStateInfo(0);
		if (state.IsTag("Attack") && playerAttack.NeedCheckAttack > 0 && other.tag == "Enemy")
		{
			enemyHealth = other.GetComponent <EnemyHealth>();
            if (enemyHealth != null && enemyHealth.currentHealth > 0)
			{
				bool inRightState = false;
				foreach (string attackName in attackNames)
				{
					if (state.IsName(attackName))
					{
						inRightState = true;
						break;
					}
				}
				
				if (inRightState)
				{
					if (state.IsName("attack05"))
					{
						animator.SetFloat("AnimationSpeed", 0.1f);
						StartCoroutine("resetAnimationSpeed");
					}
					else
					{
						animator.SetFloat("AnimationSpeed", 1f);
					}
					
					enemyHealth.TakeDamage(10, Vector3.zero, m_attackType);
                    iTween.ShakePosition (Camera.main.gameObject, iTween.Hash ("x", 0.3f, "time", cameraShakeTime));
				}
			}
		}
	}

	IEnumerator resetAnimationSpeed()
	{
		yield return new WaitForSeconds(0.3f);
		animator.SetFloat("AnimationSpeed", 1f);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackElement : MonoBehaviour
{
	public GameObject player;
	public string[] attackNames;
	public float cameraShakeTime = 0.5f;
	public int m_attackType;
	Animator animator;
	AnimatorStateInfo state;
	List<int> m_nameHash;
//	private PlayerAttack playerAttack;
	EnemyHealth enemyHealth;
	PlayerMovement playerMovement;
//	private bool needCheckAttak;
	private List<GameObject> contactGameObjects = new List<GameObject>();

	float pauseTime;

	// Use this for initialization
	void Awake()
	{
		animator = player.GetComponent<Animator>();
		m_nameHash = new List<int>();
//		playerAttack = player.GetComponent<PlayerAttack>();
		playerMovement = player.GetComponent<PlayerMovement>();

		for (int i=0; i<attackNames.Length; i++)
		{
			m_nameHash.Add(Animator.StringToHash(attackNames [i]));
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (!contactGameObjects.Contains(other.gameObject))
		{
			this.contactGameObjects.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		this.contactGameObjects.Remove(other.gameObject);
	}

	public void TakeAttack()
	{
		state = animator.GetCurrentAnimatorStateInfo(0);
		bool inRightState = false;
		foreach (string attackName in attackNames)
		{
//			Debug.Log(attackName);
			if (state.IsName(attackName))
			{
				inRightState = true;
				break;
			}
		}

		if (!inRightState)
		{
			return;
		}

		foreach (GameObject enemy in contactGameObjects)
		{
//			Debug.Log("++++++");
			enemyHealth = enemy.GetComponent <EnemyHealth>();

			if (enemyHealth != null && enemyHealth.currentHealth > 0)
			{
				if (state.IsName("attack05"))
				{
					Time.timeScale = 0.1f;
					animator.SetFloat("AnimationSpeed", 0f);
					StartCoroutine("resetAnimationSpeed");
				}
				else
				{
					animator.SetFloat("AnimationSpeed", 1f);
				}

				Vector3 targetDirection = MathUtils.XZVector(player.transform.position) - MathUtils.XZVector(enemy.transform.position);
				Quaternion r = Quaternion.LookRotation(targetDirection);
				enemy.transform.rotation = r;
			
				//					player.transform.rotation = Quaternion.LookRotation(-targetDirection);
				playerMovement.ChangeDirection(-targetDirection);
			
				enemyHealth.TakeDamage(10, Vector3.zero, m_attackType);
				iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", 0.3f, "time", cameraShakeTime));

			}
		}
	}

//	void OnTriggerStay(Collider other)
//	{
//		state = animator.GetCurrentAnimatorStateInfo(0);
//		if (state.IsTag("Attack") && playerAttack.NeedCheckAttack > 0 && other.tag == "Enemy")
//		{
//			enemyHealth = other.GetComponent <EnemyHealth>();
//			if (enemyHealth != null && enemyHealth.currentHealth > 0)
//			{
//				bool inRightState = false;
//				foreach (string attackName in attackNames)
//				{
//					if (state.IsName(attackName))
//					{
//						inRightState = true;
//						break;
//					}
//				}
//				
//				if (inRightState)
//				{
//					if (state.IsName("attack05"))
//					{
//						animator.SetFloat("AnimationSpeed", 0.1f);
//						StartCoroutine("resetAnimationSpeed");
//					}
//					else
//					{
//						animator.SetFloat("AnimationSpeed", 1f);
//					}
//
//					Vector3 targetDirection = MathUtils.XZVector(player.transform.position) - MathUtils.XZVector(other.transform.position);
//					Quaternion r = Quaternion.LookRotation(targetDirection);
//					other.transform.rotation = r;
//
////					player.transform.rotation = Quaternion.LookRotation(-targetDirection);
//					playerMovement.ChangeDirection(-targetDirection);
//
//					enemyHealth.TakeDamage(10, Vector3.zero, m_attackType);
//					iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", 0.3f, "time", cameraShakeTime));
//				}
//			}
//		}
//	}

	public void SetCheckAttackTrigger()
	{
//		this.needCheckAttak = true;
	}

	IEnumerator resetAnimationSpeed()
	{
		yield return new WaitForSeconds(0.02f);
		Time.timeScale = 1;
		animator.SetFloat("AnimationSpeed", 1f);
	}
}

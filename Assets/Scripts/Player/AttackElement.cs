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
    private PlayerAttack playerAttack;
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
        if (other.tag == "Enemy")
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            bool attacking = state.IsTag("Attack");

            if (attacking)
            {
                EnemyHealth enemyHealth = other.GetComponent <EnemyHealth>();

                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                bool inRightState = false;
                foreach (string attackName in attackNames)
                {
                    Debug.Log(attackName);
                    if (stateInfo.IsName(attackName))
                    {
                        inRightState = true;
                        break;
                    }
                }

                if (enemyHealth != null && inRightState && playerAttack.NeedCheckAttack > 0)
                {
                    Debug.Log("enemy take damage");

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

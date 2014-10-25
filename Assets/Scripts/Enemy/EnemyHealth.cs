using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth = 100;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
	public Text healthText;


	Animator animator;
    AudioSource enemyAudio;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
	float timer;
	
    void Awake ()
    {
		animator = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
		timer += Time.deltaTime;

		healthText.text = currentHealth.ToString();
		
        if (isSinking)
		{
			transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		}
		else if (isDead)
		{
			animator.SetTrigger("Dead");
		}
    }


    public void TakeDamage (int amount, Vector3 hitPoint, int attackType)
    {
        if(isDead)
        {
            return;
        }
         

        enemyAudio.Play ();

        currentHealth -= amount;
            
//        hitParticles.transform.position = hitPoint;
//        hitParticles.Play();

        if (currentHealth <= 0)
		{
			Death();
		}
		else
		{
            timer = 0;
			animator.SetInteger("HurtType", attackType);
		}
    }
    
    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

		animator.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}

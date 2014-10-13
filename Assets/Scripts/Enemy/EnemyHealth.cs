using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth = 100;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
	private float timeBetweenHurts = 0.02f;


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
        if(isDead || timer < timeBetweenHurts)
        {
            return;
        }
         

        enemyAudio.Play ();

        currentHealth -= amount;

		Debug.Log("Hurting amout: " + amount);
            
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

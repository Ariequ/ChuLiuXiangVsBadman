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
    private float cameraShakeTime = 0.05f;
    Animator animator;
    AudioSource enemyAudio;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    float timer;
    AnimatorStateInfo state;
    
    void Awake()
    {
        animator = GetComponent <Animator>();
        enemyAudio = GetComponent <AudioSource>();
        capsuleCollider = GetComponent <CapsuleCollider>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        timer += Time.deltaTime;

        healthText.text = currentHealth.ToString();

        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        } else if (isDead)
        {
            animator.SetTrigger("Dead");
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint, int attackType, Vector3 hurtPostion)
    {
        state = animator.GetCurrentAnimatorStateInfo(0);

        if (isDead || state.IsName("hit03"))
        {
            return;
        }

        Vector3 targetDirection = MathUtils.XZVector(hurtPostion) - MathUtils.XZVector(transform.position);
        Quaternion r = Quaternion.LookRotation(targetDirection);
        transform.rotation = r;

        enemyAudio.Play();
        iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", 0.1f * attackType, "time", cameraShakeTime * attackType));

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        } else
        {
            Debug.Log("hurttype :" + attackType);
            animator.SetTrigger("HurtType_" + attackType);
        }

    }

    public void CheckLive(int amount)
    {
//        Debug.Log("=============");
    }
    
    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        animator.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        GetComponent <NavMeshAgent>().enabled = false;
//        GetComponent <Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCatch : MonoBehaviour
{
    private List<GameObject> contactGameObjects = new List<GameObject>();
    Animator animator;
    PlayerAttack playerAttack;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        playerAttack = GetComponentInParent<PlayerAttack>();
    }

    void OnEnable()
    { 
        EasyButton.On_ButtonDown += HandleOn_ButtonDown;
    }

    void OnDisable()
    {
        EasyButton.On_ButtonDown -= HandleOn_ButtonDown;
    }
    
    void OnDestroy()
    {
        EasyButton.On_ButtonDown -= HandleOn_ButtonDown;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!contactGameObjects.Contains(other.gameObject) && other.tag == "Enemy")
        {
            Debug.Log("enemy name :" + other.name);
            this.contactGameObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit enemy name :" + other.name);
        this.contactGameObjects.Remove(other.gameObject);
    }

    public void CheckCatch()
    {
        Debug.Log("checkcath=====");
        foreach (GameObject enemy in contactGameObjects)
        {
            Animator animator = enemy.GetComponent<Animator>();
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

            Debug.Log("state :" + state.nameHash);

            if (state.IsName("hit03"))
            {
                Debug.Log("can set becatch");
                animator.SetTrigger("BeCatch");
                break;
            }
        }
    }

    void HandleOn_ButtonDown(string buttonName)
    {
        if (buttonName == "Catch" && !playerAttack.IsCatchingEnemy)
        {
            animator.SetTrigger("Catch");
            CheckCatch();
        }
    }
}
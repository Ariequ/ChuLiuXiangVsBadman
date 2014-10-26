using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCatch : MonoBehaviour
{
    private List<GameObject> contactGameObjects = new List<GameObject>();
    Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
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
            this.contactGameObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        this.contactGameObjects.Remove(other.gameObject);
    }

    public void CheckCatch()
    {
        foreach (GameObject enemy in contactGameObjects)
        {
            Animator animator = enemy.GetComponent<Animator>();
            animator.SetTrigger("BeCatch");
        }
    }

    void HandleOn_ButtonDown (string buttonName)
    {
        if (buttonName == "Catch")
        {
            animator.SetTrigger("Catch");
            CheckCatch();
        }
    }
}
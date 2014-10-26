using UnityEngine;
using System.Collections;

public class PlayerAttacked : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        anim.SetInteger("HurtType", Random.Range(1, 3));
    }
}

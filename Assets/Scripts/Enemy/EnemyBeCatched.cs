using UnityEngine;
using System.Collections;

public class EnemyBeCatched : MonoBehaviour
{
    Animator animator;
    GameObject player;
    AnimatorStateInfo state;
    bool messageBeSended;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("lift"))
        {
            Vector3 playerPostion = player.transform.position;
            playerPostion.y += 1.8f;
            transform.position = playerPostion;
            transform.rotation = player.transform.rotation;
            transform.Rotate(new Vector3(0, 90, 0));

            if (!messageBeSended)
            {
                messageBeSended = true;
                player.SendMessage("EnemyBeCatched", gameObject);
            }
        }
        else
        {
            messageBeSended = false;
        }
    }
}

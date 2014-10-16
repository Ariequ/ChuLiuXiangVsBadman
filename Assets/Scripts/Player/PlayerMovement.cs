using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement : MonoBehaviour
{
	public JoyStickInput joyStickInput;
	private Animator animator;
	private float speed = 0;
	private Vector3 direction = Vector3.zero;
	private int m_SpeedId = 0;
    
	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		m_SpeedId = Animator.StringToHash("Speed");     
	}
    
//	void Update()
//	{
//		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
//
//		if (state.IsName("Idle") || state.IsName("Locomotion"))
//		{
////			animator.SetBool("AttackKeyDown", false);
////			animator.SetBool("JumpKeyDown", false);
//			animator.SetFloat(m_SpeedId, speed);
//			transform.rotation = Quaternion.Lerp(transform.rotation, MathUtils.LookRotationXZ(direction), Time.deltaTime * 10); 
//		}
//		else
//		{
//			animator.SetFloat(m_SpeedId, 0);
//		}
//
//		if (state.IsName("Idle"))
//		{
//			animator.SetBool("Attacked", false);
//		}
//	}

//	void FixedUpdate()
//	{
//		#if UNITY_IPHONE
//		joyStickInput.Do(transform, Camera.main.transform, ref speed, ref direction);  
//		#else
//		JoystickToEvents.Do(transform, Camera.main.transform, ref speed, ref direction);   
//		#endif
//         
//		animator.SetBool("JumpKeyDown", Input.GetKeyDown(KeyCode.Space));
//		animator.SetBool("AttackKeyDown", Input.GetKeyDown(KeyCode.J));
//	}

	public void SetAttackKeyDown()
	{
		Debug.Log("SetAttackKeyDown");
		animator.SetBool("AttackKeyDown", true);
	}

	public void SetJumpKeyDown()
	{
		animator.SetBool("JumpKeyDown", true);
	}

	public void OnTap()
	{
		animator.SetTrigger("AttackKeyDown");
		StartCoroutine("resetAttackKeyDown");
	}

	IEnumerator resetAttackKeyDown()
	{
		yield return new WaitForSeconds(0.01f);
		animator.SetBool("AttackKeyDown", false);
	}

	public void OnSlice(Vector2 startPoint, Vector2 endPoint, float velocity)
	{
		Vector3 direction = new Vector3(endPoint.x - startPoint.x, 0, endPoint.y - startPoint.y);
		Quaternion r = Quaternion.LookRotation(direction);
		transform.rotation = r;
		animator.SetFloat(m_SpeedId, velocity);
		animator.SetTrigger("JumpKeyDown");
	}
}

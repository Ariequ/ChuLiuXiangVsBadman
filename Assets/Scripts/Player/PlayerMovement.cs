using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement : MonoBehaviour
{
//	public JoyStickInput joyStickInput;
	private Animator animator;
	private Vector3 direction = Vector3.zero;
	private int m_SpeedId = 0;

	private const float MAX_SPEED = 5.0f;

	CharacterController chracterController;
	
    
	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		m_SpeedId = Animator.StringToHash("Speed");    
		chracterController = GetComponent<CharacterController>();
	}
     
	void Update()
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

		if (state.IsName("Idle") || state.IsName("Locomotion"))
		{
			chracterController.Move(new Vector3(this.direction.normalized.x, 0, this.direction.normalized.z) * this.direction.magnitude * animator.GetFloat("Speed") * Time.deltaTime);
		}
		else
		{
			animator.SetFloat(m_SpeedId, 0);
		}

		if (state.IsName("Idle"))
		{
			animator.SetBool("Attacked", false);
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (Input.mousePosition.x > Screen.width / 2)
			{
				OnTap();
			}
		}
		else
		if (Input.GetKeyDown(KeyCode.J))
		{
			OnTap();
		}

		transform.rotation = Quaternion.Lerp(transform.rotation, MathUtils.LookRotationXZ(direction), Time.deltaTime * 10); 

		if (!chracterController.isGrounded)
		{
			chracterController.Move(Physics.gravity * Time.deltaTime);
		}
	}

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
		animator.SetBool("AttackKeyDown", true);
//		StartCoroutine("resetAttackKeyDown");
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

	public void OnCure(Vector2 point)
	{
		LayerMask layerMaskPlayers = 1 << LayerMask.NameToLayer("Terrain");  
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(point.x, point.y, 0));  
		RaycastHit hit;  
		if (Physics.Raycast(ray, out hit, 600, layerMaskPlayers.value))
		{  
			direction = hit.point - gameObject.transform.position;	
			animator.SetFloat(m_SpeedId, 5f);
		} 
	}

	public void OnLongPress(Vector2 point)
	{
//		Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, 0));
		LayerMask layerMaskPlayers = 1 << LayerMask.NameToLayer("Terrain");  
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(point.x, point.y, 0));  
		RaycastHit hit;  
		if (Physics.Raycast(ray, out hit, 600, layerMaskPlayers.value))
		{  
			direction = hit.point - gameObject.transform.position;	
			animator.SetFloat(m_SpeedId, 5f);
		}  

//		direction = worldPosition;
//		Quaternion r = Quaternion.LookRotation(direction);
//		transform.rotation = r;

	}

	public void OnLongPressComlete()
	{
		animator.SetFloat(m_SpeedId, 0f);
	}

	public void OnCureComplete()
	{
		animator.SetFloat(m_SpeedId, 0f);
	}

	public void ChangeDirection(Vector3 direction)
	{
		this.direction = direction;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			animator.SetFloat(m_SpeedId, 0f);
		}
	}

	void OnEnable()
	{
		EasyJoystick.On_JoystickMove += On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
		EasyButton.On_ButtonPress += On_ButtonPress;	
	}
	
	void OnDisable()
	{
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
		EasyButton.On_ButtonPress -= On_ButtonPress;
	}
	
	void OnDestroy()
	{
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
		EasyButton.On_ButtonPress -= On_ButtonPress;
	}
	
	void On_JoystickMove(MovingJoystick move)
	{
//		float angle = move.Axis2Angle(true);
		this.direction = new Vector3(move.joystickAxis.x, 0, move.joystickAxis.y);
		animator.SetFloat(m_SpeedId, Mathf.Min(1, move.joystickAxis.magnitude) * MAX_SPEED);
	}
	
	void On_JoystickMoveEnd(MovingJoystick move)
	{
		animator.SetFloat(m_SpeedId, 0f);
	}

	void On_ButtonPress(string buttonName)
	{
		OnTap();
	}
}

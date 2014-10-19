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
	
    
	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		m_SpeedId = Animator.StringToHash("Speed");     
	}
    
	void Update()
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

		if (state.IsName("Idle") || state.IsName("Locomotion"))
		{
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

		transform.rotation = Quaternion.Lerp(transform.rotation, MathUtils.LookRotationXZ(direction), Time.deltaTime * 10); 
	}

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
		EasyButton.On_ButtonUp += On_ButtonUp;	
		//EasyButton.On_ButtonDown += On_ButtonDown;
	}
	
	void OnDisable()
	{
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
		//		EasyButton.On_ButtonPress -= On_ButtonPress;
		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}
	
	void OnDestroy()
	{
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
		//		EasyButton.On_ButtonPress -= On_ButtonPress;
		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}
	
	void On_JoystickMove(MovingJoystick move)
	{
		float angle = move.Axis2Angle(true);
		transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
		animator.SetFloat(m_SpeedId, move.joystickAxis.magnitude * 5);
//		transform.Translate(Vector3.forward * move.joystickValue.magnitude * Time.deltaTime);	
	}
	
	void On_JoystickMoveEnd(MovingJoystick move)
	{
		animator.SetFloat(m_SpeedId, 0f);
	}

	void On_ButtonPress(string buttonName)
	{
		OnTap();
	}
	
	void On_ButtonUp(string buttonName)
	{
		if (buttonName == "Exit")
		{
			Application.LoadLevel("StartMenu");	
		}
	}	
}

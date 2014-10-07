using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class CharacterActionController : MonoBehaviour
{
	public float speed = 2f;

	private bool _isAttacking = false;

	private CharacterAnimationController _animationController;

	private CharacterController _characterController;

	private Transform __transform;

	void Awake ()
	{
		__transform = transform;

		_animationController = GetComponent<CharacterAnimationController>();
		_characterController = GetComponent<CharacterController>();
	}

	void Update ()
	{
		if (direction == Vector3.zero)
		{
			_animationController.Idle();
		}
		else if (_animationController.CheckCanMove())
		{
			Vector3 velocity = direction * Time.deltaTime * speed;
			__transform.localRotation = MathUtils.LookRotationXZ(velocity);
			__transform.Translate(velocity, Space.World);
			_animationController.Move(speed);
		}

	}

	public Vector3 direction { private get; set; }

//	public bool IsAttacking
//	{
//		get
//		{
//			return _isAttacking;
//		}
//		set
//		{
//			_isAttacking = value;
//			if (_isAttacking)
//			{
//				direction = Vector3.zero;
//				_animationController.Attack();
//			}
//		}
//	}

	public void Attack ()
	{
		_animationController.Attack();
	}
}


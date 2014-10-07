using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterAnimationController : MonoBehaviour 
{
	private IdleAnimationPeriod _idlePeriod;
	private MoveAnimationPeriod _movePeriod;
	private AttackAnimationPeriod _attackPeriod;

	private AbstractAnimationPeriod _currentPeriod;

	private CharacterAnimationConfig _animationConfig;

	private object _param;

	void Start () 
	{
		_animationConfig = GetComponent<CharacterAnimationConfig>();

		_idlePeriod = new IdleAnimationPeriod(_animationConfig.idleNameList, gameObject);
		_movePeriod = new MoveAnimationPeriod(_animationConfig.moveNameList, gameObject);
		_attackPeriod = new AttackAnimationPeriod(_animationConfig.attackNameList, gameObject);

		_currentPeriod = _idlePeriod;
	}
	
	void Update () 
	{
		bool isPlaying = _currentPeriod.IsPlaying();
//		Debug.Log(isPlaying);
		if (isPlaying)
		{
			_currentPeriod.Play(_param);
		}
		else
		{
			_currentPeriod = _idlePeriod;
		}

//		Debug.Log(_currentPeriod);
	}

	public void Idle()
	{
		if (CheckCanPlay(_idlePeriod))
		{
			_idlePeriod.Play(null);
		}
	}

	public void Move(float speed)
	{
		if (CheckCanPlay(_movePeriod))
		{
			_param = speed;
			_currentPeriod = _movePeriod;
		}
	}

	public void Attack()
	{
		if (CheckCanPlay(_attackPeriod))
		{
			_param = null;
			_currentPeriod = _attackPeriod;
		}
		else if (_currentPeriod == _attackPeriod)
		{
			_attackPeriod.HasNextStep = true;
		}

//		Debug.Log(_attackPeriod.attackQueue.ToString());
	}

	public bool CheckCanMove()
	{
		return _currentPeriod == _idlePeriod || _currentPeriod.canMove;
	}

	private bool CheckCanPlay(AbstractAnimationPeriod period)
	{
		return _currentPeriod == _idlePeriod || !_currentPeriod.isStucked || period.priority > _currentPeriod.priority;
	}
}

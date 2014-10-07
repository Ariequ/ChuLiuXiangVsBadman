using UnityEngine;
using System;

public class AbstractAnimationPeriod
{
	public bool isStucked { get; protected set;}

	public int priority { get; protected set;}

	public bool canMove { get; protected set;}

	protected GameObject _gameObject;

	protected Animation _animation;

	protected string[] _animationNameList;

	protected AnimationState _currentState;

	private float __startTimeStamp;

	public AbstractAnimationPeriod (string[] nameList, GameObject self)
	{
		_gameObject = self;
		_animation = self.animation;

		_animationNameList = nameList;
		_currentState = _animation[_animationNameList[0]];
	}

	public virtual bool IsPlaying ()
	{
//		Debug.Log(_currentState.time);
		return _currentState.time >= 0 && _currentState.time <= _currentState.length;
	}

	public virtual void Play (object param)
	{
		PlayInside();
	}

	public virtual void Stop ()
	{
	}

	protected void PlayInside(string stateName = null, bool cross = true)
	{
		if (stateName == null)
			stateName = _animationNameList[0];

		_currentState = _animation[stateName];

		if (cross)
			_animation.CrossFade(stateName);
		else
			_animation.Play(stateName);
//		Debug.Log(stateName);
		
		__startTimeStamp = Time.time;
	}

}


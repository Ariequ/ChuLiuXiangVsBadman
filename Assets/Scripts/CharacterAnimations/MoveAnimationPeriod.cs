using UnityEngine;

using System;
using System.Collections;

public class MoveAnimationPeriod : AbstractAnimationPeriod
{
	private const float SPEED_THRESHOLD = 2f;

	private string _walkStateName;
	private string _runStateName;

	public MoveAnimationPeriod (string[] nameList, GameObject self) : base (nameList, self)
	{
		isStucked = false;
		priority = 1;
		canMove = true;

		_walkStateName = nameList[0];
		_runStateName = nameList[1];
	}

	public override bool IsPlaying ()
	{
		return base.IsPlaying();
	}
	
	public override void Play (object param)
	{
		float speed = (float)param;
		
		if (speed > SPEED_THRESHOLD)
		{
			PlayInside(_runStateName);
		}
		else
		{
			PlayInside(_walkStateName);
		}
	}
	
	public override void Stop ()
	{
		base.Stop ();
	}
}


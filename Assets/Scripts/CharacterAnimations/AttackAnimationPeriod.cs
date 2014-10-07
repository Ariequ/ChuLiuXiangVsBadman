using UnityEngine;

using System;
using System.Collections;

public class AttackAnimationPeriod : AbstractAnimationPeriod
{
	private const int MAX_COMBO_COUNT = 3;

	public InputQueue attackQueue;

	private bool _nextStep = false;
	private int _comboCount = 0;

	public AttackAnimationPeriod (string[] nameList, GameObject self) : base (nameList, self)
	{
		isStucked = true;
		priority = 2;
		canMove = false;

		attackQueue = new InputQueue(MAX_COMBO_COUNT);
	}

	public override bool IsPlaying ()
	{
		if (!base.IsPlaying())
		{
			if (HasNextStep)
			{
				_comboCount++;
				HasNextStep = false;
				if (_comboCount == MAX_COMBO_COUNT)
				{
					_comboCount = 0;
					return false;
				}
				return true;
			}
			else
			{
				_comboCount = 0;
				return false;
			}
		}
		else
		{
			return true;
		}
	}
	
	public override void Play (object param)
	{
		PlayInside(_animationNameList[_comboCount], false);
//		Debug.Log(_animationNameList[_comboCount]);
	}
	
	public override void Stop ()
	{
		base.Stop ();
	}

	public bool HasNextStep { get; set; }
}


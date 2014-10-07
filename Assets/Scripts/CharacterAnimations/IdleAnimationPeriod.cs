using UnityEngine;

using System;
using System.Collections;

public class IdleAnimationPeriod : AbstractAnimationPeriod
{
	public IdleAnimationPeriod (string[] nameList, GameObject self) : base (nameList, self)
	{
		isStucked = false;
		priority = 0;
		canMove = true;
	}

	public override bool IsPlaying ()
	{
		return base.IsPlaying();
	}

	public override void Play (object param)
	{
		PlayInside(null, false);
	}

	public override void Stop ()
	{
		base.Stop ();
	}
}


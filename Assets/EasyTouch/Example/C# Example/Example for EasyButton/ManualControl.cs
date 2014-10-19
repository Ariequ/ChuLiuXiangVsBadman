﻿using UnityEngine;
using System.Collections;

public class ManualControl : MonoBehaviour {
	
	public EasyJoystick Joystick;

	
	// Update is called once per frame
	void Update () {

		if (Joystick != null )
			Joystick.On_Manual(new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis("Vertical")));
		
	}
}

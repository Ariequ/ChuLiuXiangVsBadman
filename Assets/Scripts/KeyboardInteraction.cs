using UnityEngine;
using System.Collections;

public class KeyboardInteraction : MonoBehaviour
{
	private CharacterActionController _actionController;

	void Awake ()
	{
		_actionController = GetComponent<CharacterActionController>();
	}

	// Update is called once per frame
	void Update ()
	{
		float xMotion = Input.GetAxis("Horizontal");
		float zMotion = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(xMotion, 0f, zMotion);
		_actionController.direction = direction;

		if (Input.GetKeyDown(KeyCode.J))
		{
//			Debug.Log("J");
			_actionController.Attack();
		}
	}
}


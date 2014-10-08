using UnityEngine;
using System.Collections;

public class JoystickToEvents : MonoBehaviour 
{
    public static void Do(Transform root, Transform camera, ref float speed, ref Vector3 direction)
    {
        Vector3 rootDirection = root.forward;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
				
        Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

        // Get camera rotation.    

        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f; // kill Y
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);

        // Convert joystick input in Worldspace coordinates
        Vector3 moveDirection = referentialShift * stickDirection;
				
		Vector2 speedVec =  new Vector2(horizontal, vertical);
		speed = Mathf.Clamp(speedVec.magnitude, 0, 1);      

        if (speed > 0.01f) // dead zone
        {
			direction = moveDirection; //Vector3.Angle(rootDirection, moveDirection) / 180.0f * (axis.y < 0 ? -1 : 1);
        }
        else
		{
//			direction = root.rotation.eulerAngles;

//			Debug.Log("direction: " + direction.ToString());
		}
    }
	
}

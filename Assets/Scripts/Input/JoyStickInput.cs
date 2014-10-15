using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickInput : MonoBehaviour
{
    float firtOperationTime;
    float lastNoOperationTime;
    bool inFastMood;
    bool inNoOperationMood;
    bool pressing;
    public GameObject[] directionButtons;
    Animator animator;
    AnimatorStateInfo info;
    ButtonController[] btnControllers;

    void Awake()
    {
		#if UNITY_IPHONE

		#else
		gameObject.SetActive(false);
		#endif

        animator = GetComponent<Animator>();
        btnControllers = new ButtonController[4];

        btnControllers [0] = directionButtons [0].GetComponent<ButtonController>();
        btnControllers [1] = directionButtons [1].GetComponent<ButtonController>();
        btnControllers [2] = directionButtons [2].GetComponent<ButtonController>();
        btnControllers [3] = directionButtons [3].GetComponent<ButtonController>();
    }

    private enum ButtonDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public void Do(Transform root, Transform camera, ref float speed, ref Vector3 direction)
    {
        float horizontal = 0;

        horizontal = btnControllers [2].isPressed ? -1 : 0;

        if (horizontal == 0)
        {
            horizontal = btnControllers [3].isPressed ? 1 : 0;
        }

        float vertical = 0;

        vertical = btnControllers [0].isPressed ? 1 : 0;

        if (vertical == 0)
        {
            vertical = btnControllers [1].isPressed ? -1 : 0;
        }

        
        if (horizontal != 0 || vertical != 0)
        {        
            inNoOperationMood = false;
            
            if (inFastMood)
            {
                speed = 1f;    
            }
            else
            {
                if (Time.time - lastNoOperationTime < 0.1f)
                {
                    inFastMood = true;
                    speed = 1f;                    
                }
                else
                {
                    speed = 0.5f;
                }
            }
        }
        else
        {
            speed = 0;
            inFastMood = false;
            if (!inNoOperationMood)
            {
                inNoOperationMood = true;
                lastNoOperationTime = Time.time;
            }
        }
        
        Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
        
        // Get camera rotation.    
        
        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f; // kill Y
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);
        
        // Convert joystick input in Worldspace coordinates
        Vector3 moveDirection = referentialShift * stickDirection;
        
        //      Vector2 speedVec =  new Vector2(horizontal, vertical);
        //      speed = Mathf.Clamp(speedVec.magnitude, 0, 1);      
        
        if (speed > 0.01f) // dead zone
        {
            direction = moveDirection; //Vector3.Angle(rootDirection, moveDirection) / 180.0f * (axis.y < 0 ? -1 : 1);
        }
        else
        {
            direction = root.forward;
        }
    }
}

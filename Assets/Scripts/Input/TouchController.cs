using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{
	PlayerMovement playMovement;
	void Awake()
	{
		addTapRecognizer();
		addSwipeRecognizer();
		playMovement = GetComponent<PlayerMovement>();
	}

	private void addTapRecognizer()
	{
		var recognizer = new TKTapRecognizer();
		
		// we can limit recognition to a specific Rect, in this case the bottom-left corner of the screen
		recognizer.boundaryFrame = new TKRect( 0, 0, Screen.width, Screen.height );
		
		// we can also set the number of touches required for the gesture
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			recognizer.numberOfTouchesRequired = 2;
		
		recognizer.gestureRecognizedEvent += ( r ) =>
		{
			playMovement.OnTap();
			SendMessage("SetNeedCheckAttack");
			Debug.Log( "tap recognizer fired: " + r );
		};
		TouchKit.addGestureRecognizer( recognizer );
	}

	private void addSwipeRecognizer()
	{
		var recognizer = new TKSwipeRecognizer( TKSwipeDirection.All );
		recognizer.gestureRecognizedEvent += ( r ) =>
		{
			playMovement.OnSlice(r.startPoint, r.endPoint, r.swipeVelocity);
			Debug.Log( "swipe recognizer fired: " + r );
		};
		TouchKit.addGestureRecognizer( recognizer );
	}
}

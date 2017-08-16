using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

//TODO Slow player movement on ramps
//TODO Stop player leaving the ground when getting to the end of a ramp. 

[RequireComponent (typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float moveStopRadius = 0.2f;
	[SerializeField] private float attackStopRadius = 5f;
	ThirdPersonCharacter thirdPersonCharacter;
	CameraRaycaster cameraRaycaster;
	Vector3 currentDestination;
	Vector3 clickPoint;

	bool isInDirectMode = false;

	void Start () {
		cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
		thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
		currentDestination = transform.position;		
	}

	void FixedUpdate () {
		if (CrossPlatformInputManager.GetButtonDown("ChangeControls")) { //TODO Change this to a menu item. 
			isInDirectMode = !isInDirectMode;
			currentDestination = transform.position; //Clears the click target when changing control mode 
			Debug.Log("Direct Control Mode: " + isInDirectMode);
		}

		if (isInDirectMode) {
			ProcessGamepadMovement ();
		}
		else {
			ProcessMouseMovement ();
		}
	}


	void WalkToDestination ()
	{
		Vector3 playerToClickPoint = currentDestination - transform.position;
		if (playerToClickPoint.magnitude >= moveStopRadius) {
			thirdPersonCharacter.Move (playerToClickPoint, false, false);
		}
		else {
			thirdPersonCharacter.Move (Vector3.zero, false, false);
		}
	}

	Vector3 ShortDestination (Vector3 destination, float shortening) {
		Vector3 reductionVector = (destination - transform.position).normalized * shortening;
		return destination - reductionVector;
	}


	void ProcessMouseMovement ()
	{
		if (Input.GetMouseButton (0)) {
			clickPoint = cameraRaycaster.raycastHit.point;

			switch (cameraRaycaster.currentLayerHit) {
			case Layer.Walkable:
				currentDestination = ShortDestination(clickPoint, moveStopRadius);
				break;
			case Layer.NonTraversable:
				Debug.Log ("Non-Traversable object clicked");
				currentDestination = transform.position;
				break;
			case Layer.Enemy:
				Debug.Log ("Not moving to Enemy");
				currentDestination = ShortDestination(clickPoint, attackStopRadius);
				break;
			case Layer.RaycastEndStop:
				Debug.Log ("Outside Worldspace");
				currentDestination = transform.position;
				break;
			default:
				Debug.Log ("Unexpected layer");
				return;
			}
		}
		WalkToDestination ();
	}

	//Frankensteined from ThirdPersonUserControls
	void ProcessGamepadMovement () {
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

		Transform mainCamera = Camera.main.transform;
		Vector3 cameraForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 characterMove = v*cameraForward + h*mainCamera.right;

		thirdPersonCharacter.Move (characterMove, false, false);
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, clickPoint);
		Gizmos.DrawSphere(currentDestination, 0.15f);
		Gizmos.DrawSphere(clickPoint, 0.1f);
		//Gizmos.DrawWireSphere(transform.position, attackStopRadius);
	}
}

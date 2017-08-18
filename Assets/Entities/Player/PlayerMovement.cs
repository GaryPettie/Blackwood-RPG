using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;

//TODO Slow player movement on ramps
//TODO Stop player leaving the ground when getting to the end of a ramp. 

[RequireComponent (typeof(ThirdPersonCharacter))]
[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent (typeof(AICharacterControl))]
public class PlayerMovement : MonoBehaviour {

	ThirdPersonCharacter thirdPersonCharacter;
	AICharacterControl aiCharacterControl;
	CameraRaycaster cameraRaycaster;
	Vector3 currentDestination;
	Vector3 clickPoint;
	Transform walkTarget;

	const int walkableLayer = 8, nonTraversableLayer = 9;
	const int enemyLayer = 13;

	bool isInDirectMode = false;

	void Start () {
		cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
		cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;

		thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
		aiCharacterControl = GetComponent<AICharacterControl>();

		walkTarget = new GameObject("WalkTarget").GetComponent<Transform>();

		currentDestination = transform.position;

	}

	void ProcessMouseClick (RaycastHit raycastHit, int layerHit) {
		switch (layerHit) {
			case (enemyLayer):
				Transform enemy = raycastHit.collider.transform; 
				aiCharacterControl.SetTarget(enemy);
				break;
			case (walkableLayer):
				
				walkTarget.position = raycastHit.point;
				aiCharacterControl.SetTarget(walkTarget);
				break;
			default:
				Debug.LogWarning("Player clicked on unknown layer, unable to navigate to that location");
				return;
		}
	}


	//TODO not currently being called anywhere
	//Frankensteined from ThirdPersonUserControls
	void ProcessGamepadMovement () {
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

		Transform mainCamera = Camera.main.transform;
		Vector3 cameraForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 characterMove = v*cameraForward + h*mainCamera.right;

		thirdPersonCharacter.Move (characterMove, false, false);
	}
}

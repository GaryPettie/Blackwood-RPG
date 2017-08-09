using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent (typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float moveStopRadius = 0.2f;
	ThirdPersonCharacter m_Character;
	CameraRaycaster cameraRaycaster;
	Vector3 currentClickTarget;

	void Start () {
		cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
		m_Character = GetComponent<ThirdPersonCharacter>();
		currentClickTarget = transform.position;		
	}

	void FixedUpdate () {
		if (Input.GetMouseButtonDown(0)) {
			print ("Cursor raycast hit " + cameraRaycaster.hit.collider.gameObject.name);
			switch (cameraRaycaster.layerHit) {
				case Layer.Walkable:
					currentClickTarget = cameraRaycaster.hit.point;
					break;
				case Layer.NonTraversable:
					Debug.Log("Non-Traversable object clicked");
					break;
				case Layer.Enemy:
					Debug.Log("Not moving to Enemy");
					break;
				default:
					Debug.Log("Unexpected layer");
					return;
			}
		}
		Vector3 playerToClickPoint = currentClickTarget - transform.position ;
		if(playerToClickPoint.magnitude >= moveStopRadius) {
			m_Character.Move(playerToClickPoint, false, false);
		}
		else {
			m_Character.Move(Vector3.zero, false, false);
		}

	}
}

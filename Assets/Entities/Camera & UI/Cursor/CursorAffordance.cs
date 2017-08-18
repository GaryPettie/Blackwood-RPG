using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

	[SerializeField] private Texture2D walkCursor = null;
	[SerializeField] private Texture2D attackCursor = null;
	[SerializeField] private Texture2D errorCursor = null;
	[SerializeField] private Vector2 cursorHotspot = new Vector2(0, 0);

	const int walkableLayer = 8, nonTraversableLayer = 9;
	const int enemyLayer = 13;


	CameraRaycaster cameraRaycaster;

	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster>();
		//TODO Consider de-registering OnLayerChanged on leaving all game scenes
		cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged;
	}
	
	void LateUpdate () {

	}

	void OnLayerChanged (int newLayer) {
		switch (newLayer) {
			case walkableLayer: 
				Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
				break;
			case enemyLayer:
				Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
				break;
			default:
				Cursor.SetCursor(errorCursor, cursorHotspot, CursorMode.Auto);
				return;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

	[SerializeField] private Texture2D walkCursor = null;
	[SerializeField] private Texture2D attackCursor = null;
	[SerializeField] private Texture2D errorCursor = null;
	[SerializeField] private Vector2 cursorHotspot = new Vector2(0, 0);
	CameraRaycaster cameraRaycaster;

	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster>();
		//TODO Consider de-registering OnLayerChanged on leaving all game scenes
		cameraRaycaster.onLayerChange += OnLayerChanged;
	}
	
	void LateUpdate () {

	}

	void OnLayerChanged (Layer newLayer) {
		switch (newLayer) {
			case Layer.Walkable: 
				Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.Enemy:
				Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
				break;
			default:
				Cursor.SetCursor(errorCursor, cursorHotspot, CursorMode.Auto);
				return;
		}
	}
}

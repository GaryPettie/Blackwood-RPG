using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

	[SerializeField] private Texture2D walkCursor = null;
	[SerializeField] private Texture2D attackCursor = null;
	[SerializeField] private Texture2D errorCursor = null;
	[SerializeField] private Vector2 cursorHotspot = new Vector2(96, 96);
	CameraRaycaster cameraRaycaster;

	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	void Update () {
		switch (cameraRaycaster.layerHit) {
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

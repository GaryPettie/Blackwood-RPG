using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

	[SerializeField] float xRotationsPerMinute = 1f;
	[SerializeField] float yRotationsPerMinute = 1f;
	[SerializeField] float zRotationsPerMinute = 1f;
	
	void Update () {
        // xDegreesPerFrame = Time.DeltaTime, 60, 360, xRotationsPerMinute
        // degrees frame^-1 = seconds frame^-1 / seconds minute^-1 * degrees rotation^-1 * rotation minute^-1
        // degrees frame^-1 = frame^-1 minute * degrees rotation^-1 * rotation minute^-1
        // degrees frame^-1 = frame^-1 * degrees

		float xDegreesPerFrame = (xRotationsPerMinute * 360 / 60) * Time.deltaTime;
        transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

		float yDegreesPerFrame = (yRotationsPerMinute * 360 / 60) * Time.deltaTime;
        transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

		float zDegreesPerFrame = (zRotationsPerMinute * 360 / 60) * Time.deltaTime;
        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
	}
}

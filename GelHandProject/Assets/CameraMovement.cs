using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public Transform player;
	public float lerpVal;

	// Use this for initialization

	
	// Update is called once per frame
	private void Update () {
		Vector3 newPosition= Vector3.Lerp (this.transform.position, player.position, lerpVal);
		newPosition.z = -10;
		this.transform.position = newPosition;
	
	}
}

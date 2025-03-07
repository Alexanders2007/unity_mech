using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// LateUpdate is called once per frame (after all items are processed, e.g. after Player moved)
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}

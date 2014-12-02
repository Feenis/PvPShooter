using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour {
	public float speed;
	public float rotatespeed;
	public GameObject bullet;
	private CharacterController player;

	// Use this for initialization
	void Start () {
		player = GetComponent<CharacterController> ();
	}
	

	void Update () {
		Vector3 movement = new Vector3(0,0,0);

		//Vertical Movement and Horizontal
		if (Input.GetAxis("Forward") > 0) 
			movement = speed*transform.forward/5;
		if (Input.GetAxis("Forward") < 0)
			movement = speed*transform.forward/5*-1;

		//Gravity
		if (!player.isGrounded) 
			movement.y = movement.y - .1f;
		
		//Shooting Mechanics
		//bullet.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Rotate")*100);
		if (Input.GetButtonDown ("Shoot"))
			Instantiate (bullet, new Vector3(transform.position.x,
			                                 transform.position.y,
			                                 transform.position.z), transform.rotation);

		player.Move (movement);
		//Camera Movement -  note: must go after player movement
		transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Rotate")*rotatespeed);
	}
}

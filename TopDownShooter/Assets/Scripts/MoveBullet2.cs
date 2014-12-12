using UnityEngine;
using System.Collections;

public class MoveBullet2 : MonoBehaviour {
	public float speed;
	private int death = 120;		//timer for bullet destroy: 3 seconds
	
	void Start () {
		if(Input.GetAxis("Forward2") > 0)
			transform.Translate (new Vector3(0,0,speed+1));
	}
	
	
	void Update () {
		death--;
		//this might work as game mechanic (sidenote)
		//transform.RotateAround (transform.position, Vector3.up, 45);
		transform.Translate (new Vector3(0,0,speed));
		if (death <= 0) 
			Destroy (this.gameObject);
	}
	
	void OnTriggerEnter(Collider hit) 
	{
		if (hit.gameObject.name == "Cube") {
			Destroy (this.gameObject);
		}
	}
	

	
}

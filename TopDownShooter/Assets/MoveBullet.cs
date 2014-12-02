using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour {
	public float speed;
	private int death = 120;

	void Start () {
		if(Input.GetAxis("Forward") > 0)
			transform.Translate (new Vector3(0,0,speed*1.75f));;
	}
	

	void Update () {
		death--;
		//this might work as game mechanic (sidenote)
		//transform.RotateAround (transform.position, Vector3.up, 45);
		transform.Translate (new Vector3(0,0,speed));
		if (death <= 0) 
			Destroy (this.gameObject);
	}
}

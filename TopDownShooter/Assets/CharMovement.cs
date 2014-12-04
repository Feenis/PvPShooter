using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour {
	public float maxspeed;
	public float atkspeed;
	public float dmg;
	public float maxhealth;
	public float rotatespeed;
	public GameObject bullet;
	public GameObject cube2;
	private CharacterController player;
	private float currhealth;
	private float initspeed = .5f;							//initial speed
	private float currspeed;
	private float maxatkspd = .2f;							//fastest the bullet can be rapidly shoty
	private bool shot = false;								//checks if bullet already shot the player
	private bool shooting = false;							//checks if player already shot bullet within attack speed period
	
	// Use this for initialization
	void Start () {
		player = GetComponent<CharacterController> ();	
		currhealth = maxhealth;								//set health to maximum
	}
	
	
	void Update () {
		Vector3 movement = new Vector3(0,0,0);
		shot = false;										//shot checking so it does not damage twice for one bullet

		//Attack Speed Checking
		if(atkspeed < maxatkspd)
			atkspeed = maxatkspd;

		//Forward Movement and Acceleration
		if(currspeed > maxspeed)
			currspeed = maxspeed;
		if (Input.GetAxis("Forward") > 0) {
			if(initspeed > currspeed)
				initspeed = currspeed;
			else
				initspeed += .05f;							//acceleration						
			movement = initspeed*transform.forward/5;
		}
		if (Input.GetAxis("Forward") < 0) {
			if(initspeed > currspeed)
				initspeed = currspeed;
			else
				initspeed += .05f;							//acceleration
			movement = initspeed*transform.forward/5*-1;
		}
		if (Input.GetAxis("Forward") == 0)
			initspeed = .5f;
		
		//Gravity
		if (!player.isGrounded) 
			movement.y = movement.y - 10f;
		
		//Shooting Mechanics
		//bullet.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Rotate")*100);
		if (Input.GetButton ("Shoot") && shooting == false) {
			Instantiate (bullet, new Vector3(transform.position.x,
			                                 transform.position.y,
			                                 transform.position.z), transform.rotation);
			shooting = true;
			StartCoroutine(WaitFor(atkspeed));
		}

		player.Move (movement);
		//Camera Movement -  note: must go after player movement
		transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Rotate")*rotatespeed);
	}
	
	
	void OnTriggerEnter(Collider hit)
	{
		//Got hit by bullet
		if(hit.gameObject.name == "Bullet2(Clone)" && shot == false) 
		{
			float enemydmg = cube2.GetComponent<CharMovement2>().dmg;			//get enemy's damage
			currhealth = currhealth - enemydmg;								//subtract from current health
			if(currhealth <= 0)
				Destroy (this.gameObject);									//death
			shot = true;													//enemy has been shot with this bullet already
		}
	}

	void OnCollisionEnter(Collider hit)
	{
		if(hit.gameObject.name == "attackup"){
			dmg++;
		}
		if(hit.gameObject.name == "attackdown") {
			//cube2.GetComponent<CharMovement2>().lowerdmg();
		}
		if(hit.gameObject.name == "speedup") {
			currspeed += .5f;
		}
		if(hit.gameObject.name == "speeddown") {
			//cube2.GetComponent<CharMovement2>().lowerspeed();
		}
		if(hit.gameObject.name == "atkspeedup") {
			atkspeed -= .1f;
		}
		if(hit.gameObject.name == "atkspeeddown") {
			//cube2.GetComponent<CharMovement2>().loweratkspeed();
		}
		if(hit.gameObject.name == "healthup") {
			if(currhealth+10 <= maxhealth)
				currhealth += 10;
			else
				currhealth = maxhealth;
		}
		Destroy (hit.gameObject);
	}

	IEnumerator WaitFor(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		shooting = false;
	}
}

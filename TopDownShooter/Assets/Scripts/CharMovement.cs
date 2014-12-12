using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour
{
		public float maxspeed;
		public float atkspeed;
		public float dmg;
		public float maxhealth;
		public float maxdmg;
		public float doubleatkspeed;							//power up
		public float incrdmg;									//power up
		public float incrspeed;									//power up
		public float initspeed = 1f;							//initial speed
		public GameObject bullet;
		public GameObject cube2;								//used to deal with effects on enemy player
		private CharacterController player;
		private float rotatespeed = 5f;
		private float currhealth;
		private float currspeed = 3f;							//speed we can get to currently
		private float maxatkspd = .2f;							//fastest the bullet can be rapidly shoty
		private bool shot = false;								//checks if bullet already shot the player
		private bool shooting = false;							//checks if player already shot bullet within attack speed period
		
		public GUISkin sciFiSkin;
		public int currentLevel = 1;
		private bool paused = false;
	
		// Use this for initialization
		void Start ()
		{
				player = GetComponent<CharacterController> ();	
				currhealth = maxhealth;								//set health to maximum\
		}
		
		void OnGUI ()
		{
				
				GUI.skin = sciFiSkin;
				int xOffset = (Screen.width - 75) / 2;
				int yOffset = (Screen.height - 440) / 2;
			
				Rect pos = new Rect (xOffset, yOffset, 100, 25);
				if (GUI.Button (pos, "Pause Game")) {
						Debug.Log ("Clicked the Pause Game button");
						Time.timeScale = 0;
						if (!paused) {
								Time.timeScale = 1;
								paused = false;
						}
						paused = true;
						
				}
				pos.x -= 120;
				if (paused) {
						if (GUI.Button (pos, "Main Menu")) {
								Debug.Log ("Returned to Main Menu");
								Application.LoadLevel (0);
						}
						pos.x += 240;
						if (GUI.Button (pos, "Restart Level")) {
								Application.LoadLevel (currentLevel);
								Time.timeScale = 1;
						}
				}
		}
	
		void Update ()
		{
		
		
				// ****************************************************************
		
				Vector3 movement = new Vector3 (0, 0, 0);
				shot = false;										//shot checking so it does not damage twice for one bullet

				//Attack Speed Checking
				atkspeed *= doubleatkspeed;							//if double attack speed power up is in use

				//Forward Movement and Acceleration
				if (Input.GetAxis ("Forward") > 0) {
						if (initspeed > (currspeed + incrspeed))			//checks for increased movement power up
								initspeed = currspeed;
						else 
								initspeed += .04f;							//acceleration						
						movement = initspeed * transform.forward / 5;
				}
				if (Input.GetAxis ("Forward") < 0) {
						if (initspeed > (currspeed + incrspeed))			//checks for increased movement power up
								initspeed = currspeed;
						else
								initspeed += .04f;							//acceleration
						movement = initspeed * transform.forward / 5 * -1;
				}
				if (Input.GetAxis ("Forward") == 0) {
						if (initspeed > 1.5f)
								initspeed -= .5f;
				}
		
				//Gravity
				if (!player.isGrounded) 
						movement.y = movement.y - 1f;
		
				//Shooting Mechanics
				//bullet.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Rotate")*100);
				if (Input.GetButton ("Shoot") && shooting == false) {
						Instantiate (bullet, new Vector3 (transform.position.x,
			                                 transform.position.y,
			                                 transform.position.z), transform.rotation);
						shooting = true;
						StartCoroutine (WaitFor (atkspeed));
				}

				player.Move (movement);
				//Camera Movement -  note: must go after player movement
				transform.RotateAround (transform.position, Vector3.up, Input.GetAxis ("Rotate") * rotatespeed);
		}
	
	
		void OnTriggerEnter (Collider hit)
		{
				//Got hit by bullet
				if (hit.gameObject.name == "Bullet2(Clone)" && shot == false) {
						float enemydmg = cube2.GetComponent<CharMovement2> ().dmg +
								cube2.GetComponent<CharMovement2> ().incrdmg;	    //get enemy's damage
						currhealth = currhealth - enemydmg;									//subtract from current health
						if (currhealth <= 0)
								Destroy (this.gameObject);										//death
						shot = true;														//enemy has been shot with this bullet already
				}
		}

		void OnCollisionEnter (Collider hit)
		{
				if (hit.gameObject.name == "attackup") {
						if (dmg < maxdmg)
								dmg++;
						else
								dmg = maxdmg;			
				}
				if (hit.gameObject.name == "attackdown") {
						//cube2.GetComponent<CharMovement2>().lowerdmg();
				}
				if (hit.gameObject.name == "speedup") {
						if (currspeed < maxspeed)
								currspeed += .5f;
						else
								currspeed = maxspeed;
				}
				if (hit.gameObject.name == "speeddown") {
						//cube2.GetComponent<CharMovement2>().lowerspeed();
				}
				if (hit.gameObject.name == "atkspeedup") {
						if (atkspeed > maxatkspd)
								atkspeed -= .1f;
						else
								atkspeed = maxatkspd;
				}
				if (hit.gameObject.name == "atkspeeddown") {
						//cube2.GetComponent<CharMovement2>().loweratkspeed();
				}
				if (hit.gameObject.name == "healthup") {
						if (currhealth + 10 <= maxhealth)
								currhealth += 10;
						else
								currhealth = maxhealth;
				}
				if (hit.gameObject.name == "doubleatkspeed") {
						doubleatkspeed = 2;
						StartCoroutine (BackToNormal (5));
				}
				if (hit.gameObject.name == "increasedDmg") {
						incrdmg = 4;
						StartCoroutine (BackToNormal (5));
				}
				if (hit.gameObject.name == "increasedSpd") {
						incrspeed = 2;
						StartCoroutine (BackToNormal (5));
				}
				Destroy (hit.gameObject);
		}

		public void lowerdmg ()
		{
				if (dmg >= 2)
						dmg--;
		}

		public void lowerspeed ()
		{
				if (currspeed >= 1.5f)
						currspeed -= .5f;
		}

		public void loweratkspeed ()
		{
				if (atkspeed < 1)
						atkspeed += .1f;
		}

		//keeps attack speed maintained
		IEnumerator WaitFor (float seconds)
		{
				yield return new WaitForSeconds (seconds);
				shooting = false;
		}

		//returns player back to normal after a specific power up
		IEnumerator BackToNormal (float seconds)
		{
				yield return new WaitForSeconds (seconds);
				incrdmg = 0;
				incrspeed = 0;
				doubleatkspeed = 1;
		}
}

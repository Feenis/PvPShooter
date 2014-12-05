using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
		void OnGUI ()
		{
				int xOffset = (Screen.width - 600) / 2;
				int yOffset = (Screen.height - 400) / 2;
				Rect pos = new Rect (xOffset, yOffset, 600, 25);
		}
}

using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{

		public GUIStyle headerStyle;
		public GUISkin sciFiSkin;
		public int selectedLevel = -1;

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
				GUI.skin = sciFiSkin;
				int xOffset = (Screen.width - 300) / 2;
				int yOffset = (Screen.height - 175) / 2;
		
				Rect pos = new Rect (xOffset, yOffset, 300, 25);
				if (GUI.Button (pos, "Play Game")) {
						Debug.Log ("Clicked the Play Game button");
						Application.LoadLevel (1);
				}
				pos.y += 50;
				if (GUI.Button (pos, "Choose Level")) {
						Debug.Log ("Clicked the choose level button");
				}
		
		}

//	void OnGUI() {
//		int xOffset = (Screen.width - 600) / 2;
//		int yOffset = (Screen.height - 400) / 2;
//
//		Rect pos = new Rect( xOffset, yOffset, 600, 50 );
//		GUI.Label( pos, "Choose Level", headerStyle );
//
//		pos.y += 50;
//		pos.height = 350;
//		GUI.Box( pos, "" );
//
//		int x;
//		int y;
//		int levelNumber = 1;
//		for( y = 0; y < 3; y++ ) {
//			for( x = 0; x < 4; x++ ) {
//				pos.x = xOffset + 125 + x * 100;
//				pos.y = yOffset + 50 + 50 + y * 100;
//				pos.width = pos.height = 50;
//
//				if( GUI.Button( pos, levelNumber.ToString() )) {
//					selectedLevel = levelNumber;
//				}
//				levelNumber++;
//			}
//		}
//
//		pos.x = xOffset + 0;
//		pos.y = yOffset + 400;
//		pos.width = 600;
//		pos.height = 20;
//		GUI.Label( pos, "Selected Level is " + selectedLevel );
//	}
}

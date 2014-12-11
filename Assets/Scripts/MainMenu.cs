using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour{
	public Vector2 playGame;
	public Vector2 loadGame;
	public Vector2 options;
	public Vector2 exit;
	
	private float buttonWidth = 0.3f;
	private float buttonHeight = 0.1f;
	
	//private Texture texture; //Replace button form
	//private GUIStyle style; //Stylize the button
	private GUISkin skin;
	
	void Start(){
		skin = Resources.Load("GUISkin") as GUISkin;
	}
	
	void OnGUI(){
		GUI.skin = skin;
		if(GUI.Button(new Rect(Screen.width * playGame.x, Screen.height * playGame.y,
		                       Screen.width * buttonWidth, Screen.height * buttonHeight), "Play Game")){
			print("Clicked Play Game");
			Application.LoadLevel("level1");
		}		
		if(GUI.Button(new Rect(Screen.width * loadGame.x, Screen.height * loadGame.y,
		                       Screen.width * buttonWidth, Screen.height * buttonHeight), "Load Game")){
			print("Clicked Load Game");
		}		
		if(GUI.Button(new Rect(Screen.width * options.x, Screen.height * options.y,
		                       Screen.width * buttonWidth, Screen.height * buttonHeight), "Options")){
			print("Clicked Options");
		}		
		if(GUI.Button(new Rect(Screen.width * exit.x, Screen.height * exit.y,
		                       Screen.width * buttonWidth, Screen.height * buttonHeight), "Exit")){
			Application.Quit();
		}		
	}
}

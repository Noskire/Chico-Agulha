using UnityEngine;

public class MenuScript : MonoBehaviour{
	public float buttonX;
	public float buttonY;
	public int buttonWidth;
	public int buttonHeight;
	void OnGUI(){
		//Rect StartButton = new Rect(buttonX, buttonY, buttonWidth, buttonHeight);
		Rect StartButton = new Rect((2 * Screen.width / 3 - buttonWidth / 2),
			(2 * Screen.height / 3 - buttonHeight / 2), (Screen.width / 5), (Screen.height / 10));
		if(GUI.Button(StartButton, "Start")){
			Application.LoadLevel("main");
		}
		Rect QuitButton = new Rect((2 * Screen.width / 3 - buttonWidth / 2),
			(2 * Screen.height / 3 - buttonHeight / 2 + (Screen.height / 5)), (Screen.width / 5), (Screen.height / 10));
		if(GUI.Button(QuitButton, "Quit")){
			Application.Quit();
		}
	}
}
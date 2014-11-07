using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour{
	public Rect box, label;
	private int caseSwitch = 1, frame = 0;
	void OnGUI(){
		frame++;
		switch(caseSwitch){
		case 1:
			//Background box
			GUI.Box(box, "Dica: Mover");
			//Movement
			GUI.Label(label, "wasd, setas ou analogico(XBox J)");
			break;
		case 2:
			//Background box
			GUI.Box(box, "Dica: Pular");
			//Jump
			GUI.Label(label, "Espaço ou Y(XBox J)");
			break;
		case 3:
			//Background box
			GUI.Box(box, "Dica: Ataque Fisico");
			//Melee Attack
			GUI.Label(label, "LCtrl ou A(XBox J)");
			break;
		case 4:
			//Background box
			GUI.Box(box, "Dica: Bloquear");
			//Block
			GUI.Label(label, "LAlt ou B(XBox J)");
			break;
		case 5:
			//Background box
			GUI.Box(box, "Dica: Baladeira");
			//Block
			GUI.Label(label, "LCmd ou X(XBox J)");
			break;
		default:
			//Console.WriteLine("Default case");
			break;
		}
		if(frame == 720 && caseSwitch != -1){
			frame = 0;
			caseSwitch++;
			if(caseSwitch > 5){
				caseSwitch = -1;
			}
		}
	}
}

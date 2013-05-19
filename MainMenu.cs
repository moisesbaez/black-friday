using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin customSkin;
	private enum GUIState { Main, Instructions };
	private GUIState currentState = GUIState.Main;
	
	void OnGUI()
	{
		GUI.skin = customSkin;
		
		switch(currentState)
		{
			case GUIState.Main:		
				GUI.Label(new Rect(342.5f, 60f, 595f, 58f), "", "Black Friday Title");
				if(GUI.Button(new Rect(534f, 240f, 212f, 58f), "", "Play Button"))
				{
					Application.LoadLevel("Game");
				}
				if(GUI.Button(new Rect(349f, 360f, 582f, 58f), "", "Instructions Button"))
				{
					currentState = GUIState.Instructions;
				}
				break;
			case GUIState.Instructions:
				GUI.Label(new Rect(168f, 40f, 944f, 576f), "", "Instructions Text");
				if(GUI.Button(new Rect(566.5f, 660f, 147f, 39f), "", "Back Button"))
				{ 
					currentState = GUIState.Main;
				}
				break;
		}
	}
}

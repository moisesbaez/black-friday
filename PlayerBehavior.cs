using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	
	public Camera mainCamera;
	public AudioClip playerMoveSound;
	
	private enum GameState { RegisterOne, RegisterTwo, RegisterThree, GameOver, Moving }
	private GameState currentState;
	private GameState targetState;
	
	private Vector3 registerOnePosition;
	private Vector3 registerTwoPosition;
	private Vector3 registerThreePosition;
	
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float movementSpeed;
	private float rate;
	
	// Use this for initialization
	void Start ()
	{
		currentState = GameState.RegisterTwo;
		registerOnePosition = new Vector3(-5.0f, 2.25f, -8.8f);
		registerTwoPosition = new Vector3(0, 2.25f, -8.8f);
		registerThreePosition = new Vector3(5.0f, 2.25f, -8.8f);
		movementSpeed = 2.0f;
		rate = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		int inputDirection = (int)Input.GetAxisRaw("Horizontal");
		
		if(GameManager.GameOver)
			currentState = GameState.GameOver;
		
		switch(currentState)
		{
			case GameState.RegisterOne:
				if(inputDirection == 1)
				{
					startPosition = registerOnePosition;
					endPosition = registerTwoPosition;
					currentState = GameState.Moving;
					targetState = GameState.RegisterTwo;
					audio.PlayOneShot(playerMoveSound, 0.5f);
				}
				break;
			
			case GameState.RegisterTwo:
				if(inputDirection == -1)
				{
					startPosition = registerTwoPosition;
					endPosition = registerOnePosition;
					currentState = GameState.Moving;
					targetState = GameState.RegisterOne;
					audio.PlayOneShot(playerMoveSound, 0.5f);
				}
				else if(inputDirection == 1)
				{
					startPosition = registerTwoPosition;
					endPosition = registerThreePosition;
					currentState = GameState.Moving;
					targetState = GameState.RegisterThree;
					audio.PlayOneShot(playerMoveSound, 0.5f);
				}
				break;
			
			case GameState.RegisterThree:
				if(inputDirection == -1)
				{
					startPosition = registerThreePosition;
					endPosition = registerTwoPosition;
					currentState = GameState.Moving;
					targetState = GameState.RegisterTwo;
					audio.PlayOneShot(playerMoveSound, 0.5f);
				}
				break;
			
			case GameState.Moving:
				rate += Time.deltaTime * movementSpeed;
				mainCamera.transform.position = Vector3.Lerp(startPosition, endPosition, rate);
				if(rate >= 1.0f)
				{
					currentState = targetState;
					rate = 0.0f;
				}
				break;
			
			case GameState.GameOver:
				rate = 0.0f;
				if(!GameManager.GameOver)
				{
					mainCamera.transform.position = registerTwoPosition;
					currentState = GameState.RegisterTwo;
				}
				break;
		}
	
	}
}

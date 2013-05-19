using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	public List<GameObject> customerPrefabs = new List<GameObject>();
	public List<GameObject> itemPrefabs = new List<GameObject>();
	public RegisterLine registerOne;
	public RegisterLine registerTwo;
	public RegisterLine registerThree;
	public static int score;
	public GUIText ScoreBox;
	public GUISkin customSkin;
	
	private static float timer;
	private float spawnTime = 3.0f;
	private float timeElapsed = 2.0f;
	private static bool gameOver;
	
	public static bool GameOver
	{
		get { return gameOver; }
	}
	
	void Start ()
	{
		gameOver = false;
		timer = 0.0f;
		registerOne.GetComponent<RegisterLine>();
		registerTwo.GetComponent<RegisterLine>();
		registerThree.GetComponent<RegisterLine>();
		ScoreBox.GetComponent<GUIText>();
	}
	
	void Update ()
	{
		ScoreBox.text = score.ToString();
		
		if(timer > 30.0f)
		{
			gameOver = true;
			registerOne.ResetRegister();
			registerTwo.ResetRegister();
			registerThree.ResetRegister();
		}
		
		if(!gameOver)
		{
			timeElapsed += Time.deltaTime;
			timer += Time.deltaTime;
			
			if(timeElapsed >= spawnTime)
			{
				RegisterLine register = GetRegister();
				register.GenerateCustomer(customerPrefabs.ElementAt(Random.Range(0, customerPrefabs.Count)), itemPrefabs);
				timeElapsed = 0.0f;
			}
		}
	}
	
	RegisterLine GetRegister()
	{
		if(registerTwo.Count <= registerOne.Count && registerTwo.Count <= registerThree.Count)
			return registerTwo;
		else if(registerOne.Count <= registerTwo.Count && registerOne.Count <= registerThree.Count)
			return registerOne;
		else
		{
			return registerThree;
		}
	}
	
	void OnGUI()
	{
		GUI.skin = customSkin;
		if(gameOver)
		{
			GUI.Label(new Rect(441, 261, 398, 198), "game over", "Game Over Background");
			GUI.Label(new Rect(441, 321, 398, 198), "restart");
			if(GUI.Button(new Rect(500, 381, 100, 50), "yes", "Game Over Button"))
				ResetGame();
			if(GUI.Button(new Rect(680, 381, 100, 50), "no", "Game Over Button"))
			{
				ResetGame();
				Application.LoadLevel("Menu");
			}
		}
		
		GUI.Label(new Rect(850f, 635f, 321f, 37f), "", "Bar Background");
		GUI.Label(new Rect(854f, 638.5f, Mathf.Clamp(313f * (timer/30.0f), 0.0f, 313.0f), 27f), "", "Bar");
	}
	
	void ResetGame()
	{
		gameOver = false;
		timer = 0.0f;
		score = 0;
	}
	
	public static void IncreaseStress()
	{
		timer += 5.0f;
	}
	
	public static void DecreaseStress()
	{
		timer -= 7.0f;
	}
}

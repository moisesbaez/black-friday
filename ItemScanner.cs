using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ItemScanner : MonoBehaviour {
	
	public BagBehavior bagObject;
	public MoneyBehavior moneyObject;
	public AudioClip scanSound;
	
	private List<Color> regularMaterials;
	private Renderer childRenderer;
	
	void Start ()
	{
		regularMaterials = new List<Color>();
		childRenderer = GetComponentInChildren<Renderer>();
		bagObject.GetComponent<BagBehavior>();
		moneyObject.GetComponent<MoneyBehavior>();
	}
	
	void Update ()
	{
		if(GameManager.GameOver)
		{
			bagObject.ResetBag();
			moneyObject.ResetMoney();
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		GameManager.score += 10;
		audio.PlayOneShot(scanSound, 0.3f);
		Destroy(col.gameObject);
		
		bagObject.currentCount++;
		
		if(bagObject.IsFull)
			moneyObject.EnableMoney();
		
		for(int i = 0; i < childRenderer.materials.Length; i++)
			childRenderer.materials[i].color = regularMaterials[i];
	}
	
	void OnTriggerEnter()
	{
		for(int i = 0; i < childRenderer.materials.Length; i++)
		{
			regularMaterials.Add(childRenderer.materials[i].color);
			childRenderer.materials[i].color = Color.green;
		}
	}
}

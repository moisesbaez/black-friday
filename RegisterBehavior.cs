using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RegisterBehavior : MonoBehaviour {
	
	public BagBehavior bagObject;
	public AudioClip registerSound;
	
	private Renderer childRenderer;
	private List<Color> regularMaterials;
	
	void Start ()
	{
		regularMaterials = new List<Color>();
		childRenderer = GetComponentInChildren<Renderer>();
		bagObject.GetComponent<BagBehavior>();
	}
	
	void Update () {
	
	}
	
	void OnTriggerExit(Collider col)
	{
		GameManager.score += 20;
		audio.PlayOneShot(registerSound, 0.5f);
		MoneyBehavior moneyObject = col.GetComponent<MoneyBehavior>();
		moneyObject.ResetMoney();
		bagObject.EnableBag();
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

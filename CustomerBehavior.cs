using UnityEngine;
using System.Collections;

public class CustomerBehavior : MonoBehaviour {
	
	public int itemCount;
	public float frustrationLevel;
	
	public AudioClip customerFailSound;
	public AudioClip customerSuccessSound;
	
	
	private RegisterLine registerObject;
	private float timer;
	
	private SkinnedMeshRenderer skinRenderer;
	
	void Start ()
	{
		timer = 20.0f;
		registerObject = this.gameObject.transform.parent.GetComponent<RegisterLine>();
		skinRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
	}
	
	void Update ()
	{
		timer -= Time.deltaTime * frustrationLevel;
		if(timer <= 0.0f)
		{
			AudioSource.PlayClipAtPoint(customerFailSound, Vector3.zero, 0.7f);
			transform.parent.Find("Money").GetComponent<MoneyBehavior>().ResetMoney();
			transform.parent.Find("Bag").GetComponent<BagBehavior>().ResetBag();
			registerObject.RemoveCustomer(this.gameObject);
			GameManager.IncreaseStress();
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		AudioSource.PlayClipAtPoint(customerSuccessSound, Vector3.zero, 0.5f);
		col.GetComponent<BagBehavior>().ResetBag();
		GameManager.DecreaseStress();
		GameManager.score += 50;
		registerObject.RemoveCustomer(this.gameObject);
	}
	
	void OnTriggerStay()
	{
		skinRenderer.material.color = Color.green;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(registerObject.backgroundPosition.x,
								 registerObject.backgroundPosition.y,
								 registerObject.backgroundPosition.width,
								 registerObject.backgroundPosition.height), registerObject.frustrationBackground);
		GUI.DrawTexture(new Rect(registerObject.texturePosition.x,
								 registerObject.texturePosition.y,
								 registerObject.texturePosition.width * (timer/20.0f),
								 registerObject.texturePosition.height), registerObject.frustrationTexture);
	}
}

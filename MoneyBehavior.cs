using UnityEngine;
using System.Collections;

public class MoneyBehavior : MonoBehaviour {
	
	private Vector3 defaultPosition;
	
	void Start ()
	{
	}
	
	void Update ()
	{
	}
	
	public void EnableMoney()
	{
		this.gameObject.SetActive(true);	
	}
	
	public void ResetMoney()
	{
		//transform.localPosition = new Vector3(-1f, 1.5f, -6.3f);
		this.gameObject.SetActive(false);
	}
}

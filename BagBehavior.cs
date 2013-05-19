using UnityEngine;
using System.Collections;

public class BagBehavior : MonoBehaviour {
	
	public int itemCount;
	public int currentCount;
	
	private Vector3 defaultPosition;
	
	public bool IsFull
	{
		get { return itemCount == currentCount; }
	}
	
	void Start ()
	{
	}
	
	void Update ()
	{
	}
	
	public void EnableBag()
	{
		this.gameObject.SetActive(true);
	}
	
	public void ResetBag()
	{
		transform.localPosition = new Vector3(-1f, 1.5f, -6.3f);
			
		currentCount = 0;
		this.gameObject.SetActive(false);
	}
	
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RegisterLine : MonoBehaviour {
	
	public Texture2D frustrationTexture;
	public Texture2D frustrationBackground;
	public Rect texturePosition;
	public Rect backgroundPosition;
	
	private List<GameObject> registerLine;
	
	private bool isLineEmpty;
	private int lineCapacity;
	
	private float[] itemPositions = new float[] { -1.2f, -0.6f, 0.0f, 0.6f, 1.2f };
	private Dictionary<float, float> weightedItems;
	
	public int Count
	{
		get { return registerLine.Count; }
	}
	
	void Start ()
	{
		weightedItems = new Dictionary<float, float>();
		weightedItems.Add(1.5f, 7.0f);
		weightedItems.Add(2.0f, 10.0f);
		weightedItems.Add(2.5f, 6.0f);
		
		registerLine = new List<GameObject>();
		isLineEmpty = true;
		lineCapacity = 5;
	}
	
	void Update ()
	{
		if(registerLine.Count > 0 && isLineEmpty && !GameManager.GameOver)
		{
			this.gameObject.transform.Find("Bag").GetComponent<BagBehavior>().itemCount = registerLine.ElementAt(0).GetComponent<CustomerBehavior>().itemCount;
			registerLine.ElementAt(0).SetActive(true);
			isLineEmpty = false;
		}
	}
	
	public void RemoveCustomer(GameObject customer)
	{
		registerLine.Remove(customer);
		Destroy(customer);
		isLineEmpty = true;
	}
	
	public void GenerateCustomer(GameObject customer, List<GameObject> items)
	{
		if(registerLine.Count < lineCapacity)
		{
			GameObject newCustomer = (GameObject)Instantiate(customer);
			KeyValuePair<float, float> chosenItem = weightedItems.RandomElementByWeight(selector => selector.Value);
			GenerateItems(newCustomer, items);
			newCustomer.GetComponent<CustomerBehavior>().frustrationLevel = chosenItem.Key;
			newCustomer.SetActive(false);
			newCustomer.transform.parent = this.gameObject.transform;
			newCustomer.transform.localPosition = customer.transform.position;
			registerLine.Add(newCustomer);
		}
		
	}
	
	void GenerateItems(GameObject customer, List<GameObject> items)
	{
		int itemCount = Random.Range(1, 6);
		ShufflePositions(itemPositions);
		for(int i = 0; i < itemCount; i++)
		{
			customer.GetComponent<CustomerBehavior>().itemCount = itemCount;
			GameObject newItem = (GameObject)Instantiate(items.ElementAt(Random.Range(0, 5)));
			newItem.transform.position = new Vector3(itemPositions[i], newItem.transform.position.y, newItem.transform.position.z);
			newItem.transform.parent = customer.transform;
		}
	}
	
	void ShufflePositions(float[] itemPositions)
	{
		for(int i = itemPositions.Length; i > 1; i--)
		{
			int j = Random.Range(0, i);
			
			float temp = itemPositions[j];
			itemPositions[j] = itemPositions[i - 1];
			itemPositions[i - 1] = temp;
		}
	}
	
	public void ResetRegister()
	{
		for(int i = 0; i < registerLine.Count; i++)
			RemoveCustomer(registerLine[i]);
	}
}

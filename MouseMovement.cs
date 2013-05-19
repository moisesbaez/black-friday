using UnityEngine;
using System.Collections;

public class MouseMovement : MonoBehaviour {

	public Camera distanceCamera;
	
	Vector3 defaultPosition;
	Vector3 screenPosition;
	Vector3 offset;
	
	bool isDragging;
	
	void Start ()
	{
		isDragging = false;
		defaultPosition = transform.position;
	}
	
	void Update()
	{
		if(!isDragging)
			transform.position = defaultPosition;
	}
	
	void OnMouseDown()
	{
		isDragging = true;
		screenPosition = distanceCamera.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - distanceCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
	}
	
	void OnMouseDrag()
	{
		Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
		//Debug.Log(Camera.main.ScreenToWorldPoint(screenPoint) + offset);
		transform.position = distanceCamera.ScreenToWorldPoint(screenPoint) + offset;
	}
	
	void OnMouseUp()
	{
		isDragging = false;
	}
}

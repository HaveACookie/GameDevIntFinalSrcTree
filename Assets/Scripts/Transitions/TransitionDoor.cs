using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDoor : MonoBehaviour {

	//Settings
	private string door_type;
	
	//Variables
	private GameObject door;
	private Vector3 axis;

	private bool transition;
	private float door_angle;
	private float timer;
	
	//Initialization
	void Start () {
		//Create Door
		if (door_type == "")
		{
			door_type = "pDoorA";
		}

		door = Instantiate(Resources.Load<GameObject>("System/Transitions/" + door_type));
		door.transform.position = Vector3.zero;
		axis = door.GetComponent<MeshFilter>().mesh.bounds.ClosestPoint(new Vector3(-Mathf.Infinity, 0, 0));
		axis = new Vector3(axis.x, 0, axis.z);

		//Variables
		timer = 0;
		door_angle = 0.15f;
		transition = false;
	}
	
	//Update Event
	void Update () {
		//Timer
		timer += Time.deltaTime;
		
		//Rotate Door
		if (timer < 1f)
		{
			door_angle = Mathf.Lerp(door_angle, 0, 0.35f * Time.deltaTime);
			door.transform.RotateAround(axis, new Vector3(0, 1, 0), door_angle);
		}
		else if (timer > 1.45f)
		{
			door_angle = Mathf.Lerp(door_angle, 0.5f, 0.1f * Time.deltaTime);
			door.transform.RotateAround(axis, new Vector3(0, 1, 0), door_angle);
			transform.position = transform.position + new Vector3(0, 0, 0.025f);
		}
		else
		{
			door_angle = 1.2f;
		}
		
		//Transition
		if (timer > 3.2f)
		{
			if (!transition)
			{
				GameManager.instance.endTransition();
				transition = true;
			}
		}
	}
	
	//Setters
	public string doortype
	{
		set
		{
			door_type = value;
		}
	}
}

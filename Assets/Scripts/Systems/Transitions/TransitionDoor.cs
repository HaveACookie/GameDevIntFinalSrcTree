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
	/*
	 * Intent: Create the Door and set all the variables for the animation
	 */
	void Start () {
		//Pixilate
		if (GameManager.instance.pixel_effect)
		{
			gameObject.AddComponent<InnoPixelCamera>();
		}
		
		//Creates the Door
		if (door_type == "")
		{
			door_type = "pDoorA";
		}

		door = Instantiate(Resources.Load<GameObject>("System/Transitions/" + door_type));
		door.transform.position = Vector3.zero;
		
		//Gets the Most Left Axis of the Door
		axis = door.GetComponent<MeshFilter>().mesh.bounds.ClosestPoint(new Vector3(-Mathf.Infinity, 0, 0));
		axis = new Vector3(axis.x, 0, axis.z);

		//Variables
		timer = 0;
		door_angle = 0.15f;
		transition = false;
	}
	
	//Update Event
	/*
	 * Intent: Turns the door around an axis at certain periods of time for the door animation
	 */
	void Update () {
		//Timer
		timer += Time.deltaTime;
		
		//Rotates the Door
		if (timer < 1f)
		{
			//Turns the Door around the axis for a second
			//And makes the speed the door turning slower
			door_angle = Mathf.Lerp(door_angle, 0, 0.35f * Time.deltaTime);
			door.transform.RotateAround(axis, new Vector3(0, 1, 0), door_angle);
		}
		else if (timer > 1.45f)
		{
			//Turns the Door around the axis for a second again but after 1.45 seconds
			//And makes the speed the door turning slower again
			door_angle = Mathf.Lerp(door_angle, 0.5f, 0.1f * Time.deltaTime);
			door.transform.RotateAround(axis, new Vector3(0, 1, 0), door_angle);
			transform.position = transform.position + new Vector3(0, 0, 0.025f);
		}
		else
		{
			//Resets the Door angle speed
			door_angle = 1.2f;
		}
		
		//Transitions the Door after 3.2 Seconds
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
	/*
	 * Intent: Sets the door asset used in the Transition scene
	 */
	public string doortype
	{
		set
		{
			door_type = value;
		}
	}
}

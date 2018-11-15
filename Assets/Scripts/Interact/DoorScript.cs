using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractScript {

	//Settings
	[Header("Door Settings")]
	[SerializeField] private string door_id;
	[SerializeField] private Vector2 door_position;
	
	[Header("Transition Settings")]
	[SerializeField] private string trans_scene_name;
	[SerializeField] private string trans_door_id;
	[SerializeField] private string trans_door_type;

	protected override void init()
	{
		base.init();
		interact_tag = "Door";
	}
	
	public override void action()
	{
		GameManager.instance.switchScene(trans_scene_name, trans_door_id, trans_door_type);
	}

	public string id
	{
		get
		{
			return door_id;
		}
	}

	public Vector2 position
	{
		get
		{
			return transform.position + new Vector3(door_position.x, 0, door_position.y);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Vector3 pos = transform.position + new Vector3(door_position.x, 0, door_position.y);
		Gizmos.DrawSphere(pos, 0.15f);
	}
}

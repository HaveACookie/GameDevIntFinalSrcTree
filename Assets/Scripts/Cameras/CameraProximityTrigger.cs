using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProximityTrigger : CameraTrigger {

	//Components
	private PlayerBehaviour player;
	
	//Settings
	[SerializeField] private float trigger_radius = 1.5f;
	
	//Initialization
	void Start () {
		//Components
		base.cm = CameraManager.instance;
		player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
		gameObject.tag = "Finish";

		if (camera_id < 0)
		{
			gameObject.SetActive(false);
		}
	}
	
	//Update Event
	void Update () {
		if (cm.cam_active != camera_id) {
			if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.z)) < trigger_radius)
			{
				switchCamera();
			}
		}
	}

	//Debug
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		float first_point = 0;
		for (float i = 5; i < 360; i += 5f)
		{
			float second_point = i;
			Vector3 point_a = new Vector3(Mathf.Cos(first_point * Mathf.Deg2Rad), 0, Mathf.Sin(first_point * Mathf.Deg2Rad)) * trigger_radius;
			Vector3 point_b = new Vector3(Mathf.Cos(second_point * Mathf.Deg2Rad), 0, Mathf.Sin(second_point * Mathf.Deg2Rad)) * trigger_radius;
			Gizmos.DrawLine(transform.position + point_a, transform.position + point_b);
			first_point = second_point;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerEZ : CameraTrigger {

	//Components
	private Collider coll;
	
	//Settings
	[SerializeField] private Camera cam;
	
	void Start () {
		//Components
		cm = CameraManager.instance;
		gameObject.tag = "Finish";
		
		if (cam != null)
		{
			base.camera_id = cm.getCameraNum(cam.gameObject);
		}
		else
		{
			base.camera_id = -1;
		}
		
		//Collider
		if (camera_id < 0)
		{
			gameObject.SetActive(false);
		}

		if (GetComponent<Collider>() == null)
		{
			gameObject.SetActive(false);
		}
		else
		{
			coll = GetComponent<Collider>();
			coll.isTrigger = true;
		}
	}

	void OnDrawGizmosSelected()
	{
		if (cam != null)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(cam.gameObject.transform.position, 1.5f);
			Gizmos.DrawLine(cam.gameObject.transform.position, transform.position);
			Gizmos.DrawSphere(transform.position, 0.5f);
		}
	}
}

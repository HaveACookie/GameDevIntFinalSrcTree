using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

	//Components
	protected CameraManager cm;
	private Collider col;
	
	//Settings
	[SerializeField] protected int camera_id = -1;
	
	//Initialization
	void Start () {
		//Components
		cm = CameraManager.instance;
		gameObject.tag = "Finish";

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
			col = GetComponent<Collider>();
			col.isTrigger = true;
		}
	}
	
	//Methods
	public void switchCamera()
	{
		cm.changeCamera(camera_id);
	}
}

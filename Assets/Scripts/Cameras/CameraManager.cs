using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	//Singleton
	public static CameraManager instance { get; private set; }
	
	//Settings
	public int cam_active { get; private set; }
	private bool set_pixilate;
	private GameObject[] cameras;
	
	//Initialization
	void Awake ()
	{
		//Singleton
		instance = this;
		
		//Settings
		cam_active = -1;
		set_pixilate = false;
			
		//Get Cameras Array
		int children = transform.childCount;
		cameras = new GameObject[children];
		for (int i = 0; i < children; i++)
		{
			cameras[i] = transform.GetChild(i).gameObject;
			cameras[i].tag = "Untagged";
			cameras[i].SetActive(false);
		}
	}
	
	//Update Event
	void FixedUpdate () {
		if (set_pixilate)
		{
			cameras[cam_active].AddComponent<InnoPixelCamera>();
			cameras[cam_active].GetComponent<InnoPixelCamera>().referenceHeight = 270;
			cameras[cam_active].GetComponent<InnoPixelCamera>().pixelsPerUnit = 32;
			set_pixilate = false;
		}
	}
	
	//Methods
	public void changeCamera(int camera_id)
	{
		for (int i = 0; i < cameras.Length; i++)
		{
			cameras[i].tag = "Untagged";
			if (cameras[i].GetComponent<InnoPixelCamera>() != null)
			{
				Destroy(cameras[i].GetComponent<InnoPixelCamera>());
			}
			cameras[i].SetActive(false);
		}
		
		cam_active = camera_id;
		cameras[camera_id].tag = "MainCamera";
		cameras[camera_id].SetActive(true);
		set_pixilate = true;
	}

	public int getCameraNum(GameObject camera)
	{
		for (int i = 0; i < cameras.Length; i++)
		{
			if (camera == cameras[i])
			{
				return i;
			}
		}
		return -1;
	}
	
}

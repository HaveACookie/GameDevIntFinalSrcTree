using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//Settings
	public static GameManager instance { get; private set; }
	
	public string save_slot { get; private set; }
	public InventoryManager inventory { get; private set; }

	//Variables
	

	//Init
	void Awake () {
		//Destroy Manager if another exists in scene
		if (GameObject.FindWithTag("GameController") != null)
		{
			Destroy(gameObject);
		}
		else
		{
			//Instantiate Game Manager
			gameObject.tag = "GameController";
			DontDestroyOnLoad(gameObject);
			inventory = gameObject.AddComponent<InventoryManager>();
			instance = this;
		}
	}
	
	//Update
	void Update () {
		
	}
	
	//Input
	public bool getKey(string input)
	{
		if (input == "up")
		{
			if (Input.GetKey(KeyCode.W))
			{
				return true;
			}
		}
		else if (input == "down")
		{
			if (Input.GetKey(KeyCode.S))
			{
				return true;
			}
		}
		else if (input == "left")
		{
			if (Input.GetKey(KeyCode.A))
			{
				return true;
			}
		}
		else if (input == "right")
		{
			if (Input.GetKey(KeyCode.D))
			{
				return true;
			}
		}
		else if (input == "interact")
		{
			if (Input.GetKey(KeyCode.E))
			{
				return true;
			}
		}
		else if (input == "inventory")
		{
			if (Input.GetKey(KeyCode.Q))
			{
				return true;
			}
		}
		else if (input == "aim")
		{
			if (Input.GetMouseButton(1))
			{
				return true;
			}
		}
		else if (input == "attack")
		{
			if (Input.GetMouseButton(0))
			{
				return true;
			}
		}
		else if (input == "run")
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				return true;
			}
		}
			
		return false;
	}
	
	public bool getKeyDown(string input)
	{
		if (input == "up")
		{
			if (Input.GetKeyDown(KeyCode.W))
			{
				return true;
			}
		}
		else if (input == "down")
		{
			if (Input.GetKeyDown(KeyCode.S))
			{
				return true;
			}
		}
		else if (input == "left")
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				return true;
			}
		}
		else if (input == "right")
		{
			if (Input.GetKeyDown(KeyCode.D))
			{
				return true;
			}
		}
		else if (input == "interact")
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				return true;
			}
		}
		else if (input == "inventory")
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				return true;
			}
		}
		else if (input == "aim")
		{
			if (Input.GetMouseButtonDown(1))
			{
				return true;
			}
		}
		else if (input == "attack")
		{
			if (Input.GetMouseButtonDown(0))
			{
				return true;
			}
		}
		else if (input == "run")
		{
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				return true;
			}
		}
			
		return false;
	}
	
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//Settings
	public static GameManager instance { get; private set; }

	//Variables
	public int player_equip { get; private set; }
	
	public int[] inventory { get; private set; }
	public int[] inventory_stock { get; private set; }
	
	public int[] storage { get; private set; }
	public int[] storage_stock { get; private set; }

	

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
			instance = this;
		}
	}
	
	//Update
	void Update () {
		
	}
	
	//Inventory Methods
	public void changeEquip(int inventory_slot)
	{
		player_equip = inventory_slot;
		PlayerBehaviour player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
		player.item_equip = inventory_slot;
	}
	
	public bool addItem(int item_index)
	{
		for (int i = 0; i < inventory.Length; i++)
		{
			if (inventory[i] == 0)
			{
				inventory[i] = item_index;
				inventory_stock[i] = 0;
				return true;
			}
		}

		return false;
	}
	
	public bool addItem(int item_index, int item_stock)
	{
		for (int i = 0; i < inventory.Length; i++)
		{
			if (inventory[i] == 0)
			{
				inventory[i] = item_index;
				inventory_stock[i] = item_stock;
				return true;
			}
		}

		return false;
	}

	public bool removeItem(int item_index)
	{
		for (int i = 0; i < inventory.Length; i++)
		{
			if (inventory[i] == item_index)
			{
				inventory[i] = 0;
				inventory_stock[i] = 0;
				return true;
			}
		}

		return false;
	}

	public bool combineItems(int index_a, int index_b)
	{
		if (inventory[index_a] == inventory[index_b])
		{
			inventory_stock[index_a] += inventory_stock[index_b];
			inventory_stock[index_b] = 0;
			inventory[index_b] = 0;
			return true;
		}
		else if (inventory[index_a] == 0)
		{
			inventory_stock[index_a] = inventory_stock[index_b];
			inventory[index_a] = inventory[index_b] = 0;
			inventory_stock[index_b] = 0;
			inventory[index_b] = 0;
			return true;
		}
		return false;
	}
	
	public bool combineItemsStorage(int index_a, int index_b)
	{
		if (storage[index_a] == storage[index_b])
		{
			storage_stock[index_a] += storage_stock[index_b];
			storage_stock[index_b] = 0;
			storage[index_b] = 0;
			return true;
		}
		else if (storage[index_a] == 0)
		{
			storage_stock[index_a] = storage_stock[index_b];
			storage[index_a] = storage[index_b];
			storage_stock[index_b] = 0;
			storage[index_b] = 0;
			return true;
		}
		return false;
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
	
}
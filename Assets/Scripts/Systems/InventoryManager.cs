using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    //Settings
	public int player_equip { get; private set; }
	
	public int[] inventory { get; private set; }
	public int[] inventory_stock { get; private set; }
	
	public int[] storage { get; private set; }
	public int[] storage_stock { get; private set; }
	
	//Initialization
	void Awake()
	{
		player_equip = -1;
		
		inventory = new int[6];
		inventory_stock = new int[6];

		storage = new int[64];
		storage_stock = new int[64];
	}
	
	//Debug
	public void debugScramble()
	{
		player_equip = Random.Range(0, 5);
		for (int i = 0; i < 6; i++)
		{
			inventory[i] = Random.Range(0, 72);
			inventory_stock[i] = Random.Range(0, 50);
		}
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
	
}

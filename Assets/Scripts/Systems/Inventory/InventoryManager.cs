using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

	//Health
	public int health { get; private set; } //The health of the player between 0 and 4
	public bool poison { get; private set; } //The poison of the
	
    //Inventory
	public int player_equip { get; private set; }
	
	public int[] inventory { get; private set; }
	public int[] inventory_stock { get; private set; }
	
	public int[] storage { get; private set; }
	public int[] storage_stock { get; private set; }
	
	//Initialization
	void Awake()
	{
		//Health
		health = 2;
		poison = false;
		
		//Inventory
		player_equip = -1;
		
		inventory = new int[6];
		inventory_stock = new int[6];

		storage = new int[64];
		storage_stock = new int[64];
		
		//Debug (Remove Later)
		//Guns
		inventory[0] = 2;
		inventory[1] = 9;
		inventory_stock[1] = 5;
		inventory[2] = 9;
		inventory_stock[2] = 20;
		inventory[3] = 61;
		inventory[4] = 61;
		
		//Herbs
		/*
		inventory[0] = 61;
		inventory[1] = 61;
		inventory[2] = 61;
		inventory[3] = 62;
		inventory[4] = 63;
		inventory[5] = 30;
		*/
		
		//Chemicals
		/*
		inventory[0] = 39;
		inventory[1] = 40;
		inventory[2] = 41;
		inventory[3] = 42;
		*/
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
	
	//Health Methods
	public void heal(int heal_amount)
	{
		health = heal_amount;
		health = Mathf.Clamp(health, 0, 4);
	}
	
	public void cure()
	{
		poison = false;
	}
	
	//Inventory Methods
	public void changeEquip(int inventory_slot)
	{
		player_equip = inventory_slot;
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

	public bool checkItem(int item_index)
	{
		for (int i = 0; i < inventory.Length; i++)
		{
			if (inventory[i] == item_index)
			{
				return true;
			}
		}

		return false;
	}
	
	public void addItemAtIndex(int item, int item_index)
	{
		inventory[item_index] = item;
		inventory_stock[item_index] = 0;
	}
	
	public void addItemAtIndex(int item, int item_stock, int item_index)
	{
		inventory[item_index] = item;
		inventory_stock[item_index] = item_stock;
	}

	public bool removeItem(int item_index)
	{
		for (int i = 0; i < inventory.Length; i++)
		{
			if (inventory[i] == item_index)
			{
				inventory[i] = 0;
				inventory_stock[i] = 0;
				if (player_equip == i)
				{
					player_equip = -1;
				}
				return true;
			}
		}

		return false;
	}
	
	public void removeItemAtIndex(int item_index)
	{
		inventory[item_index] = 0;
		inventory_stock[item_index] = 0;
		if (player_equip == item_index)
		{
			player_equip = -1;
		}
	}

	public int combineChemicals(int index_a, int index_b)
	{
		if ((inventory[index_a] > 38 && inventory[index_a] < 46) && (inventory[index_b] > 38 && inventory[index_b] < 46))
		{
			if (inventory[index_a] == 39)
			{
				//Water
				if (inventory[index_b] == 40)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 42;
					inventory_stock[index_b] = 0;
					return 1;
				}
			}
			else if (inventory[index_a] == 40)
			{
				//UMB No2
				if (inventory[index_b] == 39)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 42;
					inventory_stock[index_b] = 0;
					return 1;
				}
				if (inventory[index_b] == 41)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 43;
					inventory_stock[index_b] = 0;
					return 1;
				}
			}
			else if (inventory[index_a] == 41)
			{
				//UMB No4
				if (inventory[index_b] == 40)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 43;
					inventory_stock[index_b] = 0;
					return 1;
				}
				if (inventory[index_b] == 42)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 44;
					inventory_stock[index_b] = 0;
					return 1;
				}
			}
			else if (inventory[index_a] == 42)
			{
				//NP 003
				if (inventory[index_b] == 41)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 44;
					inventory_stock[index_b] = 0;
					return 1;
				}
				if (inventory[index_b] == 45)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 46;
					inventory_stock[index_b] = 0;
					return 1;
				}
			}
			else if (inventory[index_a] == 43)
			{
				//Yellow 6
				if (inventory[index_b] == 44)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 45;
					inventory_stock[index_b] = 0;
					return 1;
				}
			}
			else if (inventory[index_a] == 44)
			{
				//UMB No7
				if (inventory[index_b] == 43)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 45;
					inventory_stock[index_b] = 0;
					return 1;
				}
			}
			else if (inventory[index_a] == 45)
			{
				//UMB No13
				if (inventory[index_b] == 42)
				{
					inventory[index_a] = 38;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 46;
					inventory_stock[index_b] = 0;
					return 1;
				}
			}
		}
		else
		{
			return 0;
		}
		
		return -1;
	}

	public bool craftHealing(int index_a, int index_b)
	{
		if (inventory[index_a] == 61)
		{
			//Green Herb
			switch (inventory[index_b])
			{
				case 61:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 66;
					inventory_stock[index_b] = 0;
					return true;
				case 62:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 67;
					inventory_stock[index_b] = 0;
					return true;
				case 63:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 68;
					inventory_stock[index_b] = 0;
					return true;
				case 66:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 70;
					inventory_stock[index_b] = 0;
					return true;
				case 68:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 69;
					inventory_stock[index_b] = 0;
					return true;
				default :
					return false;
			}
		}
		else if (inventory[index_a] == 62)
		{
			//Red Herb
			switch (inventory[index_b])
			{
				case 61:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 67;
					inventory_stock[index_b] = 0;
					return true;
				case 68:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 70;
					inventory_stock[index_b] = 0;
					return true;
				default :
					return false;
			}
		}
		else if (inventory[index_a] == 63)
		{
			//Blue Herb
			switch (inventory[index_b])
			{
				case 61:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 68;
					inventory_stock[index_b] = 0;
					return true;
				case 66:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 69;
					inventory_stock[index_b] = 0;
					return true;
				case 67:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 71;
					inventory_stock[index_b] = 0;
					return true;
				default :
					return false;
			}
		}
		else if (inventory[index_a] == 66)
		{
			//Mix GG
			switch (inventory[index_b])
			{
				case 61:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 70;
					inventory_stock[index_b] = 0;
					return true;
				case 63:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 69;
					inventory_stock[index_b] = 0;
					return true;
				default :
					return false;
			}
		}
		else if (inventory[index_a] == 67)
		{
			//Mix GR
			switch (inventory[index_b])
			{
				case 63:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 71;
					inventory_stock[index_b] = 0;
					return true;
				default :
					return false;
			}
		}
		else if (inventory[index_a] == 68)
		{
			//Mix GB
			switch (inventory[index_b])
			{
				case 61:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 69;
					inventory_stock[index_b] = 0;
					return true;
				case 62:
					inventory[index_a] = 0;
					inventory_stock[index_a] = 0;
					inventory[index_b] = 71;
					inventory_stock[index_b] = 0;
					return true;
				default :
					return false;
			}
		}
		
		return false;
	}

	public int reloadItem(int index_a, int index_b)
	{
		if (inventory[index_a] == 2 || inventory[index_a] == 3)
		{
			//Pistol
			if (inventory[index_b] == 9)
			{
				if (inventory_stock[index_a] < 16)
				{
					int refill = 16 - inventory_stock[index_a];
					transferStock(index_b, index_a, refill);
					return 1;
				}
			}
		}
		else if (inventory[index_a] == 4)
		{
			//Shotgun
			if (inventory[index_b] == 10)
			{
				if (inventory_stock[index_a] < 7)
				{
					int refill = 7 - inventory_stock[index_a];
					transferStock(index_b, index_a, refill);
					return 1;
				}
			}
		}
		else if (inventory[index_a] == 5)
		{
			//Bazooka
			if (inventory[index_b] > 10 && inventory[index_b] < 14)
			{
				if (inventory_stock[index_a] < 6)
				{
					int refill = 6 - inventory_stock[index_a];
					transferStock(index_b, index_a, refill);
					return 1;
				}
			}
		}
		else if (inventory[index_a] == 6)
		{
			//Revolver
			if (inventory[index_b] == 14)
			{
				if (inventory_stock[index_a] < 6)
				{
					int refill = 6 - inventory_stock[index_a];
					transferStock(index_b, index_a, refill);
					return 1;
				}
			}
		}
		else
		{
			return -1;
		}

		return 0;
	}

	public void transferStock(int index_a, int index_b, int transfer)
	{
		int transfer_amount = Mathf.Clamp(inventory_stock[index_a], 0, transfer);
		inventory_stock[index_a] -= transfer_amount;
		inventory_stock[index_b] += transfer_amount;
		if (inventory_stock[index_a] <= 0)
		{
			inventory[index_a] = 0;
			inventory_stock[index_a] = 0;
		}
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

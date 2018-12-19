using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {

	//Settings
	private int save_num;
	private List<string> scene_names;
	
	//Save
	private Dictionary<string, bool> player_save;
	
	//Indexes
	private List<GameObject> items;
	private List<GameObject> enemies;
	private List<GameObject> puzzles;
	
	//Initialization
	void Awake()
	{
		//Get all scene names
		int scene_count = SceneManager.sceneCount;
		scene_names = new List<string>();
		for (int i = 0; i < scene_count; i++)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			scene_names.Add(scene.name);
		}
		
		player_save = new Dictionary<string, bool>();
	}
	
	//Scene Functions
	public void indexContent()
	{
		//Create Lists
		items = new List<GameObject>();
		enemies = new List<GameObject>();
		puzzles = new List<GameObject>();

		//Interacts
		GameObject[] interacts = GameObject.FindGameObjectsWithTag("Interact");
		foreach (GameObject interact in interacts)
		{
			InteractScript insect = interact.GetComponent<InteractScript>();
			if (insect.compareInteractTag("Item"))
			{
				items.Add(interact);
			}
			else if (insect.compareInteractTag("Puzzle"))
			{
				puzzles.Add(interact);
			}
		}

		items = sortByPosition(items);
		puzzles = sortByPosition(puzzles);
		
		//Enemies
		GameObject[] enemies_ = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies_)
		{
			enemies.Add(enemy);
		}

		enemies = sortByPosition(enemies);
	}

	public void cleanIndexedContent()
	{
		//Clean Items
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i] != null)
			{
				if (getItem(i) && items[i].GetComponent<InteractScript>().index)
				{
					GameObject temp = items[i];
					items[i] = null;
					Destroy(temp);
				}
			}
		}
		
		//Clean Enemies
		for (int i = 0; i < enemies.Count; i++)
		{
			if (enemies[i] != null)
			{
				if (getEnemy(i))
				{
					GameObject temp = enemies[i];
					enemies[i] = null;
					DestroyImmediate(temp);
				}
			}
		}
		
		//Clean Puzzles
		for (int i = 0; i < puzzles.Count; i++)
		{
			if (puzzles[i] != null)
			{
				if (getPuzzle(i) && puzzles[i].GetComponent<InteractScript>().index)
				{
					GameObject temp = puzzles[i];
					puzzles[i] = null;
					Destroy(temp);
				}
			}
		}
	}
	
	//Load Functions
	public bool getItem(int item_index)
	{
		string index_name = save_num.ToString() + SceneManager.GetActiveScene().name + "_item_" + item_index.ToString();
		bool key_value;
		if (!player_save.TryGetValue(index_name, out key_value))
		{
			if (PlayerPrefs.HasKey(index_name))
			{
				return intToBool(PlayerPrefs.GetInt(index_name));
			}
			return false;
		}
		return key_value;
	}
	
	public bool getEnemy(int item_index)
	{
		string index_name = save_num.ToString() + SceneManager.GetActiveScene().name + "_enemy_" + item_index.ToString();
		bool key_value;
		if (!player_save.TryGetValue(index_name, out key_value))
		{
			if (PlayerPrefs.HasKey(index_name))
			{
				return intToBool(PlayerPrefs.GetInt(index_name));
			}
			return false;
		}
		return key_value;
	}
	
	public bool getPuzzle(int item_index)
	{
		string index_name = save_num.ToString() + SceneManager.GetActiveScene().name + "_puzzle_" + item_index.ToString();
		bool key_value;
		if (!player_save.TryGetValue(index_name, out key_value))
		{
			if (PlayerPrefs.HasKey(index_name))
			{
				return intToBool(PlayerPrefs.GetInt(index_name));
			}
			return false;
		}
		return key_value;
	}

	//Save Functions
	public void saveItem(GameObject item, bool value)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (item == items[i])
			{
				saveItem(i, value);
			}
		}
	}
	
	public void saveEnemy(GameObject enemy, bool value)
	{
		for (int i = 0; i < enemies.Count; i++)
		{
			if (enemy == enemies[i])
			{
				saveEnemy(i, value);
			}
		}
	}
	
	public void savePuzzle(GameObject puzzle, bool value)
	{
		for (int i = 0; i < puzzles.Count; i++)
		{
			if (puzzle == puzzles[i])
			{
				savePuzzle(i, value);
			}
		}
	}
	
	public void saveItem(int item_index, bool value)
	{
		string index_name = save_num.ToString() + SceneManager.GetActiveScene().name + "_item_" + item_index.ToString();
		player_save.Add(index_name, value);
	}
	
	public void saveEnemy(int enemy_index, bool value)
	{
		string index_name = save_num.ToString() + SceneManager.GetActiveScene().name + "_enemy_" + enemy_index.ToString();
		player_save.Add(index_name, value);
	}
	
	public void savePuzzle(int puzzle_index, bool value)
	{
		string index_name = save_num.ToString() + SceneManager.GetActiveScene().name + "_puzzle_" + puzzle_index.ToString();
		player_save.Add(index_name, value);
	}
	
	//Save Bulk Data
	public void saveGame()
	{
		//Save Exists
		PlayerPrefs.SetInt(save_num.ToString() + "save_exists", 1);
		
		//Save Player Position
		Vector3 player_position = GameObject.FindWithTag("Player").transform.position;
		float player_rotation = GameObject.FindWithTag("Player").transform.eulerAngles.y;
		PlayerPrefs.SetFloat(save_num.ToString() + "player_position_x", player_position.x);
		PlayerPrefs.SetFloat(save_num.ToString() + "player_position_y", player_position.y);
		PlayerPrefs.SetFloat(save_num.ToString() + "player_position_z", player_position.z);
		PlayerPrefs.SetFloat(save_num.ToString() + "player_rotation", player_rotation);
		
		//Save Inventory
		InventoryManager inventory = GetComponent<InventoryManager>();
		PlayerPrefs.SetInt(save_num.ToString() + "player_health", inventory.health);
		PlayerPrefs.SetInt(save_num.ToString() + "player_poison", boolToInt(inventory.poison));

		PlayerPrefs.SetInt(save_num.ToString() + "equip", inventory.player_equip);
		for (int i = 0; i < inventory.inventory.Length; i++)
		{
			PlayerPrefs.SetInt(save_num.ToString() + "inventory_" + i.ToString(), inventory.inventory[i]);
		}
		for (int i = 0; i < inventory.inventory_stock.Length; i++)
		{
			PlayerPrefs.SetInt(save_num.ToString() + "inventory_stock_" + i.ToString(), inventory.inventory_stock[i]);
		}
		for (int i = 0; i < inventory.storage.Length; i++)
		{
			PlayerPrefs.SetInt(save_num.ToString() + "storage_" + i.ToString(), inventory.storage[i]);
		}
		for (int i = 0; i < inventory.storage_stock.Length; i++)
		{
			PlayerPrefs.SetInt(save_num.ToString() + "storage_stock_" + i.ToString(), inventory.storage_stock[i]);
		}

		//Save Backlog
		foreach (KeyValuePair<string, bool> entry in player_save)
		{
			PlayerPrefs.SetInt(entry.Key, boolToInt(entry.Value));
		}

		//Save
		PlayerPrefs.Save();
	}

	//Bulk Data Functions
	public void copySave(int index)
	{
		//Save Exists
		PlayerPrefs.SetInt(save_num.ToString() + "save_exists", PlayerPrefs.GetInt(index.ToString() + "save_exists"));
		
		//Save Player Position
		PlayerPrefs.SetFloat(save_num.ToString() + "player_position_x", PlayerPrefs.GetFloat(index.ToString() + "player_position_x"));
		PlayerPrefs.SetFloat(save_num.ToString() + "player_position_y", PlayerPrefs.GetFloat(index.ToString() + "player_position_y"));
		PlayerPrefs.SetFloat(save_num.ToString() + "player_position_z", PlayerPrefs.GetFloat(index.ToString() + "player_position_z"));
		PlayerPrefs.SetFloat(save_num.ToString() + "player_rotation", PlayerPrefs.GetFloat(index.ToString() + "player_rotation"));
		
		//Save Inventory
		InventoryManager inventory = GetComponent<InventoryManager>();
		PlayerPrefs.SetInt(save_num.ToString() + "player_health", PlayerPrefs.GetInt(index.ToString() + "player_health"));
		PlayerPrefs.SetInt(save_num.ToString() + "player_poison", PlayerPrefs.GetInt(index.ToString() + "player_poison"));

		PlayerPrefs.SetInt(save_num.ToString() + "equip", inventory.player_equip);
		for (int i = 0; i < inventory.inventory.Length; i++)
		{
			PlayerPrefs.SetInt(save_num.ToString() + "inventory_" + i.ToString(), PlayerPrefs.GetInt(index.ToString() + "inventory_" + i.ToString()));
		}
		for (int i = 0; i < inventory.inventory_stock.Length; i++)
		{
			PlayerPrefs.SetInt(save_num.ToString() + "inventory_stock_" + i.ToString(), PlayerPrefs.GetInt(index.ToString() + "inventory_stock_" + i.ToString()));
		}
		for (int i = 0; i < inventory.storage.Length; i++)
		{
			PlayerPrefs.SetInt(save_num.ToString() + "storage_" + i.ToString(), PlayerPrefs.GetInt(index.ToString() + "storage_" + i.ToString()));
		}
		for (int i = 0; i < inventory.storage_stock.Length; i++)
		{
			PlayerPrefs.SetInt(save_num.ToString() + "storage_stock_" + i.ToString(), PlayerPrefs.GetInt(index.ToString() + "storage_stock_" + i.ToString()));
		}

		save_num = index;
		saveGame();
	}
	
	public void deleteSave(int index)
	{
		List<string> indexes = new List<string>();
		
		//Delete PlayerData
		indexes.Add(index.ToString() + "save_exists");
		indexes.Add(index.ToString() + "player_position_x");
		indexes.Add(index.ToString() + "player_position_y");
		indexes.Add(index.ToString() + "player_position_z");
		indexes.Add(index.ToString() + "player_rotation");
		
		indexes.Add(index.ToString() + "player_health");
		indexes.Add(index.ToString() + "player_poison");
		
		indexes.Add(index.ToString() + "equip");
		InventoryManager inventory = GetComponent<InventoryManager>();
		for (int i = 0; i < inventory.inventory.Length; i++)
		{
			indexes.Add(index.ToString() + "inventory_" + i.ToString());
		}
		for (int i = 0; i < inventory.inventory_stock.Length; i++)
		{
			indexes.Add(index.ToString() + "inventory_stock_" + i.ToString());
		}
		for (int i = 0; i < inventory.storage.Length; i++)
		{
			indexes.Add(index.ToString() + "storage_" + i.ToString());
		}
		for (int i = 0; i < inventory.storage_stock.Length; i++)
		{
			indexes.Add(index.ToString() + "storage_stock_" + i.ToString());
		}

		foreach (string temp_key in indexes)
		{
			if (PlayerPrefs.HasKey(temp_key))
			{
				PlayerPrefs.DeleteKey(temp_key);
			}
		}
		
		foreach (string name in scene_names)
		{
			string scene_key = index.ToString() + name;
			
			//Delete Puzzle Indexes
			int i = 0;
			while (PlayerPrefs.HasKey(scene_key + "_puzzle_" + i.ToString()))
			{
				PlayerPrefs.DeleteKey(scene_key + "_puzzle_" + i.ToString());
				i++;
			}
			
			//Delete Item Indexes
			i = 0;
			while (PlayerPrefs.HasKey(scene_key + "_item_" + i.ToString()))
			{
				PlayerPrefs.DeleteKey(scene_key + "_item_" + i.ToString());
				i++;
			}
			
			//Delete Enemy Indexes
			i = 0;
			while (PlayerPrefs.HasKey(scene_key + "_enemy_" + i.ToString()))
			{
				PlayerPrefs.DeleteKey(scene_key + "_enemy_" + i.ToString());
				i++;
			}
		}
	}
	
	//Misc Methods
	private int boolToInt(bool check)
	{
		if (!check)
		{
			return 0;
		}
		return 1;
	}
	
	private bool intToBool(int check)
	{
		if (check == 0)
		{
			return false;
		}
		return true;
	}

	private List<GameObject> sortByPosition(List<GameObject> list)
	{
		bool swapped = true;
		int j = 0;
		GameObject tmp;
		while (swapped) {
			swapped = false;
			j++;
			for (int i = 0; i < list.Count - j; i++) {
				if (list[i].transform.position.x > list[i + 1].transform.position.x) {
					tmp = list[i];
					list[i] = list[i + 1];
					list[i + 1] = tmp;
					swapped = true;
				}
			}
		}
		return list;
	}
}

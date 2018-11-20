using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//Components
	public static GameManager instance { get; private set; }
	
	public string save_slot { get; private set; }
	public InventoryManager inventory { get; private set; }
	public EventManager events { get; private set; }
	
	//Settings
	[SerializeField] private bool pixelate_effect;

	//Variables
	private string scene_load;
	private string door_index;
	private string door_type_index;

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
			events = gameObject.AddComponent<EventManager>();
			instance = this;
		}
		
		//Debug
		pixelate_effect = true;
	}
	
	//Update
	void Update () {
		
	}
	
	//Scene Switch
	public void switchScene(string scene_name, string door_id)
	{
		scene_load = scene_name;
		door_index = door_id;
		
		GameObject new_canvas = createOverlay();
		new_canvas.AddComponent<SceneTransition>().fadein = true;
		new_canvas.GetComponent<SceneTransition>().scene = "Transition";
	}

	public void switchScene(string scene_name, string door_id, string door_type)
	{
		door_type_index = door_type;
		switchScene(scene_name, door_id);
	}

	public void endTransition()
	{
		GameObject new_canvas = createOverlay();
		new_canvas.AddComponent<SceneTransition>().fadein = true;
		new_canvas.GetComponent<SceneTransition>().scene = scene_load;
	}

	public void createFadeout()
	{
		GameObject new_canvas = createOverlay();
		new_canvas.AddComponent<SceneTransition>().fadein = false;
	}

	private GameObject createOverlay()
	{
		GameObject new_canvas = new GameObject("Transition", typeof(Canvas));
		new_canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
		new_canvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		new_canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
		new_canvas.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
		new_canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
		new_canvas.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0, 0);
		return new_canvas;
	}
	
	//Scene Management
	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
 
	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
 
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		//Scene Load Event
		createFadeout();
		if (scene.name == "Transition")
		{
			if (door_type_index == null)
			{
				door_type_index = "";
			}
			GameObject.FindWithTag("MainCamera").AddComponent<TransitionDoor>().doortype = door_type_index;
			door_type_index = null;
		}
		else if (scene.name == scene_load)
		{
			scene_load = null;

			List<DoorScript> doors = new List<DoorScript>();
			GameObject[] interact = GameObject.FindGameObjectsWithTag("Interact");
			foreach (GameObject act in interact)
			{
				if (act.GetComponent<InteractScript>().compareInteractTag("Door"))
				{
					doors.Add(act.GetComponent<DoorScript>());
				}
			}

			GameObject player = GameObject.FindWithTag("Player");

			foreach (DoorScript door in doors)
			{
				if (door.id == door_index)
				{
					player.transform.position = door.position;
					float door_angle = -((Mathf.Atan2(player.transform.position.z - door.transform.position.z, player.transform.position.x - door.transform.position.x) * 180) / Mathf.PI) + 90;
					player.transform.eulerAngles = new Vector3(0, door_angle, 0);
					break;
				}
			}
		}
		
		//Index Content and Clean
		events.indexContent();
		events.cleanIndexedContent();
	}
	
	//Getter & Setters
	public bool pixel_effect
	{
		get
		{
			return pixelate_effect;
		}
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
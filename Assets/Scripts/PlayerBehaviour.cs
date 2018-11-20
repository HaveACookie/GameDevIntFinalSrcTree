using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour {

	//Components
	private GameManager gm;  //Game Manager Singleton
	private Rigidbody rb;  //Player Physics Rigidbody
	//private HealthPoints hp;
	
	//Settings
	[SerializeField] private float spd;  //Player Speed
	[SerializeField] private float run_spd; //Player Running Speed
	[SerializeField] private float turn_spd; //Player Turning Speed
	[SerializeField] private float back_spd; //Player Moving Backwards Speed

	private bool can_move;  //Turn Player Input on and off
	
	//Variables
	private Vector2 velocity;  //Player Physics movement in the x and z axis
	
	//Init Player
	void Awake()
	{
		gameObject.tag = "Player";
	}
	
	void Start () {
		//Components
		gm = GameManager.instance;
		rb = gameObject.GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeRotation;
		//hp = gameObject.GetComponent<HealthPoints>();
		
		//Settings
		can_move = true;

		//Variables
		velocity = Vector2.zero;
	}
	
	//Update Event
	void Update () {
		//Player Action
		velocity = Vector2.zero;  //Reset the player velocity to zero
		if (can_move)
		{
			//Debug (This block of code will be deleted later)
			if (Input.GetKeyDown(KeyCode.K))
			{
				//pickupItem(30, 0, new GameObject("This is a test"));
				gm.switchScene("MansionPartA", "door_b");
			}

			//Player Input
			if (gm.getKey("aim"))
			{
				//Attacking and Aiming Stances
			}
			else
			{
				//Variables
				float angle_spd = 0f;  //Change in angle that is applied to player's transform.eulerAngles.y
				float move_spd = 0f;  //Movement Speed in the player's forward direction (If negative player moves backwards)

				//Move forwards and backwards
				float walk_run_spd = spd;
				if (gm.getKey("run"))
				{
					walk_run_spd = run_spd;  //Set Player's Speed to Run Speed
				}

				if (gm.getKey("up"))
				{
					move_spd = walk_run_spd; //Move Forwards in the Run or Walk Speed
				}
				else if (gm.getKey("down"))
				{
					move_spd = -back_spd;  //Move Backwards in the Walk Backwards Speed
				}

				//Turning left and right
				if (gm.getKey("left"))
				{
					angle_spd = -turn_spd;  //Turn Left
				}
				else if (gm.getKey("right"))
				{
					angle_spd = turn_spd;  //Turn Right
				}
				
				//Interact
				if (gm.getKeyDown("interact"))
				{
					checkInteract();  //Check for an object tagged with "Interact" with the checkInteract() method 
				}
				
				//Set Movement Physics
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angle_spd, transform.eulerAngles.z);
				float facing_angle = (-transform.eulerAngles.y + 90) * Mathf.Deg2Rad;
				velocity = new Vector2(Mathf.Cos(facing_angle), Mathf.Sin(facing_angle)) * move_spd;
			}
			
			//Inventory Menu
			if (gm.getKey("inventory"))
			{
				//Create Inventory Canvas and turn off Player Movement
				CameraManager.instance.createInventoryCanvas();
				can_move = false;
			}
		}
	}

	//Physics
	void FixedUpdate()
	{
		//Set Player's change in position with velocity
		if (velocity != Vector2.zero)
		{
			rb.MovePosition(transform.position + new Vector3(velocity.x, 0, velocity.y));
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		//Check for Camera Trigger and set Camera associated with the Camera Trigger to be the active Camera
		if (collision.gameObject.CompareTag("Finish"))
		{
			if (collision.GetComponent<CameraTrigger>() != null)
			{
				collision.GetComponent<CameraTrigger>().switchCamera();
			}
		}
	}
	
	//Interact
	private void checkInteract()
	{
		Collider[] hits;
		
		//Check for objects tagged with "Interact" in front of the Player
		float facing_angle = (-transform.eulerAngles.y + 90) * Mathf.Deg2Rad;
		Vector3 check_offset = new Vector3(Mathf.Cos(facing_angle), 0, Mathf.Sin(facing_angle)) * 0.8f;
		hits = Physics.OverlapSphere(transform.position + check_offset, 0.5f);

		foreach (Collider hit in hits)
		{
			//Perform inherited InteractInterface method action()
			if (hit.gameObject.CompareTag("Interact"))
			{
				hit.GetComponent<InteractInterface>().action();
				return;
			}
		}
	}
	
	//Methods
	public void pickupItem(int item, int stock, GameObject item_object)
	{
		//Opens up an Inventory Canvas and then sets the Inventory to show what item was picked up and freezes the Player
		CameraManager.instance.createInventoryCanvas();
		CameraManager.instance.setInventoryPickUp(item, stock, item_object);
		can_move = false;
	}

	//Getter & Setters
	public int equip
	{
		//Returns the Player's current equipped weapon
		//If it returns -1, the player doesn't have anything equipped
		get
		{
			if (gm.inventory.player_equip == -1)
			{
				return -1;
			}
			return gm.inventory.inventory[gm.inventory.player_equip];
		}
	}
	
	public int ammo
	{
		//Returns the player's current ammo count on their weapon
		get
		{
			if (equip == -1)
			{
				return 0;
			}
			if (equip == 1)
			{
				return 1;
			}
			return gm.inventory.inventory_stock[gm.inventory.player_equip];
		}
	}
	
	public bool canmove
	{
		//Sets the player's input to be frozen or not frozen
		set
		{
			can_move = value;
		}
	}
	
	//Debug
	void OnDrawGizmos()
	{
		//Remove after done testing interact hit boxes
		//Testing with hitboxes
		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position - new Vector3(0, 0.8f, 0), new Vector3(2, 0.4f, 2));
	}
}

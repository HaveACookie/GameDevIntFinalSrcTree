using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour {

	//Components
	private GameManager gm;
	private Rigidbody rb;
	//private HealthPoints hp;
	
	//Settings
	[SerializeField] private float spd;
	[SerializeField] private float run_spd;
	[SerializeField] private float turn_spd;
	[SerializeField] private float back_spd;

	private bool can_move;
	
	//Variables
	private Vector2 velocity;
	
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
		//Movement
		velocity = Vector2.zero;
		if (can_move)
		{
			//Debug
			if (Input.GetKeyDown(KeyCode.K))
			{
				pickupItem(30, 0, new GameObject("This is a test"));
			}
			
			//Variables
			float angle_spd = 0f;
			float move_spd = 0f;
			
			//Move forwards and backwards
			float walk_run_spd = spd;
			if (gm.getKey("run"))
			{
				walk_run_spd = run_spd;
			}

			if (gm.getKey("up"))
			{
				move_spd = walk_run_spd;
			}
			else if (gm.getKey("down"))
			{
				move_spd = -back_spd;
			}
			
			//Turning left and right
			if (gm.getKey("left"))
			{
				angle_spd = -turn_spd;
			}
			else if (gm.getKey("right"))
			{
				angle_spd = turn_spd;
			}
			
			//Inventory Menu
			if (gm.getKey("inventory"))
			{
				CameraManager.instance.createInventoryCanvas();
				can_move = false;
			}
			
			//Set Movement Physics
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angle_spd, transform.eulerAngles.z);
			float facing_angle = (-transform.eulerAngles.y + 90) * Mathf.Deg2Rad;
			velocity = new Vector2(Mathf.Cos(facing_angle), Mathf.Sin(facing_angle)) * move_spd;
		}
	}

	//Physics
	void FixedUpdate()
	{
		if (velocity != Vector2.zero)
		{
			rb.MovePosition(transform.position + new Vector3(velocity.x, 0, velocity.y));
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		//Check for Camera Trigger
		if (collision.gameObject.CompareTag("Finish"))
		{
			if (collision.GetComponent<CameraTrigger>() != null)
			{
				collision.GetComponent<CameraTrigger>().switchCamera();
			}
		}
	}
	
	//Methods
	public void pickupItem(int item, int stock, GameObject item_object)
	{
		CameraManager.instance.createInventoryCanvas();
		CameraManager.instance.setInventoryPickUp(item, stock, item_object);
		can_move = false;
	}

	//Getter & Setters
	public int equip
	{
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
		set
		{
			can_move = value;
		}
	}
	
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour {

	//Components
	private GameManager gm;  //Game Manager Singleton
	private Rigidbody rb;  //Player Physics Rigidbody
                           //private HealthPoints hp;
    public AudioSource playerAudio;
    public AudioClip gunShot;
    public AudioClip knifeSwing;
       
	//Settings
	[SerializeField] private float spd;  //Player Speed
	[SerializeField] private float run_spd; //Player Running Speed
	[SerializeField] private float turn_spd; //Player Turning Speed
	[SerializeField] private float back_spd; //Player Moving Backwards Speed
	[SerializeField] private float attack_delay;

	private bool can_move;  //Turn Player Input on and off
	
	//Variables
	private Vector2 velocity;  //Player Physics movement in the x and z axis
	private float aim;
	private float aim_stance;
	private float attack_time;
	private float invincibility;
	
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
		aim = 0f;
		aim_stance = 0f;
	}
	
	//Update Event
	void Update () {
		//Player Action
		velocity = Vector2.zero;  //Reset the player velocity to zero
		if (can_move)
		{
			//Invincibility
			if (invincibility > 0)
			{
				invincibility -= Time.deltaTime;
				Debug.Log(invincibility);
			}
			
			if (attack_time > 0)
			{
				attack_time -= Time.deltaTime;
			}
			
			//Debug (This block of code will be deleted later)
			gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.blue, Color.red, aim);
			float aim_stance_direction = (aim_stance * Mathf.PI) / 6;
			gameObject.transform.GetChild(0).transform.localPosition = (new Vector3(0f, Mathf.Sin(aim_stance_direction), Mathf.Cos(aim_stance_direction)) * 1.25f) + new Vector3(0, 3f, 0);

			//Player Input
			if (gm.getKey("aim"))
			{
				//Attacking and Aiming Stances
				aim = Mathf.Lerp(aim, 1, Time.deltaTime * 5f);

				if (gm.getKey("up"))
				{
					aim_stance = Mathf.Lerp(aim_stance, 1, Time.deltaTime * 5f);
				}
				else if (gm.getKey("down"))
				{
					aim_stance = Mathf.Lerp(aim_stance, -1, Time.deltaTime * 5f);
				}
				else
				{
					aim_stance = Mathf.Lerp(aim_stance, 0, Time.deltaTime * 5f);
				}
				
				//Turning left and right
				float angle_spd = 0f; 
				
				if (gm.getKey("left"))
				{
					angle_spd = -turn_spd;  //Turn Left
				}
				else if (gm.getKey("right"))
				{
					angle_spd = turn_spd;  //Turn Right
				}
				
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angle_spd, transform.eulerAngles.z);
				
				//Shooting
				if (gm.getKeyDown("attack"))
				{
					if (attack_time <= 0)
					{
						weaponAction();
					}
				}
			}
			else
			{
				//Variables
				float angle_spd = 0f;  //Change in angle that is applied to player's transform.eulerAngles.y
				float move_spd = 0f;  //Movement Speed in the player's forward direction (If negative player moves backwards)
				aim = Mathf.Lerp(aim, 0, Time.deltaTime * 5f);
				aim_stance = Mathf.Lerp(aim_stance, 0, Time.deltaTime * 5f);

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
		
		//Check Ground for objects tagged with "Interact"
		hits = Physics.OverlapBox(transform.position - new Vector3(0, 0.8f, 0), new Vector3(1f, 0.4f, 1f), transform.rotation);
		
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
	
	//Weapons
	private void weaponAction()
	{
		if (ammo < 1)
		{
			return;
		}
		
		attack_time = attack_delay;
		GameObject hit_enemy;
		switch (equip)
		{
			case 1 :
                playerAudio.PlayOneShot(knifeSwing);
				hit_enemy = hitEnemy(40);
				if (hit_enemy != null)
				{
					hit_enemy.GetComponent<HealthScript>().damage(1);
				}
				return;
			case 2 :
                playerAudio.PlayOneShot(gunShot);
                hit_enemy = hitEnemy(30);
				if (hit_enemy != null)
				{
					hit_enemy.GetComponent<HealthScript>().damage(2);
					if (Random.Range(0, 100) < 5)
					{
						hit_enemy.GetComponent<HealthScript>().headshot();
					}
				}
				gm.inventory.removeItem(equip, 1);
				return;
			case 3 :
                playerAudio.PlayOneShot(gunShot);
                hit_enemy = hitEnemy(45);
				if (hit_enemy != null)
				{
					hit_enemy.GetComponent<HealthScript>().damage(2);
					if (Random.Range(0, 100) < 5)
					{
						hit_enemy.GetComponent<HealthScript>().headshot();
					}
				}
				gm.inventory.removeItem(equip, 1);
				return;
			default :
				return;
		}
	}

	private GameObject hitEnemy(float range)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if (enemies.Length < 1)
		{
			return null;
		}
		
		GameObject enemy_hit = null;
		for (int i = 0; i < enemies.Length; i++)
		{
			if (canHitRay(enemies[i].transform.position) && canHitAngle(enemies[i].transform.position, range))
			{
				if (enemy_hit == null)
				{
					enemy_hit = enemies[i];
				}
				else if (Vector3.Distance(transform.position, enemies[i].transform.position) < Vector3.Distance(transform.position, enemy_hit.transform.position))
				{
					enemy_hit = enemies[i];
				}
			}
		}

		return enemy_hit;
	}

	private bool canHitAngle(Vector3 target, float range)
	{
		float target_angle = pointAngle(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));
		float facing_angle = transform.eulerAngles.y;
		if (Mathf.Abs(Mathf.DeltaAngle(facing_angle, target_angle)) < range)
		{
			return true;
		}
		return false;
	}
	
	private bool canHitRay(Vector3 target)
	{
		Vector3 direction = target - transform.position;
		RaycastHit[] hit = Physics.RaycastAll(transform.position, direction, Vector3.Distance(transform.position, target), GridBehaviour.instance.solids_layer);
		if (hit.Length > 0)
		{
			return false;
		}
		return true;
	}
	
	private float pointAngle(Vector2 pointA, Vector2 pointB)
	{
		return -((Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * 180) / Mathf.PI) + 90;
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

	public bool canattack
	{
		get
		{
			if (invincibility > 0)
			{
				return false;
			}
			return true;
		}
	}

	public void hurt()
	{
		invincibility = 1.8f;
	}
	
	//Debug
	void OnDrawGizmos()
	{
		//Remove after done testing interact hit boxes
		//Testing with hitboxes
		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position - new Vector3(0, 0.8f, 0), new Vector3(1, 0.4f, 1));
	}
}

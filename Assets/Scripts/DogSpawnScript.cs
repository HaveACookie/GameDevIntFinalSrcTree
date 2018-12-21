using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSpawnScript : MonoBehaviour {
    //AudioIntegration(Christian)
    public AudioSource mainSource;
    public AudioClip noDogs;
    public AudioClip Dogs;


	//Settings
	[Header("Window")]
	[SerializeField] private GameObject window;
	[SerializeField] private Sprite window_broken;
	
	[Header("Dog")]
	[SerializeField] private GameObject dog_enemy;
	[SerializeField] private GameObject dog_leap;

	[Header("Trigger")] 
	[SerializeField] private float trigger_radius;
	[SerializeField] private Vector2 trigger_position;
	
	//Variables
	private PlayerBehaviour player;
	private bool activated;

	//Init
	void Start()
	{
		if (dog_enemy == null)
		{
			window.GetComponent<SpriteRenderer>().sprite = window_broken;
			Destroy(gameObject);
			return;
		}
		
		dog_enemy.SetActive(false);
		activated = false;
		player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	//Update
	void Update()
	{
		if (!activated)
		{
			if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), trigger_position + new Vector2(transform.position.x, transform.position.z)) < trigger_radius)
			{
				activated = true;
				spawnDog();
			}
		}
	}
	
	//Launch Dog
	private void spawnDog()
	{
		GameObject new_dog = Instantiate(dog_leap, dog_enemy.transform.position, dog_enemy.transform.rotation);
		DogSpawnLeapScript script = new_dog.AddComponent<DogSpawnLeapScript>();
		script.original_dog = dog_enemy;
		new_dog.GetComponent<Rigidbody>().AddForce(new_dog.transform.forward * 6f, ForceMode.Impulse);
		window.GetComponent<SpriteRenderer>().sprite = window_broken;

		for (int i = 0; i < 10; i++)
		{
			GameObject glass_obj = Instantiate(Resources.Load<GameObject>("Enemies/Dog/Glass/pGlass" + Random.Range(0, 10)), dog_enemy.transform.position, dog_enemy.transform.rotation);
			glass_obj.GetComponent<Rigidbody>().AddForce((new_dog.transform.forward + new Vector3(0, Random.Range(-8f, 8f), 0)) * 6f, ForceMode.Impulse);
		}
	}
	
	//Debug
	void OnDrawGizmos()
	{
		Vector3 draw_position = transform.position + new Vector3(trigger_position.x, 0, trigger_position.y);
		Gizmos.color = Color.red;
		float first_point = 0;
		for (float i = 5; i < 360; i += 5f)
		{
			float second_point = i;
			Vector3 point_a = new Vector3(Mathf.Cos(first_point * Mathf.Deg2Rad), 0, Mathf.Sin(first_point * Mathf.Deg2Rad)) * trigger_radius;
			Vector3 point_b = new Vector3(Mathf.Cos(second_point * Mathf.Deg2Rad), 0, Mathf.Sin(second_point * Mathf.Deg2Rad)) * trigger_radius;
			Gizmos.DrawLine(draw_position + point_a, draw_position + point_b);
			first_point = second_point;
		}
	}

}

public class DogSpawnLeapScript : MonoBehaviour
{
	public GameObject original_dog { private get; set; }

	void Update()
	{
		if (isGrounded())
		{
			original_dog.transform.position = transform.position;
			original_dog.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			original_dog.SetActive(true);
			Destroy(gameObject);
		}
	}
	
	private bool isGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f);
	}
	
}
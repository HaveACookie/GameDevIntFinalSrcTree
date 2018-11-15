using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {

	//Components
	private PlayerBehaviour player;
	
	//Settings
	private bool fade_in;
	private float fade_speed;
	private string transition_scene;
	
	//Variables
	private float alpha;
	private Canvas canvas;
	private Image black_screen;
	
	private bool faded;

	private List<GameObject> inactive_enemies;

	//Initialization
	void Awake()
	{
		fade_speed = 3.6f;
		inactive_enemies = new List<GameObject>();
	}

	void Start()
	{
		if (GameObject.FindWithTag("Player") != null)
		{
			player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
		}

		if (player != null)
		{
			player.canmove = false;
		}
		
		if (fade_in)
		{
			alpha = 0;
		}
		else
		{
			alpha = 1;
		}

		canvas = gameObject.GetComponent<Canvas>();
		black_screen = createBlack();
		setEnemiesInactive();
	}
	
	//Update Event
	void Update()
	{
		if (!faded)
		{
			//Set Values
			alpha = Mathf.Clamp01(alpha);
			black_screen.color = new Color(0, 0, 0, alpha);
			
			//Fade Screen
			if (fade_in)
			{
				alpha = Mathf.Lerp(alpha, 1, fade_speed * Time.deltaTime);
				if (alpha > 0.95f)
				{
					faded = true;
					SceneManager.LoadScene(transition_scene, LoadSceneMode.Single);
				}
			}
			else
			{
				alpha = Mathf.Lerp(alpha, 0, fade_speed * Time.deltaTime);
				if (alpha < 0.025f)
				{
					faded = true;
					setEnemiesActive();
					if (player != null)
					{
						player.canmove = true;
					}
					Destroy(gameObject);
				}
			}
		}
	}
	
	//Setter Methods
	public bool fadein
	{
		set
		{
			fade_in = value;
		}
	}

	public string scene
	{
		set
		{
			transition_scene = value;
		}
	}
	
	//Create Methods
	private Image createBlack()
	{
		GameObject black_screen_obj = new GameObject("black_screen", typeof(RectTransform));
		black_screen_obj.transform.SetParent(canvas.transform);
		Image black_screen_img = black_screen_obj.AddComponent<Image>();
		
		Texture2D tex = Resources.Load<Texture2D>("texture2") as Texture2D;
		black_screen_img.sprite = Sprite.Create(tex, new Rect(64, 0, 64, 64), new Vector2(0.5f, 0.5f));
		
		black_screen_img.color = new Color(0, 0, 0, alpha);
		black_screen_img.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
		black_screen_img.GetComponent<RectTransform>().localScale = new Vector3(10000, 10000, 1);

		return black_screen_img;
	}
	
	//Misc Methods
	private void setEnemiesActive()
	{
		foreach (GameObject enemy in inactive_enemies)
		{
			enemy.GetComponent<EnemyBehaviour>().enabled = true;
		}
	}
	
	private void setEnemiesInactive()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies)
		{
			inactive_enemies.Add(enemy);
			enemy.GetComponent<EnemyBehaviour>().enabled = false;
		}
	}
}
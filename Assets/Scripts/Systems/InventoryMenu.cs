using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryMenu : MonoBehaviour {

	//Settings
	private InventoryTransition transition;

	// Use this for initialization
	void Awake () {
		//Transition
		transition = gameObject.AddComponent<InventoryTransition>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class InventoryTransition : MonoBehaviour
{
	
	//Settings
	private bool trans;
	private bool transflip;
	private bool startflip;
	private float alpha;
	private Image black_screen;
	private Image background;
	private Image inventory_image;

	private InventoryMenu menu;
	private List<GameObject> enemies;

	void Start()
	{
		//Settings
		alpha = 0;
		GameObject black_screen_obj = new GameObject("black_screen", typeof(RectTransform));
		black_screen_obj.transform.SetParent(gameObject.transform);
		black_screen = black_screen_obj.AddComponent<Image>();
		
		Texture2D tex = Resources.Load<Texture2D>("texture2") as Texture2D;
		black_screen.sprite = Sprite.Create(tex, new Rect(64, 0, 64, 64), new Vector2(0.5f, 0.5f));
		startflip = true;
		black_screen.color = new Color(0, 0, 0, 1);
		if (trans)
		{
			transflip = false;
			black_screen.color = new Color(0, 0, 0, 0);
		}
		black_screen.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
		black_screen.GetComponent<RectTransform>().localScale = new Vector3(10000, 10000, 1);
		
		//Set Enemies Inactive
		enemies = new List<GameObject>();
		GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemy.Length; i++)
		{
			enemies.Add(enemy[i]);
			enemy[i].GetComponent<EnemyBehaviour>().enabled = false;
		}
	}

	void Update()
	{
		if (startflip)
		{
			if (transflip)
			{
				
			}
			else
			{
				if (trans)
				{
					alpha = Mathf.Lerp(alpha, 1, Time.deltaTime * 5f);
					if (alpha >= 0.95f)
					{
						trans = false;
						alpha = 1;
						menu = gameObject.AddComponent<InventoryMenu>();
						
						GameObject background_obj = new GameObject("background", typeof(RectTransform));
						background_obj.transform.SetParent(gameObject.transform);
						background = background_obj.AddComponent<Image>();
						background.sprite = Resources.Load<Sprite>("System/GUI/sInventoryScreenBGColor");
						background.GetComponent<RectTransform>().localScale = new Vector3(50, 50, 1);
						background.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
						
						GameObject inventory_obj = new GameObject("inventory_image", typeof(RectTransform));
						inventory_obj.transform.SetParent(gameObject.transform);
						inventory_image = inventory_obj.AddComponent<Image>();
						inventory_image.sprite = Resources.Load<Sprite>("System/GUI/sInventoryMenu");
						inventory_image.GetComponent<RectTransform>().sizeDelta = new Vector2(480, 360);
						inventory_image.GetComponent<RectTransform>().localScale = new Vector3(3, 3, 1);
						inventory_image.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
					}
				}
				else
				{
					alpha = Mathf.Lerp(alpha, 0, Time.deltaTime * 5f);
					if (alpha <= 0.05f)
					{
						startflip = false;
					}
				}
			}	
		}
		
		black_screen.color = new Color(0, 0, 0, alpha);
		black_screen.transform.SetAsLastSibling();
	}

	public void setTrans()
	{
		trans = true;
	}
}
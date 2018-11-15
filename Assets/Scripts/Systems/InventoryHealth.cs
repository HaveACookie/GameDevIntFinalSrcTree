using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHealth : MonoBehaviour {

	//Components
	private SpriteRenderer sr;
	private Image img;
	private Image health_text;

	//Initialization
	void Awake ()
	{
		sr = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		img = GetComponent<Image>();
		health_text = transform.GetChild(01).gameObject.GetComponent<Image>();

		health = 4;
	}
	
	//Update Event
	void Update ()
	{	
		img.sprite = sr.sprite;
	}

	public int health
	{
		set
		{
			int health_val = value;
			
			if (health_val == 4)
			{
				health_text.sprite = Resources.Load<Sprite>("System/GUI/HealthStatus/sHealthFine");
				img.color = new Color(56 / 255f, 163 / 255f, 21 / 255f, 0.8f);
			}
			else if (health_val == 3)
			{
				health_text.sprite = Resources.Load<Sprite>("System/GUI/HealthStatus/sHealthCaution");
				img.color = new Color(204 / 255f, 204 / 255f, 67 / 255f, 0.8f);
			}
			else if (health_val == 2)
			{
				health_text.sprite = Resources.Load<Sprite>("System/GUI/HealthStatus/sHealthCaution");
				img.color = new Color(246 / 255f, 141 / 255f, 43 / 255f, 0.8f);
			}
			else if (health_val == 1)
			{
				health_text.sprite = Resources.Load<Sprite>("System/GUI/HealthStatus/sHealthDanger");
				img.color = new Color(188 / 255f, 31 / 255f, 31 / 255f, 0.8f);
			}
			else if (health_val <= 0)
			{
				img.enabled = false;
				health_text.enabled = false;
			}

			health_text.color = img.color;
		}
	}

	public bool poison
	{
		set
		{
			if (value == true)
			{
				health_text.sprite = Resources.Load<Sprite>("System/GUI/HealthStatus/sHealthPoison");
				img.color = new Color(125 / 255f, 23 / 255f, 193 / 255f, 0.8f);
				health_text.color = img.color;
			}
		}
	}
}

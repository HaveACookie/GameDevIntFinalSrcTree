using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHealth : MonoBehaviour {

	//Components
	private SpriteRenderer sr;
	private Image img;
	private Image health_text;
	public Sprite[] health_icons;

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

	//Public Methods
	public int health
	{
		//Sets the Player's Health in the GUI Menu in the Inventory Canvas
		//If you set the health to 4 the Health GUI will be "Fine" and Green
		//If you set the health to 3 the Health GUI will be "Caution" and Yellow
		//If you set the health to 2 the Health GUI will be "Caution" and Orange
		//If you set the health to 1 the Health GUI will be "Danger" and Red
		//If you set the health to 0 the Health GUI will be turned off because you are dead UwU
		//The Health is clamped between 0 and 4
		set
		{
			int health_val = value;
			
			if (health_val == 4)
			{
				health_text.sprite = health_icons[0];
				img.color = new Color(56 / 255f, 163 / 255f, 21 / 255f, 0.8f);
			}
			else if (health_val == 3)
			{
				health_text.sprite = health_icons[1];
				img.color = new Color(204 / 255f, 204 / 255f, 67 / 255f, 0.8f);
			}
			else if (health_val == 2)
			{
				health_text.sprite = health_icons[1];
				img.color = new Color(246 / 255f, 141 / 255f, 43 / 255f, 0.8f);
			}
			else if (health_val == 1)
			{
				health_text.sprite = health_icons[2];
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
		//Sets the Player's Health GUI in the Inventory to "Poisoned" and turns it purple
		//Turn this effect on and off by setting it to true or false
		set
		{
			if (value == true)
			{
				health_text.sprite = health_icons[3];
				img.color = new Color(125 / 255f, 23 / 255f, 193 / 255f, 0.8f);
				health_text.color = img.color;
			}
		}
	}
}

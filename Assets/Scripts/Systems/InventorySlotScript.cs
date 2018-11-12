using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour
{

	//Components
	private Animator anim;
	private SpriteRenderer sr;
	private Image img;
	private Image clone;
	private Text stock;
	
	//Settings
	private bool hovered;
	private bool selected;
	
	private float offset;
	private Vector2 position;
	
	//Variables
	private float alpha;
	private float clone_alpha;
	private float sin_val;
	private float temp_sin;
	
	//Initialization
	void Awake()
	{
		//Components
		anim = transform.GetChild(0).GetComponent<Animator>();
		sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
		img = GetComponent<Image>();
		GameObject clone_obj = new GameObject("clone", typeof(RectTransform), typeof(Image));
		clone_obj.transform.SetParent(transform);
		clone_obj.transform.localPosition = new Vector3(0, 0, 0);
		clone_obj.transform.localScale = new Vector3(1, 1, 1);
		clone_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 50);
		clone = clone_obj.GetComponent<Image>();
		clone.enabled = false;
		sr.enabled = false;
		
		GameObject text_obj = new GameObject("map_button", typeof(RectTransform), typeof(Text));
		text_obj.transform.SetParent(transform);
		text_obj.transform.localPosition = new Vector3(0, 0, 0);
		text_obj.transform.localScale = new Vector3(0.06896551724f, 0.06896551724f, 1f);
		text_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(880, 660);
		stock = text_obj.GetComponent<Text>();
		stock.alignment = TextAnchor.LowerRight;
		stock.font = Resources.Load<Font>("System/GUI/ResTextFont");
		stock.fontSize = 300;
		stock.text = "";
		
		//Settings
		hovered = false;
		selected = false;
		
		offset = 10f;
		
		//Variables
		alpha = 1;
		clone_alpha = 0;
		sin_val = 0;
		temp_sin = 0;
		
		setItemValue(0, 0);
	}

	void Start()
	{
		//Settings
		position = new Vector2(transform.localPosition.x, transform.localPosition.y);
	}
	
	//Update Event
	void Update()
	{
		//Draw Sin
		temp_sin += Time.deltaTime * 0.7f;
		if (temp_sin >= 1)
		{
			temp_sin = 0f;
		}
		float draw_sin = (Mathf.Sin(temp_sin * 2 * Mathf.PI) + 1) / 2f;
		
		//Select Screen
		if (selected)
		{
			sin_val = (draw_sin * 0.2f) + 0.4f;
			alpha = 1f;
			clone_alpha = 0.6f;
		}
		else if (hovered)
		{
			sin_val = Mathf.Lerp(sin_val, 1, Time.deltaTime * 2f);
			alpha = Mathf.Lerp(alpha, 1, Time.deltaTime * 3f);
			clone_alpha = Mathf.Lerp(clone_alpha, 0.6f, Time.deltaTime * 2f);
		}
		else
		{
			sin_val = 0;
			alpha = Mathf.Lerp(alpha, 0.6f, Time.deltaTime * 3f);
			clone_alpha = 0f;
		}
		
		setPositionLerp(sin_val);
		setClonePositionLerp(sin_val);
		img.color = new Color(1, 1, 1, clone_alpha);
		clone.color = new Color(1, 1, 1, alpha);
	}

	private void setPositionLerp(float t)
	{
		transform.localPosition = new Vector2(position.x + (offset * t), position.y - (offset * t));
		stock.transform.localPosition = new Vector2(-(offset * t) / 2f, (offset * t) / 2f);
	}
	
	private void setClonePositionLerp(float t)
	{
		clone.transform.localPosition = new Vector2(-(offset * t), (offset * t));
	}

	//Public Methods
	public void setItemValue(int item, int num)
	{
		if (item <= 0)
		{
			img.enabled = false;
			clone.enabled = false;
			return;
		}

		sr.enabled = true;
		anim.Play(item.ToString());

		img.sprite = sr.sprite;
		clone.sprite = img.sprite;
		
		sr.enabled = false;
		img.enabled = true;
		clone.enabled = true;

		stock.text = "";
		if (num != 0)
		{
			stock.text = num.ToString();
		}
	}
	
	public bool hover
	{
		set
		{
			hovered = value;
		}
	}

	public bool select
	{
		set
		{
			selected = value;
		}
	}

	public Vector2 static_position
	{
		get
		{
			return position;
		}
	}
}

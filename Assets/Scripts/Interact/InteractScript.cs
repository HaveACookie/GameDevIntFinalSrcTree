using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : InteractInterface {

	//Components
	private Collider col;
	
	//Settings
	protected string interact_tag;
	
	//Variables
	[SerializeField] protected bool persistent;
	
	//Initialization
	void Awake()
	{
		init();
	}

	protected override void init()
	{
		//Components
		gameObject.tag = "Interact";
		
		//Collider
		if (gameObject.GetComponent<Collider>() != null)
		{
			col = gameObject.GetComponent<Collider>();
		}
		else
		{
			col = gameObject.AddComponent<BoxCollider>();
		}
		col.isTrigger = true;
	}

	//Interact Methods
	public override void action()
	{
		
	}

	public bool compareInteractTag(string tag)
	{
		if (tag == interact_tag)
		{
			return true;
		}
		return false;
	}

	public bool index
	{
		get
		{
			return !persistent;
		}
	}
	
}

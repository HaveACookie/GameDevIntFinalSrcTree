using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : InteractScript {

	//Settings
	[SerializeField] private int item_id;
	[SerializeField] private int item_stock;
	
	protected override void init()
	{
		base.init();
		interact_tag = "Item";
	}
	
	public override void action()
	{
		GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>().pickupItem(item_id, item_stock, gameObject);
	}
	
}

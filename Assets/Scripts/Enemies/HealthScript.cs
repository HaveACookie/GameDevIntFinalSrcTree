using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

	//Settings
	[SerializeField] public int health { get; private set; }
	
	//Methods
	public void damage(int dmg)
	{
		health -= dmg;

		//Debug Kill Enemy
		//Debug.Log(health);
		if (health <= 0)
		{
			GameManager.instance.events.saveEnemy(gameObject, true);
			gameObject.SetActive(false);
		}
	}

	public void headshot()
	{
		health = 0;
		damage(-1);
	}
	
}

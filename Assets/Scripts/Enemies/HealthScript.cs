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
	}

	public void headshot()
	{
		health = 0;
	}
	
}

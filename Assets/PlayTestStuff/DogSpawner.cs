using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSpawner : MonoBehaviour {
    public bool dogSpawn;
    public GameObject dogs;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == ("Player"))
        {
            Instantiate(dogs, transform);
        }

    }
}

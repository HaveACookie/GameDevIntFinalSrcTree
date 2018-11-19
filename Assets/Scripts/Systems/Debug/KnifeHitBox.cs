using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This should go on a triggerbox
public class KnifeHitBox : MonoBehaviour {
    public bool CanKnife;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void onTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Equals("Zombie"))
        {
            //this could be passed to a singleton
            CanKnife = true;
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GUI : MonoBehaviour {

    //usage: put this on empty game UI object
    //purpose: prompt text on screen when interacting with things


    // don't destroy this object throughout the play scenes
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GUI");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public Text GameUI;

     void Update()
    {
        
    }
}

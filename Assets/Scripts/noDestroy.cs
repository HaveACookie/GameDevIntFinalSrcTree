using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noDestroy : MonoBehaviour {

    //purpose: put this on the canvas within playscene
    // don't destroy this object throughout the play scenes
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GUI");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

      
    }
}

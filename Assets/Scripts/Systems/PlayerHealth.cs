using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerHealth : MonoBehaviour {

    //usage: put this on the player gameobject
    //intent: manage player health, enemy detection

    public int playerHealth_val = 4;
    public bool playerIsPoisoned = false;
    public bool playerIsDead = false;

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag("Enemy"))
        {
            playerHealth_val -= 1; 
        }
    }

     void Update()
    {
        if (playerHealth_val == 0)
        {
            playerIsDead = true;
            SceneManager.LoadScene("renameplaytest");
            //player death scene goes here
            //also load the start scene
        }
    }
}
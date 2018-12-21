using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractGUI : MonoBehaviour
{

    //usage: put this on player
    //purpose: prompt text on screen when interacting with things

    public Text GameUI;
    public Animator GUIAnimator; 

    
    void Start()
    {
        //supposed to find the component throughout all scenes, not working for some reason!!! 
        Text GameUI = GameObject.Find("TextInGame").GetComponent<Text>(); //Text in the Canvas 
        Animator GUIAnimator = GameObject.Find("TextInGame").GetComponent<Animator>(); //Animator for fade out 
    }



    private GameManager gm;  //Game Manager Singleton

    //make the text fade in and out, plays after every text 
    IEnumerator GUI() {
        GUIAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3f);
        GUIAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        GameUI.text = " ";
    }



    //call all the different objects here with the text you want to go with it 
    //trigger is on the player
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "LockedDoor")
        {
            GameUI.text = "Door is locked.";
            StartCoroutine(GUI());

        }

        if (collision.gameObject.tag == "Stairs")   
        {
            GameUI.text = "Stairs seem a little unstable... Better not go up there.";
            StartCoroutine(GUI());
        }

        if (collision.gameObject.tag == "Statue")
        {
            GameUI.text = "\"A woman drawing water\"";
            StartCoroutine(GUI());
        }
    }
}
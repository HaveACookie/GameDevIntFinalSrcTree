using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractGUI : MonoBehaviour
{

    //usage: put this on gameobjects whihc need to be interacted with
    //purpose: prompt text on screen when interacting with things


    // don't destroy this object throughout the play scenes
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GUI");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        //display nothing first
        GameUI.text = " ";
    }


    public Text GameUI; //Text in the Canvas 
    public Animator GUIAnimator; //Animator for fade out 
    private GameManager gm;  //Game Manager Singleton

    //make the text fade out, plays after every text 
    IEnumerator EndText() { 

    yield return new WaitForSeconds(3f);
        GUIAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
    }

    //call all the different objects here with the text you want to go with it 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LockedDoor")
        {
            GameUI.text = "Door is locked.";
            StartCoroutine(EndText());
        }

        if (collision.gameObject.tag == "Stairs")   
        {
            GameUI.text = "Stairs seem a little unstable... Better not go up there.";
            StartCoroutine(EndText());
        }
    }
}
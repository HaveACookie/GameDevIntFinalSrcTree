using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour {

    //usage: put this on an empty gameobject in the start scene
    //purpose: manage the logo cutscenes, music and transition from start to game scene.


     void Start()
    {
        StartCoroutine(cutscenes());
    }

    //animators 

    public Animator fadeAnimator;
    public Animator cutsceneAnimator;
    
    //calling
    public void FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }
    public void StartCutscene()
    {
        cutsceneAnimator.SetTrigger("BeginCutscene");
    }

    //fading in and out of logo scenes 
    IEnumerator cutscenes()

    {

        yield return new WaitForSeconds(3f);
        FadeOut();
        yield return new WaitForSeconds(1f);
        FadeIn();
        yield return new WaitForSeconds(3f);
        FadeOut();
        yield return new WaitForSeconds(1f);
        FadeIn();
        yield return new WaitForSeconds(3f);
        FadeOut();
        yield return new WaitForSeconds(1f);    
        FadeIn();
        StartCutscene();
        yield return new WaitForSeconds(1f);
   

    }


	void Update () {
		if (Input.anyKey)
        {
            SceneManager.LoadScene("MainHall", LoadSceneMode.Single);
        }
	}

    IEnumerator start()
    {
        FadeOut();
        yield return new WaitForSeconds(1f);

        //when we figure out what the first play scene is, open it here.
        //SceneManager.LoadScene("");

    }
}

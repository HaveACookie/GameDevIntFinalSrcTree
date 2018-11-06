using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandling : MonoBehaviour {

    private RaycastHit hit;
    public bool doorHit = false;
    public bool itemSurfaceHit = false;
    public bool shotHit = true;
    public bool shooting = true;
    public bool shootStance = true;
    // Use this for initialization




    // Update is called once per frame
    void Update()
    {
        interactionCasting();
        shotCasting();
        //Checks to see if the raycast is in range and if the player is pressing the desired button
        if (Input.GetKey(KeyCode.E) && doorHit.Equals(true))
        {
            Debug.Log("weMadeit");
            //Scene Management Stuff Involving Doors goes here
        }

        if(Input.GetKey(KeyCode.E) && itemSurfaceHit.Equals(true))
        {
            //Stuff involving adding items to your inventory
        }

    

  

        if (Input.GetKey(KeyCode.LeftShift))
        {
            shootStance = true;
        }
        else
        {
            shootStance = false;
        }

        if (shootStance = true && Input.GetKey(KeyCode.R))
        {
            shooting = true;
        }

        if (shooting == true & shotHit.Equals(true))
        {
            Debug.Log("ShotWorked");
        }
        




    }


    void shotCasting()
    {
        //Define Ray
        Ray shotRay = new Ray(transform.position, transform.forward);
        //Define Max RayCast Distance
        float maxRaycastDistance = 20f;
        // Define RayCast hit variable
        RaycastHit myRayShotHit = new RaycastHit();
        //Visualize the raycast
        Debug.DrawRay(shotRay.origin, shotRay.direction * maxRaycastDistance, Color.yellow);
        //Determine if gun hit

        if (myRayShotHit.collider.tag.Equals("Enemy"))
        {
            shotHit = true;
        }


    }



    void interactionCasting()
    {
        //Define Ray
        Ray interactRay = new Ray(transform.position, transform.forward);
        //Define Max RayCast Distance
        float maxRaycastDistance = 50f;
        // Define RayCast hit variable
        RaycastHit myRayHit = new RaycastHit();
        //Visualize the raycast
        Debug.DrawRay(interactRay.origin, interactRay.direction * maxRaycastDistance, Color.green);

        //Determine what object type the raycast actually hit
        if (Physics.Raycast(interactRay, out myRayHit, maxRaycastDistance))
        {
            switch (myRayHit.collider.tag)
            {
                case "Door":
                    {
                        Debug.Log("DoorHit");
                        doorHit = true;
                        //Insert reference to scenemanagement here
                        break;
                    }
                case "ItemSurface":
                    {
                        Debug.Log("ItemHit");
                        itemSurfaceHit = true;

                        break;
                    }
                default:
                    {
                        doorHit = false;
                        itemSurfaceHit = false;
                        break;
                    }



                    //additional space for more cases


                    break;


            }
        }

    }


	
	


    
}

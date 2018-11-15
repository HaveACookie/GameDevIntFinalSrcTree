using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandling : MonoBehaviour {

    private RaycastHit hit;
    public bool doorHit = false;
    public bool itemSurfaceHit = false;
    public bool shotHit = true;
    public bool shooting = true;
    public bool isKnifing = false;
    public bool shootStance = true;
    public bool shotgunShot1 = false;
    public bool shotgunShot2 = false;
    public bool shotgunShot3 = false;
    public bool shotgunShot4 = false; 
    public Transform shootMidTopStart, shootMidTopEnd;
    public Transform  shootLeftMidEnd;
    public Transform  shootRightMidEnd;
    public Transform  shootDownMidEnd;


    // Use this for initialization

    void shotgunCasting()
    {
        //LineCasting for shotgun
        Debug.DrawLine(shootMidTopStart.position, shootMidTopEnd.position, Color.cyan);
        shotgunShot1 = Physics.Linecast(shootMidTopStart.position, shootMidTopEnd.position, 1 << LayerMask.NameToLayer("Zombie"));
        Debug.DrawLine(shootMidTopStart.position, shootLeftMidEnd.position, Color.magenta);
        shotgunShot2 = Physics.Linecast(shootMidTopStart.position, shootLeftMidEnd.position, 1 << LayerMask.NameToLayer("Zombie"));
        Debug.DrawLine(shootMidTopStart.position, shootRightMidEnd.position, Color.yellow);
        shotgunShot3 = Physics.Linecast(shootMidTopStart.position, shootRightMidEnd.position, 1 << LayerMask.NameToLayer("Zombie"));
        Debug.DrawLine(shootMidTopStart.position, shootDownMidEnd.position, Color.yellow);
        shotgunShot4 = Physics.Linecast(shootMidTopStart.position, shootDownMidEnd.position, 1 << LayerMask.NameToLayer("Zombie"));
    }

    // Update is called once per frame
    void Update()
    {
        interactionCasting();
        shotgunCasting();
        shotCasting();
        //Checks to see if the raycast is in range and if the player is pressing the desired button
        if (Input.GetKey(KeyCode.E) && doorHit.Equals(true))
        {
            Debug.Log("DoorHitIsWorking");
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
        //Logic For Shootstance and shooting (needs inventory integration)
        if (shootStance = true && Input.GetKey(KeyCode.R))
        {
            shooting = true;
        }

        if (shooting == true && shotHit.Equals(true))
        {
            Debug.Log("ShotWorked");
        }
        if(shooting == true && shotgunShot1 == true)
        {
            //Should Deal Damage Here
        }
        if (shooting == true && shotgunShot2 == true)
        {
            //Should Deal Damage Here
        }
        if (shooting == true && shotgunShot3 == true)
        {
            //Should Deal Damage Here
        }
        if (shooting == true && shotgunShot4 == true)
        {
            //Should Deal Damage Here
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

    void knifeCasting()
    {   
        //Ask About spherecasting
        //DefineSphere
        RaycastHit sphereHit;
       Ray ray = new Ray(transform.position, transform.forward);
        Physics.SphereCast(ray, 1.5f, out sphereHit, 2);
        if (sphereHit.collider.tag.Equals("Enemy"))
        {

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
                case "SaveSurface":
                    {
                        Debug.Log("SaveHit");
                        // we can add a reference to saving management here
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

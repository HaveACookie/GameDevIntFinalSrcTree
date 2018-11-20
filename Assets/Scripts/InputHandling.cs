using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is a script that is added to the player object to allow shooting to take place


public class InputHandling : MonoBehaviour {

    private RaycastHit hit;

    //Surface Hit Bools
    public bool doorHit = false;
    public bool itemSurfaceHit = false;
    public bool shotHit = true;
    //Variables for Which weapon is being used
    public bool shooting = false;
    public bool shootingPistol = false;
    public bool isKnifing = false;
    public bool isShotGunning = false;
    public bool knifingSomeone = false;e
    

    public bool shootStance = true;
    //Ray/LineCast Bools and Transform Holders
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
        //LineCasting for shotgun, Debug line for visibility 
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
        //Refrences to Raycasting Methods
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

        //Puts Player in Shootstance 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shootStance = true;
        }
        else
        {
            shootStance = false;
        }
        if(shootStance == true && PlayerBehaviour.equip == 1 && Input.GetKeyDown(KeyCode.R))
        {
            knifingSomeone= true;
        }

        if(knifingSomeone == true && isKnifing == true)
        {
            //Damage here
        }

         //Checks what weapon is being used by the inventory and shoots if its the pistol
        if (shootStance == true && PlayerBehaviour.equip == 2 && Input.GetKeyDown(KeyCode.R))
        {
            shootingPistol = true;
            //we can put the animations and sound effects around here later
        }
        if (shootingPistol == true && shotHit.Equals(true))
        {
            //Deal Damage here
        }
        //Checks what weapon is being used by the inventory and shoots if its the shotgun 
        if (shootStance == true && PlayerBehaviour.equip == 3 && Input.GetKeyDown(KeyCode.R ))
        {
            isShotGunning = true; 
            //we can put the animations and sound effects around here later
        }
        //Hit Checkers for each bullet in the spread
        if(isShotGunning == true && shotgunShot1 == true)
        {
            //Should Deal Damage Here
        }
        if (isShotGunning == true && shotgunShot2 == true)
        {
            //Should Deal Damage Here
        }
        if (isShotGunning == true && shotgunShot3 == true)
        {
            //Should Deal Damage Here
        }
        if (isShotGunning == true && shotgunShot4 == true)
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
            isKnifing = true;
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

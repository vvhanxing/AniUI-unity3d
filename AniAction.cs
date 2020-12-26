using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AniAction : MonoBehaviour
{
    // Start is called before the first frame update
    public float LongTime =5.0f;
    public float[] LongTimeRange =new float[2] {5.0f,20.0f};
    public bool change_action ;
    public bool action  ;

    public float jumpForce =6.0f;
    public float WalkingSpeed = 1.0f;
    public float RunningSpeed = 2.0f;

    public string[] rotateTypes = new string[4] {"forward","rotateLeft","rotateRight","back"};
    public string[] actionTypes = new string[3] {"isIdling","isWalking","isRunning"};
    public string actionType = "isIdling";
    public string rotateType = "forward";

    public bool HitWall =false;

    public Animator animator;
    private Rigidbody rb;
    private FindObjectByRay findObjectByRay;
    private GameObject getCapture =null;
    //public Rigidbody rb;
    RaycastHit hit;

    void Start()
    {
        action=true;
        actionType = actionTypes[0];
        rotateType = "forward";
        animator = GetComponent<Animator>();

        findObjectByRay = GetComponent<FindObjectByRay>();
        rb = GetComponent<Rigidbody> ();
        //rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        //if (Physics.Raycast(transform.position,-transform.up,out hit,200))
        //{
            //transform.up = hit.normal;

        //}
        //transform.up = Vector3.up;

        

        
        if (findObjectByRay!=null)
        {
            getCapture = findObjectByRay.findObj;


            if (getCapture ==null)
            {
            //
            randomAction();
            
            }

            else

            {
 
            transform.up = Vector3.up;
            rb.angularVelocity =new Vector3(0,0,0);
            //transform.LookAt(getCapture.transform) ;
            //transform.forward = new Vector3( getCapture.transform.positio[] , getCapture.transform.position,getCapture.transform.position[] );
            transform.rotation =  Quaternion.LookRotation(-transform.position+getCapture.transform.position,transform.up);
            //transform.rotation =  Quaternion.LookRotation(getCapture.transform.position );

            Debug.DrawLine(transform.position, getCapture.transform.position, Color.blue, 1);
            doAction("isRunning");

            }
        }


        else
        {
            randomAction();
        }

        




    }


    void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag=="wall")
       {

            
            if ( Vector3.Angle(new Vector3(0,1,0),collision.contacts[0].normal) >=80.0f )
            {
                notForwardAction();
            }
            
            //doAction(actionTypes[1]);

            
       }


       if (collision.gameObject.tag==gameObject.tag)
       {
           notForwardAction();
       }


       if (collision.gameObject ==getCapture)
       {

           eat(getCapture);

       }



    }

    void OnCollisionStay(Collision collision)
    {
       if (collision.gameObject.tag=="wall")
       {

            if ( Vector3.Angle(new Vector3(0,1,0),collision.contacts[0].normal) >=80.0f )
            {
                notForwardAction();
            }
            
       }

       if (collision.gameObject.tag==gameObject.tag)
       {
           notForwardAction();
           //doAction("isRunning");
           //run back
       }


       if (collision.gameObject ==getCapture)
       {
           eat(getCapture);

       }



    }



    void randomAction()
    {

        change_action = (int) Time.time%LongTime==0;
        //Debug.Log(change_action.ToString()+ " "+ action.ToString());
        //animator.SetFloat("speed", Mathf.Abs( rb.velocity[0])+ Mathf.Abs( rb.velocity[1])+ Mathf.Abs( rb.velocity[2]));
        if (action && change_action)
        {

            //Debug.Log(action) ;
            transform.up = Vector3.up;
            rb.angularVelocity =new Vector3(0,0,0);

            //change_action doaction point
            int SelectAction = (int) Random.Range(0.0f,actionTypes.Length);
            actionType =  actionTypes[SelectAction];

            int rotateSelectAction = (int) Random.Range(0.0f,rotateTypes.Length);
            rotateType =  rotateTypes[rotateSelectAction];

            LongTime = (int)  Random.Range(LongTimeRange[0],LongTimeRange[1]);
            HitWall =  false;

            doAction(rotateType);

           //if (Physics.Raycast(transform.position,-transform.up,out hit,200))
             //{
             //transform.up = hit.normal;

             //}
             


        }
        
        else
        {

            doAction(actionType); 
            
            
        }



        if (change_action)
        {
            action =  false;
        }

        else
        {
            action =  true;
        }

    }





    void doAction(string doactionType)
    {
    
        if (doactionType== "forward")
        {
            transform.Rotate (0.0f,0.0f ,0.0f);
        }

        if (doactionType== "back")
        {
            transform.Rotate (0.0f,180.0f ,0.0f);
        }

        if ( doactionType== "rotateLeft")
        {
            transform.Rotate (0.0f,90.0f ,0.0f);
        }

        if (doactionType== "rotateRight")
        {
            transform.Rotate (0.0f,-90.0f ,0.0f);
        }



        if (animator!=null)
        {

            foreach(string actionType in actionTypes)
            {
                if (actionType == doactionType )
                {
                    animator.SetBool(actionType,true);
                }
                else
                {
                   animator.SetBool(actionType,false);
                }
            }
            
        }




        if (doactionType=="isWalking")
        {
            transform.Translate(0,0, Time.deltaTime*WalkingSpeed);
            //GetComponent<CharacterController>().SimpleMove(transform.forward*WalkingSpeed);

        }


        if (doactionType=="isRunning")
        {
            transform.Translate(0,0, Time.deltaTime*RunningSpeed);
            //GetComponent<CharacterController>().SimpleMove(transform.forward*RunningSpeed);

        }


        else if (doactionType=="isJumping")
        {
            transform.Translate(0,0, Time.deltaTime*RunningSpeed);
            transform.Translate(0,Time.deltaTime*jumpForce,0);
            //GetComponent<CharacterController>().SimpleMove(transform.forward*WalkingSpeed);

        } 


        else 
        {
            transform.Translate(0,0, 0);
        }

        //else (doactionType=="isIdling")
        //{
            //transform.Translate(0,0, 0);
        //}


        //if (doactionType=="isHowling")
        //{
            //transform.Translate(0,0, 0);
        //}
        //if (doactionType=="isEating")
        //{
            //transform.Translate(0,0, 0);
        //}



    }



    void eat(GameObject Eaten)
    {

        Destroy(Eaten);
    }

    void notForwardAction()
    {
        int rotateSelectAction = (int) Random.Range(1.0f,rotateTypes.Length);
        rotateType =  rotateTypes[rotateSelectAction];
        doAction(rotateType);
        HitWall = true;

    }


}




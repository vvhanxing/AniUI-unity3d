using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AniAction : MonoBehaviour
{
    // Start is called before the first frame update
    public float LongTime = 5.0f;
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
    private FindObjectByRay findObjectByRay;
    private GameObject getCapture =null;
    //public Rigidbody rb;

    void Start()
    {
        action=true;
        actionType = actionTypes[0];
        rotateType = "forward";
        animator = GetComponent<Animator>();

        findObjectByRay = GetComponent<FindObjectByRay>();
        //rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        

        if (findObjectByRay!=null)
        {
            getCapture = findObjectByRay.findObj;


            if (getCapture ==null)
            {
            randomAction();
            }

            else

            {
 
            
            transform.LookAt(getCapture.transform) ;
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
            int rotateSelectAction = (int) Random.Range(1.0f,rotateTypes.Length);
            rotateType =  rotateTypes[rotateSelectAction];
            doAction(rotateType);
            HitWall = true;
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
            int rotateSelectAction = (int) Random.Range(1.0f,rotateTypes.Length);
            rotateType =  rotateTypes[rotateSelectAction];
            doAction(rotateType);
            HitWall = true;
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

            //change_action doaction point
            int SelectAction = (int) Random.Range(0.0f,actionTypes.Length);
            actionType =  actionTypes[SelectAction];

            int rotateSelectAction = (int) Random.Range(0.0f,rotateTypes.Length);
            rotateType =  rotateTypes[rotateSelectAction];

            LongTime = (int)  Random.Range(5.0f,20.0f);
            HitWall =  false;

            doAction(rotateType);
            

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




        if (doactionType=="isIdling")
        {
            transform.Translate(0,0, 0);

        }
        if (doactionType=="isEating")
        {
            transform.Translate(0,0, 0);

        }

        if (doactionType=="isWalking")
        {
            transform.Translate(0,0, Time.deltaTime*WalkingSpeed);

        }


        if (doactionType=="isRunning")
        {
            transform.Translate(0,0, Time.deltaTime*RunningSpeed);

        }


        if (doactionType=="isJumping")
        {
            transform.Translate(0,0, Time.deltaTime*RunningSpeed);
            transform.Translate(0,Time.deltaTime*jumpForce,0);
            
        } 




    }



    void eat(GameObject Eaten)
    {

        Destroy(Eaten);
    }




}

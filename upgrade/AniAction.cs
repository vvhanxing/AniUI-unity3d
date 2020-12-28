using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AniAction : MonoBehaviour
{
    // Start is called before the first frame update
    public float LongTime =5.0f;
    private float decisionTime =0.0f;
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
    private float getCaptureDistance = 30;
    //public Rigidbody rb;

    public float FightDistance = 1.0f;
    private float eatFreq = 3.0f;
    private float eatTime = 0.0f;

    

    RaycastHit hit;

    public hp HP;

    void Start()
    {
        action=true;
        actionType = actionTypes[0];
        rotateType = "forward";
        animator = GetComponent<Animator>();
        HP = GetComponent<hp>();

        findObjectByRay = GetComponent<FindObjectByRay>();
        rb = GetComponent<Rigidbody> ();
        //change_action = true;
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

        if (gameObject.GetComponent<hp>().hpValue <= 0)
        {
            actionType = "isDead";
            doAction(actionType);
            //print("isDead");
        }        

        
        if (findObjectByRay!=null)
        {
            getCapture = findObjectByRay.findObj;




            if (getCapture !=null)

            {
 
                transform.up = Vector3.up;
                rb.angularVelocity =new Vector3(0,0,0);
                //transform.LookAt(getCapture.transform) ;
                //transform.forward = new Vector3( getCapture.transform.positio[] , getCapture.transform.position,getCapture.transform.position[] );
                //Vector3 lookHight = new Vector3(0,getCapture.GetComponent<Renderer>().bounds.size.y,0);
                transform.rotation =  Quaternion.LookRotation(getCapture.transform.position-(transform.position),transform.up);
                //transform.rotation =  Quaternion.LookRotation(getCapture.transform.position );

                Debug.DrawLine(transform.position, getCapture.transform.position, Color.blue, 1);
            
                getCaptureDistance = findObjectByRay.findObjDistance ;

            
                if (getCaptureDistance>FightDistance&& getCapture !=null)
                {
                    actionType = "isRunning";
                    //doAction(actionType);
                    decisionTime = 0.0f;
                    action = true;
                    change_action=true;
                }
                if (getCaptureDistance<FightDistance&& getCapture !=null)
                {
                    actionType = "isAttacking";
                    decisionTime = 0.0f;
                    action = true;
                    change_action=true;
                    
                    //rb.velocity =new Vector3(0,0,0); //.velocity
                    //transform.up = Vector3.up;
                    //doAction(actionType);
                }

            

                //transform.up = Vector3.up;
                doAction(actionType);
                //print(string.Format("isAttack-121-{0}",Time.time) );
                
            


            

            }
            //print(string.Format("isAttack-122-{0}",Time.time) );


            if (getCapture ==null)
            {
                //print(string.Format("isAttack-129-{0}-{1}",Time.time,actionType) );
                //actionType = "isIdling";
                //change_action = true;

                randomAction();

            }

        }


        else
        {   //actionType = "isIdling";
        
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

        if (findObjectByRay!=null)
        {
            getCapture = findObjectByRay.findObj;

            if (getCapture ==null)
            {
               if (collision.gameObject.tag==gameObject.tag)
               {
           
                   notForwardAction();
               }
            }
        }


       if (collision.gameObject ==getCapture)
       {

           eat(getCapture);
           //change_action = true;
           

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

        if (findObjectByRay!=null)
        {
            getCapture = findObjectByRay.findObj;

            if (getCapture ==null)
            {
               if (collision.gameObject.tag==gameObject.tag)
               {
           
                   notForwardAction();
               }
            }
        }


       if (collision.gameObject ==getCapture)
       {
           eat(getCapture);
           //change_action = true;

       }



    }



    void randomAction()
    {
        //actionType = "isIdling"
        
        //Debug.Log(change_action.ToString()+ " "+ action.ToString());
        //animator.SetFloat("speed", Mathf.Abs( rb.velocity[0])+ Mathf.Abs( rb.velocity[1])+ Mathf.Abs( rb.velocity[2]));
        
        //
        
          
        //print(string.Format("isAttack-239-{0}-{1}-{2}-{3}-{4}-{5}",Time.time,actionType,action,change_action,change_action,decisionTime ) );
        //print(change_action);     
        rb.angularVelocity =new Vector3(0,0,0); //.velocity        
        //transform.up = Vector3.up;
        if (action && change_action)
        {

            //Debug.Log(action) ;
            


            //change_action doaction point
            int SelectAction = (int) Random.Range(0.0f,actionTypes.Length);
            actionType =  actionTypes[SelectAction];
            if (actionType=="isRunning" || actionType=="isAttacking" || actionType=="isDead")
            {
                actionType = "isWalking";

            }

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
    
    decisionTime +=Time.deltaTime;
    change_action = (int) decisionTime%LongTime==0; 
    }





    void doAction(string doactionType)
    {
    
        if (doactionType== "forward")
        {
            transform.up = Vector3.up;
            transform.Rotate (0.0f,0.0f ,0.0f);
        }

        if (doactionType== "back")
        {
            transform.up = Vector3.up;
            transform.Rotate (0.0f,180.0f ,0.0f);
        }

        if ( doactionType== "rotateLeft")
        {
            transform.up = Vector3.up;
            transform.Rotate (0.0f,90.0f ,0.0f);
        }

        if (doactionType== "rotateRight")
        {
            transform.up = Vector3.up;
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
        
        if (doactionType=="isAttacking" && getCapture !=null)
         {
            //transform.Translate(0,0, Time.deltaTime*RunningSpeed);
            print("hello");
            //GetComponent<CharacterController>().SimpleMove(transform.forward*WalkingSpeed);

        }       


        else if (doactionType=="isJumping")
        {
            transform.Translate(0,0, Time.deltaTime*WalkingSpeed);
            //transform.Translate(0,Time.deltaTime*jumpForce,0);
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
        transform.up = Vector3.up;
        eatTime  +=Time.deltaTime;
        if (eatTime>=eatFreq && Eaten !=null)
        {
            HP.hpValue+=1;
            Eaten.GetComponent<hp>().hpValue-=1;
            eatTime = 0.0f;
        }

        
    }

    void notForwardAction()
    {
        int rotateSelectAction = (int) Random.Range(1.0f,rotateTypes.Length);
        rotateType =  rotateTypes[rotateSelectAction];
        doAction(rotateType);
        HitWall = true;
        //print("not fordward!!!!!!!!");

    }


}




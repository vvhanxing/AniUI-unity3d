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
    private float playerHight;

    private FindObjectByRay findObjectByRay;
    private GameObject getCapture =null;
    private float getCaptureDistance = 30;
    //public Rigidbody rb;

    public float FightDistance = 1.0f;
    public float ScareDistance = 1.0f;


    private Vector3 PlayerPoint ;
    private Vector3 CapturePoint ;
    private float eatFreq = 3.0f;
    private float eatTime = 0.0f;

    private GameObject collision_gameObject ;

    

    RaycastHit hit;

    public hp HP;

    void Start()
    {
        action=true;
        change_action =true;

        actionType = actionTypes[0];
        rotateType = "forward";
        animator = GetComponent<Animator>();
        HP = GetComponent<hp>();

        findObjectByRay = GetComponent<FindObjectByRay>();
        rb = GetComponent<Rigidbody> ();
        playerHight = GetComponent<BoxCollider>().size.y*transform.localScale.y;


    }

    // Update is called once per frame
    void FixedUpdate()
    {



        if (gameObject.GetComponent<hp>().hpValue <= 0)
        {
            actionType = "isDead";
            doAction(actionType);

        }

        if (HitWall ==true )
        {
            actionType = "isDead";
            doAction(actionType);
            decisionTime +=Time.deltaTime;

            if (decisionTime>10.0f)
            {
                HitWall =false;
                decisionTime =0.0f;
                action = true;
                change_action=true;                
                
            }


        }

///////////////////////////////////////
        
        else if (HitWall ==false && findObjectByRay!=null)
        {
            getCapture = findObjectByRay.findObj;


            if (getCapture !=null)

            {
                if (HP.CanBeEatTag.Contains (getCapture.tag))
                {
                    doScare();

                }

                else 
                
                {
                    doCapture();

                }
                
         
            }

            if (getCapture ==null)
            {

                decisionTime=0.0f;
                randomAction();

            }

        }
//////////////////////////////////////////////


        else
        {   
        
            randomAction();
        }


    }




    void OnCollisionEnter(Collision collision)
    {
       collision_gameObject = collision.gameObject;
       if (collision_gameObject.tag=="wall" && getCapture ==null)
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
               if (collision_gameObject.tag==gameObject.tag)
               {
           
                   if ( Vector3.Angle(new Vector3(0,1,0),collision.contacts[0].normal) >=80.0f )
                     {           

                     notForwardAction();
                     }
               }
            }
        }


        if (collision_gameObject.tag=="Player" )
        {

            HitWall = true;
        }





       if (collision_gameObject ==getCapture)
       {

           eat(getCapture);
           //change_action = true;
           

       }



    }

    void OnCollisionStay(Collision collision)
    {
       collision_gameObject = collision.gameObject;
       if (collision_gameObject.tag=="wall" && getCapture ==null)
       {

            if ( Vector3.Angle(new Vector3(0,1,0),collision.contacts[0].normal) >=80.0f )
            {
                //notForwardAction();
                transform.forward =  collision.contacts[0].normal;
            }
            
       }

       if (collision_gameObject.tag=="wall" && getCapture !=null)
       {

            if ( Vector3.Angle(new Vector3(0,1,0),collision.contacts[0].normal) >=80.0f )
            {

                doAction("isJumping");

            }
            
       }


        if (findObjectByRay!=null)
        {
            getCapture = findObjectByRay.findObj;

            if (getCapture ==null)
            {
               if (collision_gameObject.tag==gameObject.tag)
               {
                   if ( Vector3.Angle(new Vector3(0,1,0),collision.contacts[0].normal) >=80.0f )
                     {           

                     notForwardAction();
                     }
               }
            }
        }


       if (collision_gameObject ==getCapture)
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
            if (actionType=="isRunning" || actionType=="isAttacking" || actionType=="isDead" ||actionType=="isJumping" || actionType=="isAttacking1"||actionType=="isScare"||actionType=="isScare1")
            {
                actionType = "isWalking";

            }

            int rotateSelectAction = (int) Random.Range(0.0f,rotateTypes.Length);
            rotateType =  rotateTypes[rotateSelectAction];

            LongTime = (int)  Random.Range(LongTimeRange[0],LongTimeRange[1]);
            ///////HitWall =  false;

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
    
    //decisionTime +=Time.deltaTime;
    change_action = (int) Time.time%LongTime==0; 
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
        

        if (doactionType=="isScare")
        {
            transform.Translate(0,0, Time.deltaTime*RunningSpeed);
            //GetComponent<CharacterController>().SimpleMove(transform.forward*RunningSpeed);

        }



        if (doactionType=="isAttacking" && getCapture !=null)
         {
            //transform.Translate(0,0, Time.deltaTime*RunningSpeed);
            //print("hello");
            //GetComponent<CharacterController>().SimpleMove(transform.forward*WalkingSpeed);
            transform.Translate(0,0, 0);

        }       


        else if (doactionType=="isJumping")
        {
            //transform.Translate(0,0, Time.deltaTime*WalkingSpeed);
            transform.Translate(0,Time.deltaTime*jumpForce,0);
            //GetComponent<CharacterController>().SimpleMove(transform.forward*WalkingSpeed);

        } 


        else 
        {
            transform.Translate(0,0, 0);
        }



    }



    void eat(GameObject Eaten)
    {
        //transform.up = Vector3.up;
        
        if (decisionTime<=0.0f && Eaten !=null)
        {
            HP.hpValue+=1;
            Eaten.GetComponent<hp>().hpValue-=1;
            decisionTime = eatFreq;
        }
        decisionTime  -=Time.deltaTime;

        
    }


    void notForwardAction()
    {
        int rotateSelectAction = (int) Random.Range(1.0f,rotateTypes.Length);
        rotateType =  rotateTypes[rotateSelectAction];
        doAction(rotateType);
        ///////HitWall = true;


    }




    void doCapture()
    {
 
        transform.up = Vector3.up;
        rb.angularVelocity =new Vector3(0,0,0);
                
        PlayerPoint =new Vector3(transform.position[0],transform.position[1]+playerHight,transform.position[2]) ;
        CapturePoint = new Vector3(getCapture.transform.position[0],transform.position[1]+playerHight,getCapture.transform.position[2]);
        //print(string.Format("{0}",playerHight));

        transform.rotation =  Quaternion.LookRotation(CapturePoint-PlayerPoint,transform.up);
        //transform.rotation =  Quaternion.LookRotation(getCapture.transform.position );

        Debug.DrawLine(PlayerPoint,CapturePoint, Color.blue, 1);  
        getCaptureDistance = findObjectByRay.findObjDistance ;

            
        if (getCaptureDistance>FightDistance && getCapture !=null)
        {
            actionType = "isRunning";
            decisionTime = 0.0f;
            action = true;
            change_action=true;

        }

        if (getCaptureDistance<FightDistance && getCapture !=null)
        {
            decisionTime-=Time.deltaTime;
            if (decisionTime<=0.0f )
            {
                string[] fightTypes = new string[2] {"isAttacking","isAttacking1"};
                actionType = fightTypes [(int) Random.Range(0.0f,fightTypes.Length)];                       
                decisionTime = 3.0f;
            }


            action = true;
            change_action=true;

        }                   
                

        doAction(actionType);

       


    }

    void doScare()
    {


        transform.up = Vector3.up;
        rb.angularVelocity =new Vector3(0,0,0);
                
        PlayerPoint =new Vector3(transform.position[0],transform.position[1]+playerHight,transform.position[2]) ;
        CapturePoint = new Vector3(getCapture.transform.position[0],transform.position[1]+playerHight,getCapture.transform.position[2]);
        //print(string.Format("{0}",playerHight));

        transform.rotation =  Quaternion.LookRotation(-CapturePoint+PlayerPoint,transform.up);
        //transform.rotation =  Quaternion.LookRotation(getCapture.transform.position );

        Debug.DrawLine(PlayerPoint,CapturePoint, Color.yellow, 1);  
        getCaptureDistance = findObjectByRay.findObjDistance ;

            
        if (getCaptureDistance>ScareDistance && getCapture !=null)
        {
            actionType = "isScare";
            decisionTime = 0.0f;
            action = true;
            change_action=true;

        }

        if (getCaptureDistance<ScareDistance && getCapture !=null)
        {
            decisionTime-=Time.deltaTime;
            if (decisionTime<=0.0f )
            {
                string[] fightTypes = new string[2] {"isScare1","isScare1"};
                actionType = fightTypes [(int) Random.Range(0.0f,fightTypes.Length)];                       
                decisionTime = 3.0f;
            }


            action = true;  //for random action
            change_action=true;

        }                   
                

        doAction(actionType);

       




    }






}




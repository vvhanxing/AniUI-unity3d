using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjectByRay : MonoBehaviour
{
    // Start is called before the first frame update
    //public string findObjTag;
    public GameObject findObj; 
    public float angleRange = 45.0f;
    public float distanceRange = 20.0f;
    public float giveUpDistance = 10.0f;
    public float giveUpTime = 10f;
    public float giveUpTimeCount = 0.0f;

    public float findObjDistance =30.0f; //findObjDistance>giveUpDistance

    public string[] FindObjsClassName = new string[1] ;

    private Vector3 findObjDirection;

    
    private List<float> KnifeList;
    private Dictionary<float, GameObject> knifeDic;

    private float playerHight;
    private Vector3 PlayerPoint ;
    private Vector3 CapturePoint ;


    private GameObject FindObjs;
    private GameObject obj ;
    private Vector3 objDirection;
    private  float angle;


    //public DrawSector drawSector ;
    private Vector3 newVecA;
    private Vector3 newVecB;

    

    void Start()
    {

         //drawSector =GetComponent<DrawSector>();
         playerHight = GetComponent<BoxCollider>().size.y*transform.localScale.y;
        
    }

    void FixedUpdate()
    {
        PlayerPoint =new Vector3(transform.position[0],transform.position[1]+playerHight,transform.position[2]) ;


        if (findObj==null) 
        {
            Finding() ;
            
            
        }


        else
        {
            CapturePoint = new Vector3(findObj.transform.position[0],transform.position[1]+playerHight,findObj.transform.position[2]);

            findObjDirection = CapturePoint - PlayerPoint;
            findObjDistance = findObjDirection.magnitude;
            RaycastHit hitfindObj;

            if (findObjDistance>giveUpDistance )
            {
                findObj=null;
 
            }
            
            if (Physics.Raycast( PlayerPoint, findObjDirection , out hitfindObj,distanceRange))
            {
                if (hitfindObj.collider.gameObject != findObj)
                {

                    giveUpTimeCount +=Time.deltaTime;
                    //print(giveUpTimeCount);
                
                    if (giveUpTimeCount>giveUpTime)
                    {
                        findObj=null;
                        giveUpTimeCount = 0.0f;

                    }

                }

            }


        }
        
    }    






    void Finding()
    {

        



        knifeDic = new Dictionary<float, GameObject>();//初始化       
        KnifeList = new List<float>();
        foreach (string FindObjName in FindObjsClassName)
        {
            FindObjs = GameObject.Find(FindObjName);
            if (FindObjs!=null)
            {
            for (int count =0;count < FindObjs.transform.childCount; count++)
            //foreach (GameObject obj in GameObject.FindGameObjectsWithTag(findObjTag)) 
            {
                obj = FindObjs.transform.GetChild(count).gameObject;
                objDirection = obj.transform.position - PlayerPoint;
                float distance =  objDirection.magnitude;

                RaycastHit hit;

                if (distance<=distanceRange)
                {

            
                    if (Physics.Raycast( PlayerPoint,objDirection , out hit,distanceRange))
                    {
                        if (hit.collider.gameObject ==obj)
                        {
                            //Debug.DrawLine(transform.position, obj.transform.position, Color.red, 1);
 
                            angle = Vector3.Angle (transform.forward, objDirection);                    
                    
                            if (angle <=angleRange)
                            {
                            //print( string.Format("{0} {1} {2}",obj.name,distance,angle));
                                Debug.DrawLine(transform.position, obj.transform.position, Color.blue, 1);

                                knifeDic.Add(distance, obj);

                                if (!KnifeList.Contains(distance))
                                {
                                    KnifeList.Add(distance);
                                }
                            }


                        }
                    
                    
                    }

                }

            }
            
            }

        }
        KnifeList.Sort();
        //print(string.Format("----> {0}",KnifeList));
        if (KnifeList. Count!=0)knifeDic.TryGetValue(KnifeList[0],out findObj);
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        newVecA = Quaternion.AngleAxis(angleRange,new Vector3(0,1,0))*transform.forward;
        newVecB = Quaternion.AngleAxis(-angleRange,new Vector3(0,1,0))*transform.forward;

        //Vector3 point = new Vector3(transform.position[0],transform.position[1]+playerHight,transform.position[2]);
        
        Gizmos.DrawLine(PlayerPoint, PlayerPoint+newVecA*distanceRange);
        Gizmos.DrawLine(PlayerPoint, PlayerPoint+newVecB*distanceRange);
    }

}

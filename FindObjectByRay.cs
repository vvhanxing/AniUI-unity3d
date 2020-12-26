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
    public float findObjDistance =30.0f;

    public GameObject[] FindObjsClass =new GameObject[1];

    
    private List<float> KnifeList;
    private Dictionary<float, GameObject> knifeDic;

    //public DrawSector drawSector ;
    

    void Start()
    {

         //drawSector =GetComponent<DrawSector>();
        
    }

    void FixedUpdate()
    {
        if (findObj==null) 
        {
            Finding() ;
            drowView();
            
        }


        else
        {
            Vector3 findObjDirection = findObj.transform.position - transform.position;
            findObjDistance = findObjDirection.magnitude;
            RaycastHit hitfindObj;
            if (findObjDistance>giveUpDistance && Physics.Raycast( transform.position, findObjDirection , out hitfindObj,distanceRange)==false)
            {
                findObj=null;
 
            }
        }
        
    }    






    void Finding()
    {

        



        knifeDic = new Dictionary<float, GameObject>();//初始化       
        KnifeList = new List<float>();
        foreach (GameObject FindObjs in FindObjsClass)
        {
            for (int count =0;count < FindObjs.transform.childCount; count++)
            //foreach (GameObject obj in GameObject.FindGameObjectsWithTag(findObjTag)) 
            {
                GameObject obj = FindObjs.transform.GetChild(count).gameObject;
                Vector3 objDirection = obj.transform.position - transform.position;
                float distance =  objDirection.magnitude;

                RaycastHit hit;

                if (distance<=distanceRange)
                {

            
                    if (Physics.Raycast( transform.position,objDirection , out hit,distanceRange))
                    {
                        if (hit.collider.gameObject ==obj)
                        {
                            //Debug.DrawLine(transform.position, obj.transform.position, Color.red, 1);
 
                            float angle = Vector3.Angle (transform.forward, objDirection);                    
                    
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
        KnifeList.Sort();
        //print(string.Format("----> {0}",KnifeList));
        if (KnifeList. Count!=0)knifeDic.TryGetValue(KnifeList[0],out findObj);
        
    }
    void drowView()
    {
        Vector3 newVecA = Quaternion.AngleAxis(angleRange,new Vector3(0,1,0))*transform.forward;
        Vector3 newVecB = Quaternion.AngleAxis(-angleRange,new Vector3(0,1,0))*transform.forward;
        //Vector3 pointA = new Vector3(transform.position[0]+distanceRange*Mathf.Tan(angleRange),transform.position[1],transform.position[2]+distanceRange);
        //Vector3 pointB = new Vector3(transform.position[0]-distanceRange*Mathf.Tan(angleRange),transform.position[1],transform.position[2]+distanceRange);
        Debug.DrawLine(transform.position, transform.position+newVecA*distanceRange, Color.red, 1);
        Debug.DrawLine(transform.position, transform.position+newVecB*distanceRange, Color.red, 1);
    }

}

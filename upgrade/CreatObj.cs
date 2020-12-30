using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatObj : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit hit;
    //public GameObject parentObj; 
    //public GameObject rayObj;
    public GameObject creatObjsClass;
    public GameObject[]  creatObjs = new GameObject[4];
    public GameObject[]  creatObjs_layer1 = new GameObject[4];
    public GameObject[]  creatObjs_layer2 = new GameObject[4];

    private GameObject creatObj;
   

    public int Num = 20;
    public GameObject mapObj ; //
    public bool IsOcclusion =false;
    public float mapRange = 50.0f;
    public bool isDiv =false;
    public int mapDiv = 5;
    public float[] ScaleRange =  new float[2]{1.0f,2.0f};
    public float distanceFromMap = 0.0f;
    public float angle  =0.0f;
    float RandomRangeX = 0.0f;
    float RandomRangeZ = 0.0f;

    void Start()
    {
        if (isDiv==true){
            adding2();

        }
        
        
   

    }

    // Update is called once per frame
    void Update()
    {
        if (isDiv==false)
        {

            int NumCount = creatObjsClass.transform.childCount;
            if (NumCount<Num )
            {
                adding(Num - NumCount);
            }

        }



            



       
    }




    private void adding(int addNum)
    {
    
        for (int i = 0; i < addNum; i++) 
        {
            RandomRangeX = transform.position[0]+Random.Range(-mapRange,mapRange);
            RandomRangeZ= transform.position[2]+Random.Range(-mapRange,mapRange);
            Vector3 p = new Vector3(RandomRangeX,100.0f,RandomRangeZ);

        

            if (Physics.Raycast(p,-transform.up,out hit,200))
        
            {
                if (IsOcclusion==true)
                {

                    if (hit.collider.name==mapObj.name)
                    {
                        creatObj = creatObjs [Random.Range(0,creatObjs.Length)];
 
                        Instantiate(creatObj, new Vector3(hit.point[0],hit.point[1]+distanceFromMap,hit.point[2]),transform.rotation);

                        creatObj.transform.rotation = Quaternion.Euler(0,0,0);

                        float randomScale =  Random.Range(ScaleRange[0],ScaleRange[1]);
                        creatObj.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
                    }

                }

                else
                {
                        creatObj = creatObjs [Random.Range(0,creatObjs.Length)];
 
                        GameObject newCreatObj = Instantiate(creatObj, new Vector3(hit.point[0],hit.point[1]+distanceFromMap,hit.point[2]),transform.rotation);

                        newCreatObj.transform.rotation = Quaternion.Euler(0,0,0);

                        float randomScale =  Random.Range(ScaleRange[0],ScaleRange[1]);
                        newCreatObj.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
                        newCreatObj.transform.parent = creatObjsClass.transform;

                }

 

            }

        }        
    }



    private void adding2()
    {
        
        int Px = (int)(2.0f*mapRange/mapDiv);
        int Py = (int)(2.0f*mapRange/mapDiv);

        for (int layer = 0; layer < 3 ;layer++) 
        {
            for (int i = 0; i < mapDiv; i++) 
            {
                for (int j = 0; j < mapDiv; j++) 
                {
                    float dPx = (i)*Px+0.5f*Px-mapRange;
                    float dPy = (j)*Py+0.5f*Py-mapRange;
                    Vector3 p = new Vector3(dPx+transform.position[0],100.0f,dPy+transform.position[2]);




                    if (Physics.Raycast(p,-transform.up,out hit,200))
        
                    {

                        
                        if (layer == 0)
                        {
                             
                            creatObj = creatObjs [Random.Range(0,creatObjs.Length)];
                        }
                         if (layer == 1)
                        {
                            //print(hit.collider.name);
                            if (hit.collider.name!=mapObj.name )
                            {

                                creatObj = creatObjs_layer1[Random.Range(0,creatObjs_layer1.Length)];

                            }
                            else
                            {
                                creatObj =null;
                            }
                            
                        }
                        if (layer == 2)
                        {
                             
                            //print(hit.collider.name);
                            if (hit.collider.name!=mapObj.name )
                            {

                                creatObj = creatObjs_layer1[Random.Range(0,creatObjs_layer1.Length)];

                            }
                            else
                            {
                                creatObj =null;
                            }
                        }


                        
                        if (creatObj!=null)
                        {
                            GameObject newCreatObj = Instantiate(creatObj, new Vector3(hit.point[0],hit.point[1]+distanceFromMap,hit.point[2]),transform.rotation);

                            newCreatObj.transform.rotation = Quaternion.Euler(0,0,0);

                            float randomScale =  Random.Range(ScaleRange[0],ScaleRange[1]);
                            newCreatObj.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
                            newCreatObj.transform.parent = creatObjsClass.transform;
                        }


 

                    }                


                
                

                }
            }
        }
    
      
    }









    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Vector3 pointA = new Vector3(transform.position[0]+mapRange,transform.position[1],transform.position[2]+mapRange);
        Vector3 pointB = new Vector3(transform.position[0]-mapRange,transform.position[1],transform.position[2]+mapRange);
        Vector3 pointC = new Vector3(transform.position[0]+mapRange,transform.position[1],transform.position[2]-mapRange);
        Vector3 pointD = new Vector3(transform.position[0]-mapRange,transform.position[1],transform.position[2]-mapRange);
        Gizmos.DrawLine(pointA, pointB);
        Gizmos.DrawLine(pointA, pointC);
        Gizmos.DrawLine(pointB, pointD);
        Gizmos.DrawLine(pointC, pointD);
    }




}
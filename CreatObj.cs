using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatObj : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit hit;
    //public GameObject parentObj; 
    //public GameObject rayObj;
    public GameObject[]  envObjs = new GameObject[4];
    public int Num = 20;
    public GameObject mapObj ; 
    public float mapRange = 50.0f;
    public float[] ScaleRange =  new float[2]{1.0f,2.0f};
    public float distanceFromMap = 0.0f;
    public float angle  =0.0f;
    float RandomRangeX = 0.0f;
    float RandomRangeZ = 0.0f;

    void Start()
    {

        for (int i = 0; i < Num; i++) 
        {
            RandomRangeX = transform.position[0]+Random.Range(-mapRange,mapRange);
            RandomRangeZ= transform.position[2]+Random.Range(-mapRange,mapRange);
            Vector3 p = new Vector3(RandomRangeX,100.0f,RandomRangeZ);
            //print(transform.position[0].ToString()+" "+transform.position[1].ToString()+" "+p.ToString());
            

            
            

            //rayObj.transform.position=new Vector3(RandomRangeX,100.0f,RandomRangeZ);

        

            if (Physics.Raycast(p,-transform.up,out hit,200))
        
            {

                if (hit.collider.name==mapObj.name)
                {
                    GameObject envObj = envObjs [Random.Range(0,envObjs.Length)];
 
                    Instantiate(envObj, new Vector3(hit.point[0],hit.point[1]+distanceFromMap,hit.point[2]),transform.rotation);

                    //envObj.transform.position = new Vector3(hit.point[0],hit.point[1]+distanceFromMap,hit.point[2]);
                    //print(hit.point);
                    envObj.transform.rotation = Quaternion.Euler(0,0,0);

                    float randomScale =  Random.Range(ScaleRange[0],ScaleRange[1]);
                    envObj.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
                }
                //Debug.Log(hit.point[0]);
                
                //Debug.Log(hit.collider.name);


               



            }


        }
        

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

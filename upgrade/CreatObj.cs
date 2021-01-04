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

    public int N_layers = 2;

    public List<GameObject> creatObjs_layer0 = new List<GameObject>();
    //public List<bool> layer0_isStacked = new List<bool>();

    //public List<GameObject> creatObjs_layer1 = new List<GameObject>();
    //public List<bool> layer1_isStacked = new List<bool>();

    //public List<GameObject> can_selset_list = new List<GameObject>();
    //public List<bool> layer2_isStacked = new List<bool>();
    //public List<GameObject> StackObjectsClass = new List<GameObject>();

    //public Dictionary<GameObject,GameObject> StackedObjectSelectDict = new Dictionary<GameObject,GameObject>();

    //public Dictionary<GameObject,bool> dic_isStacked = new Dictionary<GameObject,bool>();
    //public List<string> mList = new List<string>();
    //private Dictionary<float, GameObject> knifeDic;

    private List<GameObject> creatObjs_layer ;
    private List<bool> layer_isStacked ;


    private GameObject creatObj;
   

    public int Num = 20;
    public GameObject mapObj ; //
    public bool IsOcclusion =false;
    public float mapRange = 50.0f;
    public bool isDiv =false;
    public int mapDiv = 5;
    public float[] ScaleRange =  new float[2]{1.0f,2.0f};
    public float[] RotateRange =  new float[2]{0.0f,90.0f};
    public float distanceFromMap = 0.0f;
    public bool isCenter = false;
    //public float angle  =0.0f;
    float RandomRangeX = 0.0f;
    float RandomRangeZ = 0.0f;
    public float rotateX = 0f;

    //public int [,] numArray = new int[3,3];

    float randomScale = 1.0f;

    void Start()
    {
        //dic_isStacked.Add(mapObj,true);
        //StackedObjectSelectDict
        //dic_isStacked.Add(null,false);
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
                        creatObj = creatObjs_layer0 [Random.Range(0,creatObjs_layer0.Count)];
 
                        Instantiate(creatObj, new Vector3(hit.point[0],hit.point[1]+distanceFromMap,hit.point[2]),transform.rotation);

                        creatObj.transform.rotation = Quaternion.Euler(0,0,0);

                        float randomScale =  Random.Range(ScaleRange[0],ScaleRange[1]);
                        creatObj.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
                    }

                }

                else
                {
                        creatObj = creatObjs_layer0 [Random.Range(0,creatObjs_layer0.Count )];
 
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

        for (int layer = 0; layer < N_layers ;layer++) 
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

                        //print("======");
                        if (layer == 0)
                        {
                            
                             
                            creatObj = creatObjs_layer0 [Random.Range(0,creatObjs_layer0.Count )];
                            //creatObj.transform.localScale = new Vector3(1,1,1);
                            
                            //print(creatObj );
                            
                            
                            //creatObjs_layer =  creatObjs_layer0;

                            //layer_isStacked = layer0_isStacked;

                            
                        }
                        
                        if (layer > 0)
                        {
                            //print(hit.collider.name);
                            //creatObjs_layer =  creatObjs_layer1;
                            //layer_isStacked = layer1_isStacked;     
                            //print(string.Format("-----------------------{0} {1}",i,j) );
                            //print(hit.collider.gameObject);//射线射中的物体也是其下层物体，下层物体索引对应的是堆叠物体的选择类别索引，

                            
                            if (hit.collider.name!=mapObj.name )
                            {
                                //Transform StackObjectClass = StackedObjectSelectDict[hit.collider.gameObject].transform;//hit.collider.gameObject.transform.parent;
                                if(hit.collider.gameObject.GetComponent<CanStackObject>()!=null)
                                {
                                    List<GameObject> stackObjectList =   hit.collider.gameObject.GetComponent<CanStackObject>(). StackObjectList;
                                    if (stackObjectList.Count>0)
                                    {
                                        creatObj = stackObjectList[Random.Range(0,  stackObjectList.Count)];

                                    }
                                    else
                                    {
                                        creatObj =null;
                                    }                                    
                                    
                                    //can_selset_list = StackObjectsClass[creatObjs_layer0.IndexOf(creatObj)];
                                    
                                }

                                else
                                {
                                    creatObj =null;
                                }                               
                               
                            }

                            else

                            {
                                creatObj =null;
                            }
                            
                        }



                        //print(creatObj );
                        if (creatObj!=null)
                        {
                            //print(hit.point);
                            if(creatObj.GetComponent<CanStackObject>()!=null)
                            {
                                randomScale =  Random.Range(creatObj.GetComponent<CanStackObject>().itemScaleRange[0],creatObj.GetComponent<CanStackObject>().itemScaleRange[1]);
                             
                            }
                            else
                            {
                                randomScale=1.0f;
                            }
                            //float randomScale =  Random.Range(creatObj.GetComponent<CanStackObject>().itemScaleRange[0],creatObj.GetComponent<CanStackObject>().itemScaleRange[1]);
                            


                            float randomRotate  = Random.Range(RotateRange[0],RotateRange[1]);
                            GameObject newCreatObj;

                            

                            if (isCenter!=true)
                            {
                                //print(string.Format("{0} ",hit.point[1]));
                                //creatObj.transform.localScale = new Vector3(1,1,1);
                                creatObj.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
                                newCreatObj = Instantiate(creatObj, new Vector3(hit.point[0],hit.point[1]+distanceFromMap,hit.point[2]),Quaternion.Euler(rotateX,randomRotate,0));
                                //creatObj.transform.localScale = new Vector3(1,1,1);
                            }
                            else
                            {
                                //print(string.Format("{0}  {1}  {2}",randomScale,creatObj.transform.GetComponent<MeshFilter>().mesh.bounds.size.y,hit.point[1]));
                                //creatObj.transform.localScale = new Vector3(1,1,1);
                                creatObj.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
                                newCreatObj = Instantiate(creatObj, new Vector3(hit.point[0],hit.point[1]+randomScale*0.5f*creatObj.transform.GetComponent<MeshFilter>().mesh.bounds.size.y+distanceFromMap,hit.point[2]),Quaternion.Euler(rotateX,randomRotate,0));
                                //creatObj.transform.localScale = new Vector3(1,1,1);
                            }
                            //creatObj.transform.localScale = new Vector3(1,1,1);

                            //print(creatObjs_layer0.IndexOf(creatObj));
           
                            
                            
                            
                            //newCreatObj.transform.rotation = ;
                            //newCreatObj.transform.rotation = Quaternion.Euler(-90,0,0);


                            //newCreatObj.transform.parent = creatObjsClass.transform;

                            //print(creatObjs_layer);
                            //print(creatObj);
                            //print(creatObj.transform);
                            //if (layer!=2)
                            //{
                            //if (creatObjs_layer.IndexOf(creatObj)!=-1)
                            //{
                            //print(string.Format("{0}=={1}",creatObjs_layer.IndexOf(creatObj),layer_isStacked));
                            //dic_isStacked.Add(newCreatObj,layer_isStacked[creatObjs_layer.IndexOf(creatObj)]);
                            //}
                            //else
                            //{
                                //dic_isStacked.Add(newCreatObj,false);
                            //}

                            //}

                            
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

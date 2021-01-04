using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanStackObject : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<GameObject> StackObjectList = new List<GameObject>();
    public float[]  itemScaleRange =new float[]{1,1};

    void Start()
    {

    }
    void Update () 
    {
        //返回一条射线从摄像机通过一个屏幕点
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo = new RaycastHit();
        //（射线的起点和方向，hitonfo将包含碰到碰撞器的更多信息，射线的长度）有碰撞时，返回真
        //if (transform.parent!=null)
        //{
            //print(transform.parent);

        //if (Physics.Raycast(transform.parent.transform.position,transform.forward,out hit,200))
        //{
            //显示检测到的碰撞物体的世界坐标
            //print(hitInfo.point);
            //Debug.DrawLine(transform.parent.transform.position,transform.forward+ transform.parent.transform.position,Color.red);
        //}


        for (int i = 0; i < transform.childCount; i++)
        {
            if (i>0)
            {
                Transform child = transform.GetChild(i).gameObject.transform;
                //Debug.Log(child.name);
                

                if (Physics.Raycast(child.position,child.forward,out hitInfo,10))
                {
                    print(hitInfo.point);
                    Debug.DrawLine(child.position,hitInfo.point,Color.red);  //child.position+8* child.forward


                }

            }

        }

        //}

	}


        //private void OnDrawGizmos()
        //{
        
        
        //for (int i = 0; i < transform.childCount; i++)
        //{
            //Transform child = transform.GetChild(i).gameObject;
            //Debug.Log(child.name);
            //Gizmos.DrawLine(child.position,child.position+8* child.forward);
        //}

            

        //}
        

        
}


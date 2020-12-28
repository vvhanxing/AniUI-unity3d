using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour

{
    public GameObject FirePoint;
    public GameObject bulletPrefab;
    public string fireButton = "Fire";
    public string creatButton = "Creat";
    public string delButton = "Del";
    public string creatJointButton = "Joint";
    public string copyButton = "Copy";
    public float force =10f;



    public GameObject rayObj;
    public GameObject hitPointObj;
    public float hitPointObjZsize ;
    public GameObject[] creatObjs = new GameObject[]{};
    public Vector3 creatObjScale = new  Vector3(1,1,1); 

    public GameObject gameCheck;


    //private HingeJoint hinge;
    public string JointType ="HingeJoint";

    


    RaycastHit hit;
    private GameObject beHitedObj ;

    private Vector3[] chirdsLocalPos ;
    private Quaternion[] chirdsLocalRot ;


    // Start is called before the first frame update
    void Start()
    {
        hitPointObjZsize = hitPointObj.GetComponent<MeshFilter>().mesh.bounds.size[1]*hitPointObj.transform.localScale[1];
        
    }

    // Update is called once per frame
    void Update()
    {

		if( Input.GetMouseButtonDown(0))
		{
			//m_rigidbody.AddForce( 0f, 10f, 0f, ForceMode.Impulse );
			//Instantiate(ball);
			//ball.transform.position = fireObject.transform.position;
			//ball.GetComponent<Rigidbody>().AddForce( 0f, 10f, 0f, ForceMode.Impulse );fireObject
			//ball.GetComponent<Rigidbody>().velocity =  fireObject.transform.TransformDirection(Vector3.forward*100.0f); 
			//  Debug.Log ("leftmouse");
            //创建子弹
            GameObject obj = Instantiate(bulletPrefab,FirePoint.transform.position,FirePoint.transform.rotation);
            //销毁子弹
            //GameObject ball = obj as GameObject;
            obj.GetComponent<Rigidbody>().AddForce( transform.forward*force);
            Destroy (obj, 10);
		}

    }

}

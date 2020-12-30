using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp : MonoBehaviour

{
    public float hpValue = 10.0f;
    public float removeTime = 5.0f;
    //public string[] CanBeEatTag= new string[1];
    public List<string> CanBeEatTag = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hpValue<=0)
        {
            Destroy(gameObject,removeTime);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
       if (CanBeEatTag[0] !=null){
       if (CanBeEatTag.Contains (collision.gameObject.tag))
       {
           Destroy(gameObject,removeTime);
       }
       }
       
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp : MonoBehaviour

{
    public int hpValue = 100;
    public float removeTime = 5.0f;
    //public string[] CanBeEatTag= new string[1];
    public List<string> CanBeEatTag = new List<string>();

    public ENPCHealthBar healthBar ;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<ENPCHealthBar>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (healthBar !=null)
        {
            healthBar.Value= hpValue;

        }
        
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
               //Destroy(gameObject,removeTime);
               hpValue-=1;
           }
       }
       
    }



}

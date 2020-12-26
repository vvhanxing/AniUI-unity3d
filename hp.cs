using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp : MonoBehaviour

{
    public float hpValue = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hpValue<=0)
        {
            Destroy(gameObject,6.0f);
        }
        
    }
}

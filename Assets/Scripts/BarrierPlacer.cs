using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPlacer : MonoBehaviour
{
    public float minScale, highScale;

    // Start is called before the first frame update
    void Start()
    {
        int i=Random.Range(0, 2);
        if (i == 0) transform.localScale = new Vector3(transform.localScale.x, minScale, transform.localScale.z);
        else if (i == 1) transform.localScale = new Vector3(transform.localScale.x, highScale, transform.localScale.z);
        i =Random.Range(0, 2);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180*i, transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

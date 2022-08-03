using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopControl : MonoBehaviour
{
    [SerializeField] float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}

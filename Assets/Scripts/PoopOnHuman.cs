using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopOnHuman : MonoBehaviour
{
    [SerializeField] GameObject body;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - body.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = body.transform.position + offset;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopControl : MonoBehaviour
{
    [SerializeField] float speed;
    public Vector3 dir;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir.normalized * Time.deltaTime * speed);
    }
}

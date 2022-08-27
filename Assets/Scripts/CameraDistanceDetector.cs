using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistanceDetector : MonoBehaviour
{
    // Start is called before the first frame update
    Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 forward = cam.forward;
        Vector3 other = cam.position - transform.position;

        if (Vector3.Dot(other, forward) > 5f)
        {
            // print(Vector3.Dot(other,forward));
            Destroy(gameObject);
        }
    }
}

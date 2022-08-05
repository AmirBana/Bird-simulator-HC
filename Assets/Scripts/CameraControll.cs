using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    Transform player;
    Vector3 offset;
    public float smoothSpeed;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offset = transform.position - player.position;
    }
    void LateUpdate()
    {
        if(!GameManager.Instance.gamefinish) SmoothFollow();
    }
    public void SmoothFollow()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z + offset.z);
    }
}

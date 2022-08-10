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
        offset = transform.localPosition - player.localPosition;
    }
    void LateUpdate()
    {
        if(!GameManager.Instance.gamefinish) SmoothFollow();
    }
    public void SmoothFollow()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, player.localPosition.z + offset.z);
    }
}

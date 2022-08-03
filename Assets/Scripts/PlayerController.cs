using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xMin, xMax;
    public float speed;
    [SerializeField] GameObject poopObj;
    //public Transform m_TransToMove;
    [Space]
    public bool localMovement;
    public float sensivity;
    Touch curTouch;
    Vector3 newPos = Vector3.zero;
    void Start()
    {

    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed*Time.deltaTime);
        SwipeControl();
        TargetDetect();
    }
    void SwipeControl()
    {
        if (Input.touchCount > 0)
        {
            curTouch = Input.GetTouch(0);
            if (curTouch.phase == TouchPhase.Moved)
            {
                float newX = curTouch.deltaPosition.x * sensivity * Time.deltaTime;
                newPos = localMovement ? transform.localPosition : transform.position;
                newPos.x += newX;
                newPos.x = Mathf.Clamp(newPos.x, xMin, xMax);
                if (localMovement)
                {
                    transform.localPosition = newPos;
                }
                else
                {
                    transform.position = newPos;
                }
            }
        }
    }
    void TargetDetect()
    {
        float rayHeight = 10f;
        var ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down*rayHeight, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,rayHeight))
        {
            if(hit.collider.transform.CompareTag("Human"))
            {
                Pooping();
                hit.collider.gameObject.tag = "PoopHuman";
            }
        }
    }
    void Pooping()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject obj = Instantiate(poopObj, spawnPos, poopObj.transform.rotation);
    }
}

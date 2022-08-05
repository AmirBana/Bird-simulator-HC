using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public enum State
    {
        constant,
        horizontal,
        vertical
    };
    [SerializeField] float xMin, xMax, yMin, yMax;
    [SerializeField] float speed;
    public State state; 
    int dir;
    void Start()
    {
        int r = Random.Range(0, 2);
        if (r == 0) dir = -1;
        else if (r == 1) dir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.horizontal)
            MoveLeftRight();
    }
    void MoveLeftRight()
    {
        if (transform.position.x <= xMin)
        {

            dir = 1;
        }
        else if (transform.position.x >= xMax)
        {
            dir = -1;
        }
        transform.Translate(Vector3.right * dir * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSide : MonoBehaviour
{
    public Humans human;
    public GameObject mess;
    public Side side;
    // Start is called before the first frame update
    void Start()
    {
        mess = human.mess;
    }

    // Update is called once per frame
    void Update()
    {
        side = human.side;
    }
}

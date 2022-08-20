using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class GenerateLevel : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject human;
    public GameObject coin;
    PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float distance;
    public float coinDistance;
    public float starter;
    void Start()
    {
        pathCreator = GetComponent<PathCreator>();
        float points = pathCreator.path.length;
        int gameObjectIndex = 0;
        for (float i = starter; i < (points ); i += coinDistance)
        {
            gameObjectIndex = Random.Range(0, 2);
            Vector3 pos = pathCreator.path.GetPointAtDistance(i,endOfPathInstruction);
            Quaternion rot = pathCreator.path.GetRotationAtDistance(i,endOfPathInstruction);
            /* Vector3 pos = pathCreator.path.GetPoint(i);
             Quaternion rot = pathCreator.path.GetRotation(i);*/
            if ((i - starter) % distance != 0) Instantiate(coin, pos,rot);
            else
            {
                if (gameObjectIndex == 0) Instantiate(obstacle, pos, rot);
                else if (gameObjectIndex == 1) Instantiate(human, pos, rot);
            }


        }
    }
}

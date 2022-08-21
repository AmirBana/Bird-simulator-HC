using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class GenerateLevel : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject obstacle;
    public GameObject human;
    public GameObject coin;
    [Space]
    PathCreator pathCreator;
    public float[] allWidth;
    float width;
    [Header("point management")]
    public EndOfPathInstruction endOfPathInstruction;
    public float distance;
    public float coinDistance;
    public float starter;
    public float endPoint;
    GameObject createdObj;
    void Start()
    {
        pathCreator = GetComponent<PathCreator>();
        float points = pathCreator.path.length;
        
        int gameObjectIndex = 0;
        for (float i = starter; i < (points - endPoint); i += coinDistance)
        {
            width = allWidth[Random.Range(0, allWidth.Length)];
            gameObjectIndex = Random.Range(0, 2);
            Vector3 pos = pathCreator.path.GetPointAtDistance(i,endOfPathInstruction);
            Quaternion rot = pathCreator.path.GetRotationAtDistance(i,endOfPathInstruction);
            if ((i - starter) % distance != 0) createdObj = Instantiate(coin, pos,rot);
            else
            {
                if (gameObjectIndex == 0)
                {
                    createdObj = Instantiate(obstacle, pos, rot);
                }
                else if (gameObjectIndex == 1) createdObj = Instantiate(human, pos, rot);
            }
            if (createdObj.GetComponentsInChildren<Transform>()[0].name.Contains("Danger1"))
                SetDanger1();
            else
                SetWidth();
        }
    }
    void SetWidth()
    {
        print(createdObj.GetComponentsInChildren<Transform>()[1].name);
        Transform obj = createdObj.GetComponentsInChildren<Transform>()[1];
        Vector3 widthPos = new Vector3(width,obj.localPosition.y, obj.localPosition.z);
        obj.localPosition = widthPos;
    }
    void SetDanger1()
    {

    }
}

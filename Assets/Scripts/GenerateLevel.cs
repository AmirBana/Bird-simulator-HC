using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class GenerateLevel : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject[] obstacle;
    public GameObject[] human;
    public GameObject coin;
    [Space]
    PathCreator pathCreator;
    public float[] allWidth;
    float width;
    [Header("point management")]
    public EndOfPathInstruction endOfPathInstruction;
    public float distance;
    public float coinDistance;
    [Range(2,100)][Tooltip("humanCompareToObstacle")]public int humanCompareToObstacle=2;
    public int minChoiceHuman,maxChoiceHuman;
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
            gameObjectIndex = Random.Range(0, humanCompareToObstacle);
            Vector3 pos = pathCreator.path.GetPointAtDistance(i,endOfPathInstruction);
            Quaternion rot = pathCreator.path.GetRotationAtDistance(i,endOfPathInstruction);
            if ((i - starter) % distance != 0) createdObj = Instantiate(coin, pos,rot);
            else
            {
                if (gameObjectIndex == 0)
                {
                    int obsacleKind = Random.Range(0,obstacle.Length);
                    createdObj = Instantiate(obstacle[obsacleKind], pos, rot);
                }
                else if (gameObjectIndex != 0)
                {
                    int choices = Random.Range(minChoiceHuman, maxChoiceHuman);
                    for(int j = 0; j<choices;j++)
                    {
                        Vector3 pos2 = pathCreator.path.GetPointAtDistance(i+j+1, endOfPathInstruction);
                        Quaternion rot2 = pathCreator.path.GetRotationAtDistance(i+j+1, endOfPathInstruction);
                        createdObj = Instantiate(human[Random.Range(0, human.Length)], pos2, rot2);
                        SetWidth();
                    }
                    createdObj = Instantiate(human[Random.Range(0, human.Length)], pos, rot);

                }
            }
            if (!createdObj.GetComponentsInChildren<Transform>()[0].name.Contains("Danger1"))
                SetWidth();
        }
    }
    void SetWidth()
    {
        width = allWidth[Random.Range(0, allWidth.Length)];
        Transform obj = createdObj.GetComponentsInChildren<Transform>()[1];
        Vector3 widthPos = new Vector3(width,obj.localPosition.y, obj.localPosition.z);
        obj.localPosition = widthPos;
    }
}

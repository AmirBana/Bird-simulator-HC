using UnityEngine;
public class ObstacleBPlacer : MonoBehaviour
{
    public float yMin, yMax;
    void Start()
    {
        Vector3 pos = transform.position;
        int yPlace = Random.Range(1, 3);
        if (yPlace == 1) pos.y = yMin;
        else if (yPlace == 2) pos.y = yMax;
        transform.position = pos;
    }
}

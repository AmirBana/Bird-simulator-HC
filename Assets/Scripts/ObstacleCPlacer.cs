using UnityEngine;

public class ObstacleCPlacer : MonoBehaviour
{
    private float xMin, xMax;
    void Start()
    {
        xMin = -2.5f;
        xMax = 2.5f;
        Vector3 pos = transform.localPosition;
        int placeChooser = Random.Range(0, 3);
        if (placeChooser == 1)
            pos.x = xMin;
        else if (placeChooser == 2)
            pos.x = xMax;
        else if (placeChooser == 0)
            pos.x = 0;
        transform.localPosition = pos;
    }
}

using UnityEngine;

public class CoinScript : MonoBehaviour
{
    void Start()
    {
        int yPosMultiplier = Random.Range(1, 3);
        Vector3 pos = transform.localPosition;
        if(yPosMultiplier==2)
        pos.y = pos.y * yPosMultiplier -1;
        transform.localPosition = pos;
    }
}

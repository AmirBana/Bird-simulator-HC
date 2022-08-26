using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopOnHuman : MonoBehaviour
{
    [SerializeField] GameObject body;
    Vector3 posOffset;
    Vector3 rotOffset;
    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.gameStart)
                GameManager.Instance.OnScoreChange(1);
        }
    }
    void Start()
    {
        posOffset = transform.position - body.transform.position;
        rotOffset = transform.eulerAngles - body.transform.eulerAngles;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = body.transform.position + posOffset;
        transform.eulerAngles = body.transform.eulerAngles + rotOffset;
    }
   
}
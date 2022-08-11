using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    public enum State
    {
        constant,
        horizontal,
    };
    [SerializeField] float xMin, xMax, yMin, yMax;
    [SerializeField] float speed;
    public State state; 
    int dir;
    public int bonus;
    void Start()
    {
        GetComponentInChildren<TextMeshPro>().text = "+"+bonus.ToString();
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
        if (transform.localPosition.x <= xMin)
        {

            dir = 1;
        }
        else if (transform.localPosition.x >= xMax)
        {
            dir = -1;
        }
        transform.Translate(Vector3.right * dir * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.Ammo(bonus);
            Destroy(transform.parent.gameObject);
        }
    }
}

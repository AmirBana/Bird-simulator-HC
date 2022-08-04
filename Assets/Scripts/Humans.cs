using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humans : MonoBehaviour
{
    GameObject mess;
    GameObject player;
    [SerializeField] float xMin, xMax; 
    public float offset;
    bool isMessed;
    int dir;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        mess = GameObject.FindWithTag("Mess");
        player = GameObject.FindWithTag("Player");
        isMessed = false;
        offset = 2;
        int r=Random.Range(0, 2);
        if (r == 0) dir = -1;
        else if (r == 1) dir = 1;
        mess.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMessed)
        {
            MoveIdle();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       // print("hello");
       // print(other.gameObject.name);
        if (other.gameObject.CompareTag("Poopi"))
        {
            //print("hey");
            Destroy(other.gameObject,0.1f);
            mess.SetActive(true);
         //   mess.transform.position = new Vector3(other.transform.position.x, mess.transform.position.y, other.transform.position.z);
            isMessed=true;
        }
    }
    void MoveIdle()
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

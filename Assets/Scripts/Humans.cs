using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humans : MonoBehaviour
{
    public enum State
    {
        move,
        stationary
    }
    public GameObject mess;
    GameObject player;
    public State state;
    [SerializeField] float xMin, xMax; 
    public float offset;
    bool isMessed;
    int dir;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        isMessed = false;
        offset = 2;
        int r=Random.Range(0, 2);
        if(state == State.move)
        {
            if (r == 0)
            {
                transform.Rotate(Vector3.up * -90);
                dir = -1;
            }
            else if (r == 1)
            {
                transform.Rotate(Vector3.up * 90);
                dir = 1;
            }
        }
        if (state == State.move) animator.SetInteger("State", 2);
        else if (state == State.stationary) animator.SetInteger("State", 1);
        mess.SetActive(false);
    }
    void Update()
    {
        if(!isMessed && state == State.move)
        {
            MoveIdle();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Poopi"))
        {
            animator.SetTrigger("Mess");
            Destroy(other.gameObject,0.1f);
            mess.SetActive(true);
         //   mess.transform.position = new Vector3(other.transform.position.x, mess.transform.position.y, other.transform.position.z);
            isMessed=true;
            Stopped();
        }
    }
    void Stopped()
    {
        transform.Rotate(Vector3.up * -90 * dir);
        animator.SetTrigger("Mess");
    }
    void MoveIdle()
    {
        if (transform.localPosition.x <= xMin)
        {
            if (dir == -1)
                transform.Rotate(Vector3.up * 180);
            dir = 1;
        }
        else if (transform.localPosition.x >= xMax)
        {
            if (dir == 1)
                transform.Rotate(Vector3.up * 180);
            dir = -1;
        }
        transform.Translate(Vector3.right * dir * speed * Time.deltaTime,Space.World);
    }
}

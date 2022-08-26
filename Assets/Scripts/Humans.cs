using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Humans : MonoBehaviour
{
    public enum State
    {
        move,
        stationary,
    }
    public GameObject mess;
    Transform finishPos;
    Transform walkPos;
    GameObject player;
    public State state;
    public Side side;
    [SerializeField] float xMin, xMax; 
    public float offset;
    private float groundSideDivider;
    bool isMessed;
    int dir;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    Animator animator;
    private NavMeshAgent navMesh;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.allHumans += 1;
        int stateIndex = Random.Range(0, 2);
        if(stateIndex == 0) state = State.move;
        else if(stateIndex == 1) state = State.stationary;
        groundSideDivider = (5 / 3);
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        finishPos = GameObject.FindWithTag("Finish").transform;
        walkPos = GameObject.FindWithTag("Walk").transform;
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
        if(isMessed)
        {
            Invoke("NavMeshFollow", 1.5f);
        }
        if (transform.position.z >= finishPos.position.z-5 && GameManager.Instance.gamefinish )
        {
            float finishTime = Random.Range(0.5f, 2f);
            navMesh.speed = 0;
            mess.SetActive(false);
            Invoke("FinishPath", finishTime);
        }
        else if (isMessed && GameManager.Instance.gameOver)
        {
            float walkTime = Random.Range(0.5f, 2f);
            navMesh.speed = 0;
            mess.SetActive(false);
            Invoke("WalkBackDeathBird",walkTime);
        }
        SideDesider();
    }

    void WalkBackDeathBird()
    {

        navMesh.ResetPath();
        animator.SetTrigger("WalkBack");

    }
    void FinishPath()
    {
        int finihsKind = Random.Range(1, 3);
 
        navMesh.ResetPath();
        if (finihsKind == 1)
            animator.SetInteger("Finish", 1);
        else if (finihsKind == 2)
            animator.SetInteger("Finish", 2);
    }
    void NavMeshFollow()
    {
        //animator.SetTrigger("Finish");
        //if(GameManager.Instance.gamefinish == false)
         navMesh.destination = player.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Poopi"))
        {
            Destroy(other.gameObject,0.1f);
            mess.SetActive(true);
         //   mess.transform.position = new Vector3(other.transform.position.x, mess.transform.position.y, other.transform.position.z);
            isMessed=true;
            Stopped(other);
        }
    }
    private void SideDesider()
    {
        if (transform.localPosition.x < -groundSideDivider) side = Side.left;
        else if(transform.localPosition.x >= -groundSideDivider && transform.localPosition.x < groundSideDivider) side = Side.center;
        else if(transform.localPosition.x >= groundSideDivider) side = Side.right;
    }
    void Stopped(Collider other)
    {
        var rotPos = Quaternion.LookRotation(other.transform.position);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotPos.y, transform.eulerAngles.z);
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
        transform.Translate(Vector3.forward * speed * Time.deltaTime,Space.Self);
    }
    
}

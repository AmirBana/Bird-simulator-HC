using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float xMin, xMax, yMin, yMax;
    public float speed;
    [SerializeField] GameObject poopObj;
    Transform finishPos;
    [SerializeField] int poopSize;
    bool gameOvered;
    //public Transform m_TransToMove;
    [Space]
    public bool localMovement;
    public float sensivity;
    Touch curTouch;
    Vector3 newPos = Vector3.zero;
    Animator animator;
    [SerializeField] ParticleSystem particle;
    void Start()
    {
        gameOvered = false;
        animator = GetComponentsInChildren < Animator >()[0];
        finishPos = GameObject.FindWithTag("Finish").transform;
    }
    void Update()
    {
       if(GameManager.Instance.gameStart && !GameManager.Instance.gameOver)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            SwipeControl();
        }
        TargetDetect();
        if(transform.position.z >= finishPos.position.z)
        {
            GameManager.Instance.gamefinish = true;
            speed = 50;
            if (!gameOvered) Invoke("WinBird", 2f);
        }
        if(GameManager.Instance.gameOver && !gameOvered)
        {
            DeathBird();
        }    
    }
    void DeathBird()
    {
        particle.Play();
        gameOvered = true;
        Invoke("GameOverActive", 3f);
    }
    void WinBird()
    {
        gameOvered = true;
        GameManager.Instance.GameWin();
        speed = 50f;
    }
    void GameOverActive()
    {
        GameManager.Instance.GameOver();
    }
    void SwipeControl()
    {
        if (Input.touchCount > 0)
        {
            curTouch = Input.GetTouch(0);
            if (curTouch.phase == TouchPhase.Moved)
            {
                float newX = curTouch.deltaPosition.x * sensivity * Time.deltaTime;
                float newY = curTouch.deltaPosition.y * sensivity * Time.deltaTime;
                newPos = localMovement ? transform.localPosition : transform.position;
                newPos.x += newX;
                newPos.y += newY;
                newPos.x = Mathf.Clamp(newPos.x, xMin, xMax);
                newPos.y = Mathf.Clamp(newPos.y, yMin, yMax);
                if (localMovement)
                {
                    transform.localPosition = newPos;
                }
                else
                {
                    transform.position = newPos;
                }
                if(newX > 0.02)
                {
                    //animator.SetBool("turn left", false);
                    //animator.SetBool("turn right", true);
                }
                else if(newX <-0.02)
                {
                   // animator.SetBool("turn left", true);
                   // animator.SetBool("turn right", false);
                }
            }
            else if( curTouch.phase == TouchPhase.Stationary || curTouch.phase == TouchPhase.Ended)
            {
               // animator.SetBool("turn right", false);
               // animator.SetBool("turn left", false);
            }
        }
    }
    void TargetDetect()
    {
        float rayHeight = 10f;
        var ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down*rayHeight, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,rayHeight))
        {
            if (hit.collider.tag=="Human" && GameManager.Instance.ammo >= poopSize)
            {
                var target1 = hit.collider.transform;
                hit.collider.gameObject.tag = "PoopHuman";
                GameManager.Instance.Ammo(-poopSize);
                Pooping();
                print("hit:"+hit.collider.name);
            }
        }
    }
    void Pooping()
    {
        animator.SetTrigger("poop");
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject obj = Instantiate(poopObj, spawnPos, poopObj.transform.rotation);
        //animator.SetBool("turn left", false);
    }
   
}

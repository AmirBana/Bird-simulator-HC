using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float xMin, xMax, yMin, yMax;
    private bool onLand;
    public float speed;
    public float normalSpeed;
    public float damagedSpeed;
    [SerializeField] GameObject poopObj;
    Transform finishPos;
    [SerializeField] int poopSize;
    bool gameOvered;
    bool isMoved;
    public PathCreation.Examples.PathFollower pathFollow;
    //public Transform m_TransToMove;
    [Space]
    public bool localMovement;
    public float sensivity;
    public float minSwipe;
    Touch curTouch;
    Vector3 newPos = Vector3.zero;
    Animator animator;
    [SerializeField] ParticleSystem particle,particle2;
    void Start()
    {
        gameOvered = false;
        onLand = true;
        pathFollow.speed = normalSpeed;
        animator = GetComponentsInChildren < Animator >()[0];
        finishPos = GameObject.FindWithTag("Finish").transform;
        StartCoroutine(StartFly());
    }
    void Update()
    {
        if(!onLand)
        {
            if (GameManager.Instance.gameStart && !GameManager.Instance.gameOver)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                //SwipeControl();
                Swipe2();
            }
            TargetDetect();
            if (transform.position.z >= finishPos.position.z)
            {
                GameManager.Instance.gamefinish = true;
                speed = 50;
                if (!gameOvered) Invoke("WinBird", 2f);
            }
            if (GameManager.Instance.gameOver && !gameOvered)
            {
                DeathBird();
            }
        }
    }
    IEnumerator StartFly()
    {
        while (onLand == true)
        {
           if(GameManager.Instance.gameStart)
            {
                LeanTween.moveLocalY(gameObject, 3f, 1.5f).setEaseInOutQuad();
                yield return new WaitForSeconds(0.5f);
                animator.SetTrigger("StartFly");
                yield return new WaitForSeconds(1f);
                onLand = false;
            }
           else
                yield return null;
        }
    }
    public void TakeDamage()
    {
        animator.SetTrigger("Dmg");
        particle2.Play();
        StartCoroutine(DamageEffect());
    }
    IEnumerator DamageEffect()
    {
        pathFollow.speed = damagedSpeed;
        yield return new WaitForSeconds(2f);
        pathFollow.speed = normalSpeed;
        Func<int> a = () => 2;
    }
    void DeathBird()
    {
        particle.Play();
        gameOvered = true;
        Invoke("GameOverActive", 1f);
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
    void Swipe2()
    {
        if(Input.touchCount > 0)
        {
            curTouch = Input.GetTouch(0); 
          if(Mathf.Abs(curTouch.deltaPosition.x) > minSwipe || Mathf.Abs(curTouch.deltaPosition.y)>minSwipe)
            {
                if (curTouch.phase == TouchPhase.Moved && !isMoved)
                {
                    float newX = curTouch.deltaPosition.x > 0 ? 2.5f : -2.5f;
                    float newY = curTouch.deltaPosition.y > 0 ? 3f : -3f;
                    newPos = localMovement ? transform.localPosition : transform.position;
                    if (Mathf.Abs(curTouch.deltaPosition.x) > Mathf.Abs(curTouch.deltaPosition.y))
                    {
                        newPos.x += newX;
                        newPos.x = Mathf.Clamp(newPos.x, xMin, xMax);
                    }
                    else
                    {
                        newPos.y += newY;
                        newPos.y = Mathf.Clamp(newPos.y, yMin, yMax);
                    }
                    if (localMovement)
                    {
                      LeanTween.moveLocal( gameObject,newPos,0.1f);
                        if (newX > 0) animator.SetTrigger("TurnRight");
                        else if (newX < 0) animator.SetTrigger("TurnLeft");
                    }
                    else
                    {
                        LeanTween.move(gameObject, newPos, 0.1f);
                    }
                    isMoved = true;
                }
            }
            if (curTouch.phase == TouchPhase.Ended) isMoved = false;
        }
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
                //print("hit:"+hit.collider.name);
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

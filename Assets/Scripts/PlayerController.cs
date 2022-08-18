using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Side side;
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
                
                //SwipeControl();
                Swipe2();
            }
            if(GameManager.Instance.gamefinish)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
            SideDecider();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.Coin(1);
            Destroy(other.gameObject);
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
        PositionChangeOnDmg();
        particle2.Play();
        StartCoroutine(DamageEffect());
    }
    void PositionChangeOnDmg()
    {
        float xPos = localMovement ?  transform.localPosition.x : transform.position.x;
        Vector3 newPos = localMovement ? transform.localPosition : transform.position;
        if (xPos >= 2f)
            newPos.x = 0;
        else if (xPos <= -2f)
            newPos.x = 0;
        else
        {
            int rand = UnityEngine.Random.Range(0, 2);
            if (rand == 0) newPos.x = -2.5f;
            else if (rand == 1) newPos.x = 2.5f;
        }
        if (localMovement)
            LeanTween.moveLocal(gameObject, newPos, 0.1f);
        else
            LeanTween.move(gameObject, newPos, 0.1f);
    }
    IEnumerator DamageEffect()
    {
        pathFollow.speed = damagedSpeed;
        yield return new WaitForSeconds(2f);
        pathFollow.speed = normalSpeed;
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
    void SideDecider()
    {
        if (transform.localPosition.x <= -2) side = Side.left;
        else if(transform.localPosition.x < 2) side = Side.center;
        else if(transform.localPosition.x >= 2) side = Side.right;
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
            if (hit.collider.tag=="Human")
            {
                var target1 = hit.collider.transform;
                hit.collider.gameObject.tag = "PoopHuman";
                Side hitSide = hit.collider.gameObject.GetComponent<HumanSide>().side;
                GameObject aim = hit.collider.gameObject.GetComponent<HumanSide>().mess;
                print(target1.name);
                if (hitSide == side) Pooping(true, aim);
                else Pooping(false,aim);
                //print("hit:"+hit.collider.name);
            }
        }
    }
    public void Pooping(bool hit,GameObject target)
    {
        animator.SetTrigger("poop");
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject obj = Instantiate(poopObj, spawnPos, poopObj.transform.rotation);
        PoopControl objScript = obj.GetComponent<PoopControl>();
        objScript.hitted = hit;
        objScript.target = target;

        //animator.SetBool("turn left", false);
    }
   
}

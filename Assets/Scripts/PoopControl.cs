using UnityEngine;

public class PoopControl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject effect;
    public bool hitted;
    public GameObject target;
    void Start()
    {
        if(transform.position.y > 4)
        { 
            speed *= 2;
        }
        else
        {
        
        }
    }

    void Update()
    {
        Move();
        if (transform.position.y < -2f) Destroy(gameObject);
    }
    void Move()
    {
        if(hitted)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Vector3 pos = gameObject.transform.position;
            pos.z -= 1f;
            pos.x -= 1f;
            pos.y = -3f;
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        }
    }
}

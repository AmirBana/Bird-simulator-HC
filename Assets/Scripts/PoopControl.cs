using UnityEngine;

public class PoopControl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject effect;
    public bool hitted;
    public GameObject target;
    float time;
    void Start()
    {
        if(transform.position.y > 4)
        {
            time = 0.35f;
        }
        else
        {
            time = 0.15f;
            speed *= 2;
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

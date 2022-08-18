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
            LeanTween.move(gameObject, target.transform.position, time).setEaseLinear();
        }
        else
        {
            Vector3 pos = gameObject.transform.position;
            pos.z -= 1f;
            pos.x -= 1f;
            pos.y = -3f;
            LeanTween.move(gameObject, pos, time * 3);
        }
    }
}

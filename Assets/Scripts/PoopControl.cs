using UnityEngine;

public class PoopControl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject effect;
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}

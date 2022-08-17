using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    bool isTouched;
    // Start is called before the first frame update
    void Start()
    {
        isTouched = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTouched)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isTouched = true;
                other.gameObject.GetComponent<PlayerController>().TakeDamage();
                GameManager.Instance.health -= 1;
                print("Health: " + GameManager.Instance.health);
                if (GameManager.Instance.health == 0) GameManager.Instance.gameOver = true;
            }
        }
        print("health");
    }
}

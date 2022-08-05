using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print("obstacle collier:"+other.name);
        if(other.gameObject.CompareTag("Player"))
        {
            print("Game Over");
            GameManager.Instance.gameOver = true;
        }
    }
}

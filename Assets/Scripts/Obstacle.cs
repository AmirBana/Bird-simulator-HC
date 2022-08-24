using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    bool isTouched;
    public Material transMat;

    public float fadeDistance;
    // Start is called before the first frame update
    void Start()
    {
        isTouched = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraFind();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTouched)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(ResetTrigger(other.gameObject));
                isTouched = true;
                other.gameObject.GetComponent<PlayerController>().TakeDamage();
                GameManager.Instance.health -= 1;
                GameManager.Instance.Heart();
                print("Health: " + GameManager.Instance.health);//todo remove
                if (GameManager.Instance.health == 0) GameManager.Instance.gameOver = true;
            }
        }
    }
    IEnumerator ResetTrigger(GameObject player)
    {
        player.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SphereCollider>().enabled = true;
    }
    void CameraFind()
    {
        var camera = Camera.main.transform ;
        float distance = Mathf.Abs(camera.position.z - transform.parent.position.z);
        if (distance < fadeDistance)
            GetComponent<MeshRenderer>().material = transMat;
    }
}

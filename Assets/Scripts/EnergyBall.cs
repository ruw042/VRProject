using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public GameObject explosionPrefab;
    float timeLeft = 5.0f;

    void Update()
    {
        this.GetComponent<Rigidbody>().AddForce((new Vector3(0, 1, 0))*1.0f);
        if (gameObject.transform.position.y < 0)
        {
            Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "shootable")
        {
            Instantiate(explosionPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

    }

    
    
}

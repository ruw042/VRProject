using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public AudioClip clip;
    public GameObject explosion;

    private Vector3 ax;
    private float rotat;
    private float angle;
	// Use this for initialization
	void Start () {
        ax = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rotat = Random.Range(5f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
        angle = angle + Time.deltaTime * rotat;
        transform.rotation = Quaternion.AngleAxis(angle, ax);
        //transform.Rotate(ax, Time.deltaTime*rotat);
    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 pos = transform.position;
        GameObject exp = Instantiate(explosion, pos, Quaternion.identity);
        Destroy(exp, exp.GetComponent<ParticleSystem>().duration);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(clip, pos,100);
    }
}

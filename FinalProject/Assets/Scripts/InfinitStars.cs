using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitStars : MonoBehaviour {

    public int starsMax = 100;
    public float starSize = 1.0f;
    public float starDistance = 10;
    public float starClipDistance = 1;

    private Transform tx;
    private ParticleSystem.Particle[] points;

	// Use this for initialization
	void Start () {
        tx = transform;
	}
	
    void CreateStars()
    {
        points = new ParticleSystem.Particle[starsMax];
        for(int i = 0;i<starsMax;i++)
        {
            points[i].position = Random.insideUnitSphere * starDistance + tx.position;
            points[i].color = new Color(1,1,1,1);
            points[i].size = starSize;
        }
    }

	// Update is called once per frame
	void Update () {
        if (points == null) CreateStars();

        for(int i = 0; i < starsMax; i++)
        {
            float distance = Vector3.Distance(points[i].position, tx.position);
            if (distance > starDistance)
            {
                points[i].position = Random.insideUnitSphere * starDistance + tx.position;
            }

            if(distance <= starClipDistance)
            {
                float percent = distance / starClipDistance;
                points[i].color = new Color(1, 1, 1, percent);
                points[i].size = percent * starSize;
            }
        }


        GetComponent<ParticleSystem>().SetParticles(points,points.Length);
	}
}

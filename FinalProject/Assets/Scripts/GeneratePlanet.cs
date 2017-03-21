using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlanet : MonoBehaviour {

    public GameObject[] planets;
    public GameObject radarPlanet;

    private float countDown;
    private List<GameObject> around;

	// Use this for initialization
	void Start () {
        around = new List<GameObject>();
        //countDown = Random.Range(3f, 5f);
        for(int i = 0; i < 10; i++)
        {
            Generate();
        }
	}
	
	// Update is called once per frame
	void Update () {
        /*
        countDown -= Time.deltaTime;
        if(countDown <= 0)
        {
            if(around.Count >= 10)
                Generate();
            countDown = Random.Range(3f, 5f);
        }*/
	}
    public void Regen()
    {
        for(int i = 0; i < around.Count; i++)
        {
            Destroy(around[i]);
        }
        for (int i = 0; i < 10; i++)
        {
            Generate();
        }
    }
    void Generate()
    {
        Vector3 position;
        position = Random.insideUnitSphere * 300 + transform.position;
        while(Vector3.Distance(position,transform.position) <= 150)
        {
            position = Random.insideUnitSphere * 300 + transform.position;
        }
        int index = Random.Range(0, 6);
        GameObject tmpPlanet = Instantiate(planets[index], position, Quaternion.identity);
        float scale = Random.Range(1f, 5f);
        tmpPlanet.transform.localScale = new Vector3(scale, scale, scale);
        GameObject tmpRadarPlanet = Instantiate(radarPlanet, position, Quaternion.identity);
        tmpRadarPlanet.transform.parent = tmpPlanet.transform;

        around.Add(tmpPlanet);
    }

    bool achieveMax()
    {
        bool result = false;
        int count = 0;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 300);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].transform.gameObject.layer == 10)
            {
                count++;
            }
            if(count >= 7)
            {
                result = true;
                break;
            }
        }
        if(count >= 5)
        {
            result = true;
        }
        return result;
    }
}

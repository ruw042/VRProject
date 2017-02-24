using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerRotate : MonoBehaviour {

    public GameObject parser;
    public GameObject plane;
    private List<GameObject> points;
    // Use this for initialization
    void Start () {
        points = parser.GetComponent<Parser>().checkPoints;
    }
	
	// Update is called once per frame
	void Update () {
        if (parser.GetComponent<Parser>().nextPoint >= points.Count)
        {
            transform.forward = plane.transform.forward;
        }
        else
        {
            transform.forward =  points[parser.GetComponent<Parser>().nextPoint].transform.position - plane.transform.position;
           

        }
    }
}

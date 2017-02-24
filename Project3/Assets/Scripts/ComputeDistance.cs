using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeDistance : MonoBehaviour {

    public GameObject plane;
    public GameObject parser;

    private List<GameObject> points;
    // Use this for initialization
    void Start () {
        points = parser.GetComponent<Parser>().checkPoints;
    }
	
	// Update is called once per frame
	void Update () {
        if (parser.GetComponent<Parser>().nextPoint >= points.Count)
        {
            GetComponent<TextMesh>().text = "Finished";
        }
        else
        {
            float distance = Vector3.Distance(plane.transform.position, points[parser.GetComponent<Parser>().nextPoint].transform.position);
            GetComponent<TextMesh>().text = "Distance: " + distance.ToString(".0#") + " m";

        }
        
    }
}

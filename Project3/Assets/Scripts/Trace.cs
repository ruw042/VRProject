using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour {

    public LineRenderer line;
    public GameObject parser;
    public GameObject plane;

    private List<GameObject> points;
	// Use this for initialization
	void Start () {
        points = parser.GetComponent<Parser>().checkPoints;
	}
	
	// Update is called once per frame
	void Update () {
		if(parser.GetComponent<Parser>().nextPoint >= points.Count)
        {
            line.enabled = false;
        }else
        {
            line.enabled = true;
            line.numPositions = 2;
            line.SetPosition(0, plane.transform.position);
            line.SetPosition(1, points[parser.GetComponent<Parser>().nextPoint].transform.position);

        }
	}
}

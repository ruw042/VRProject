using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour {

    public string filename;
    public GameObject campus;
    public GameObject gameControl;
    public GameObject checkPointSphere;
    private List<Vector3> points;
    public List<GameObject> checkPoints;
    public List<Vector3> positions;

    public int nextPoint =1;
	// Use this for initialization
	void Start () {
    
        points = new List<Vector3>();
        Parse();
        foreach(Vector3 v in points)
        {
            GameObject sphere = Instantiate(checkPointSphere);
            sphere.transform.position = campus.transform.TransformPoint(v);
            positions.Add(sphere.transform.position);
            checkPoints.Add(sphere);
            

        }
        Destroy(checkPoints[0]);     
        gameControl.GetComponent<GameControl>().ResetPosition();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Parse()
    {
        string line;

        // Read the file and display it line by line.
        System.IO.StreamReader file = new System.IO.StreamReader(filename);
        while ((line = file.ReadLine()) != null)
        {
            var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            points.Add(new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2])));
        }

        file.Close();
    }
}

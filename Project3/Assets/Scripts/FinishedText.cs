using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedText : MonoBehaviour {

    float time = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            if (time <= 0)
            {
                GetComponent<TextMesh>().text = "";
            }
        
	}

    public void getCheckPoint()
    {
        GetComponent<TextMesh>().text = "Get CheckPoint";
        time = 3f;
    }

}

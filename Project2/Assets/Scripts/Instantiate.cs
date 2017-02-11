using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour {

    private bool state = false;
    public GameObject panel;
    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 90;
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            state = !state;
            if (state == true)
                panel.SetActive(true);
            else
                panel.SetActive(false);
        }
         
    }
}

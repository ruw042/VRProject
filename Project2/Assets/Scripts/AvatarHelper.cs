using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarHelper : MonoBehaviour {

    public GameObject cameraRig;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = cameraRig.transform.position;
        transform.rotation = cameraRig.transform.rotation; 
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject plane;
    public int controller;

    private float velocity = 0;
    private float eulerX = 0;
    private float eulerY = 0;
    private float eulerZ = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(controller == 0)
        {
            float xPlace = 0;
            float zPlace = 0;
            if(transform.localEulerAngles.z > 180)
            {
                xPlace = transform.localEulerAngles.z - 360;
            }else
            {
                xPlace = transform.localEulerAngles.z;
            }
            if (transform.localEulerAngles.x > 180)
            {
                zPlace = transform.localEulerAngles.x - 360;
            }
            else
            {
                zPlace = transform.localEulerAngles.x;
            }
            eulerX += Time.deltaTime * zPlace;
            eulerZ += Time.deltaTime * xPlace;
            //plane.transform.rotation = Quaternion.Euler(eulerZ, eulerX, plane.transform.eulerAngles.z);
            plane.transform.rotation *= Quaternion.Euler(Time.deltaTime * zPlace, 0, Time.deltaTime * xPlace);
            //plane.transform.Rotate(Time.deltaTime*zPlace, 0, Time.deltaTime*xPlace);          
        }
        else
        {
            float acc = 0;
            float turn = 0;
            if (transform.localEulerAngles.x > 180)
            {
                acc = transform.localEulerAngles.x - 360;
            }
            else
            {
                acc = transform.localEulerAngles.x;
            }
            if (transform.localEulerAngles.z > 180)
            {
                turn = transform.localEulerAngles.z - 360;
            }
            else
            {
                turn = transform.localEulerAngles.z;
            }
            velocity += Time.deltaTime * acc;
            if(velocity < 0)
            {
                velocity = 0;
            }if(velocity > 50)
            {
                velocity = 50;
            }
            plane.GetComponent<Rigidbody>().velocity = plane.transform.forward*velocity;
            plane.GetComponent<AudioSource>().volume = velocity / 50f;

            //eulerY += -Time.deltaTime * turn;
            plane.transform.rotation *= Quaternion.Euler(0, -Time.deltaTime * turn, 0);
            //Debug.Log(plane.transform.eulerAngles);
            //plane.transform.rotation = Quaternion.Euler(plane.transform.eulerAngles.x, eulerY, plane.transform.eulerAngles.z);
        }
	}
}

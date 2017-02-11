using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoardStick : MonoBehaviour {

    private int touch = 0;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name) == "RoomLeft")
        {
            touch = 1;
        }
        if ((other.gameObject.name) == "FrontOne")
        {
            touch = 2;
        }
    }
    void OnTriggerExit(Collider other)
    {
        touch = 0;
    }*/

        void OnTriggerEnter(Collider collision)
    {
        if((collision.gameObject.name) == "RoomLeft")
        {
            touch = 1;
        }
        if ((collision.gameObject.name) == "FrontOne")
        {
            touch = 2;
        }
        if ((collision.gameObject.name) == "FrontTwo")
        {
            touch = 3;
        }
        if ((collision.gameObject.name) == "Right")
        {
            touch = 4;
        }
        if ((collision.gameObject.name) == "BackOne")
        {
            touch = 5;
        }
        if ((collision.gameObject.name) == "BackTwo")
        {
            touch = 6;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        
        if ((collision.gameObject.name) == "RoomLeft")
        {
            touch = 0;
        }
        if ((collision.gameObject.name) == "FrontOne")
        {
            //Debug.Log("123");
            touch = 0;
        }
        if ((collision.gameObject.name) == "FrontTwo")
        {
            touch = 0;
        }
        if ((collision.gameObject.name) == "Right")
        {
            touch = 0;
        }
        if ((collision.gameObject.name) == "BackOne")
        {
            touch = 0;
        }
        if ((collision.gameObject.name) == "BackTwo")
        {
            touch = 0;
        }
     
    }

    void OnTriggerStay(Collider collision)
    {
        if ((collision.gameObject.name) == "RoomLeft")
        {
            touch = 1;
        }
        if ((collision.gameObject.name) == "FrontOne")
        {
            touch = 2;
        }
        if ((collision.gameObject.name) == "FrontTwo")
        {
            touch = 3;
        }
        if ((collision.gameObject.name) == "Right")
        {
            touch = 4;
        }
        if ((collision.gameObject.name) == "BackOne")
        {
            touch = 5;
        }
        if ((collision.gameObject.name) == "BackTwo")
        {
            touch = 6;
        }
    }

    public void changeRotation()
    {
        switch(touch){
            case 1:
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(-90, 0, 90);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case 2:
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(-90, 90, 90);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case 3:
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(-90, 98.4f, 90);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case 4:
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(-90, 171.5f, 90);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case 5:
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(-90, -90, 90);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case 6:
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(-90, -98.5f, 90);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                break;
            default:
                break;
        }
    }
}

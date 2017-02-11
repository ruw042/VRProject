using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public OVRInput.Controller controller;
    public LayerMask grabMask;
    public float grabRadius = 0.1f;

    public GameObject tv;
    public GameObject desk;
    public GameObject chair;
    public GameObject locker;
    public GameObject cabinet;
    public GameObject whiteboard;

    private GameObject grabbedObject;
    private bool grabbing = false;

    private Quaternion lastRotation, currentRotation;

	
	void GrabObject () {
        grabbing = true;

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if(hits.Length > 0)
        {
            int closestHit = 0;
            for(int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance)
                    closestHit = i;
            }
            
            grabbedObject = hits[closestHit].transform.gameObject;
            if(grabbedObject.tag == "PanelObject")
            {
                switch (grabbedObject.name)
                {
                    case "PanelTV":
                        grabbedObject = Instantiate(tv, transform.position, Quaternion.identity);
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.rotation = transform.rotation;
                        grabbedObject.transform.Rotate(- 90, 0, 0);
                        grabbedObject.transform.parent = transform;
                        DisableCollider(grabbedObject);                  
                        break;
                    case "PanelDesk":
                        grabbedObject = Instantiate(desk, transform.position, Quaternion.identity);
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.rotation = transform.rotation;
                        grabbedObject.transform.Rotate(-90, 0, 90);
                        grabbedObject.transform.Translate(0, 0, -0.25f);
                        grabbedObject.transform.parent = transform;
                        DisableCollider(grabbedObject);
                        break;
                    case "PanelChair":
                        grabbedObject = Instantiate(chair, transform.position, Quaternion.identity);
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.rotation = transform.rotation;
                        grabbedObject.transform.Rotate(-90, 0, 180);
                        grabbedObject.transform.parent = transform;
                        DisableCollider(grabbedObject);
                        break;
                    case "PanelLocker":
                        grabbedObject = Instantiate(locker, transform.position, Quaternion.identity);
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.rotation = transform.rotation;
                        grabbedObject.transform.Rotate(-90, 90, 180);
                        grabbedObject.transform.parent = transform;
                        DisableCollider(grabbedObject);
                        break;
                    case "PanelCabinet":
                        grabbedObject = Instantiate(cabinet, transform.position, Quaternion.identity);
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.rotation = transform.rotation;
                        grabbedObject.transform.Rotate(-90, 0, 0);
                        grabbedObject.transform.parent = transform;
                        DisableCollider(grabbedObject);
                        break;
                    case "PanelWhiteboard":
                        grabbedObject = Instantiate(whiteboard, transform.position, Quaternion.identity);
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.rotation = transform.rotation;
                        grabbedObject.transform.Rotate(-90, 0, 180);
                        grabbedObject.transform.parent = transform;
                        grabbedObject.GetComponent<BoxCollider>().isTrigger = true;
                        break;
                }

            }else
            {
                switch (grabbedObject.tag)
                {
                    case "ActualWhiteboard":
                        //whiteboardMode = 1;
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.position = transform.position;
                        grabbedObject.transform.parent = transform;
                        grabbedObject.GetComponent<BoxCollider>().isTrigger = true;
                        //Debug.Log(tmpObj.GetComponent<BoxCollider>().isTrigger);
                        break;
                    default:
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.position = transform.position;
                        grabbedObject.transform.parent = transform;
                        DisableCollider(grabbedObject);
                        break;
                }
            }
            
        }
	}

    void DropObject ()
    {
        grabbing = false;

        if(grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();
            if (grabbedObject.tag == "ActualWhiteboard")
            {
                grabbedObject.GetComponent<WhiteBoardStick>().changeRotation();
                grabbedObject.GetComponent<BoxCollider>().isTrigger = false;
                //Debug.Log(grabbedObject.GetComponent<BoxCollider>().isTrigger);
                //Debug.Log(grabbedObject.GetComponent<Rigidbody>().isKinematic);
            }
            else
            {
                EnableCollider(grabbedObject);
            }
            grabbedObject = null;
        }
    }
	
    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }


	// Update is called once per frame
	void Update () {
        
        if(grabbedObject != null)
        {
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;
        }

        if ((OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) >0.8f ) && !grabbing)
        {
            //Debug.Log("pressed");
            GrabObject();
        }
        if((OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) < 0.8) && grabbing)
        {
            //Debug.Log("not pressed");
            DropObject();
        }
   
	}

    void DisableCollider(GameObject g)
    {
        foreach (Collider c in g.GetComponents<Collider>())
        {
            c.enabled = false;
        }
    }

    void EnableCollider(GameObject g)
    {
        foreach (Collider c in g.GetComponents<Collider>())
        {
            c.enabled = true;
        }
    }
}

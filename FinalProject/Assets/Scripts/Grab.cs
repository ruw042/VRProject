using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public OVRInput.Controller controller;
    public LayerMask grabMask;
    public float grabRadius = 0.1f;

    private GameObject grabbedObject;
    private bool grabbing = false;

    private Vector3 prePos;


    void GrabObject()
    {
        //Debug.Log("123");
        grabbing = true;

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if (hits.Length > 0)
        {
            
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance)
                    closestHit = i;
            }

            grabbedObject = hits[closestHit].transform.gameObject;
            //grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

            prePos = transform.localPosition;

        }
    }

    void DropObject()
    {
        grabbing = false;

        if (grabbedObject != null)
        {
            grabbedObject.transform.localRotation = Quaternion.identity;
            //grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject = null;
        }
    }



    // Update is called once per frame
    void Update()
    {

        if (grabbedObject != null)
        {
            Vector3 movement = transform.localPosition - prePos;
            if(grabbedObject.tag == "leftController")
                grabbedObject.transform.localRotation = Quaternion.Euler(100* movement.z, 0, -100* movement.x);
            else
                grabbedObject.transform.localRotation = Quaternion.Euler(100 * movement.z, 0, -100 * movement.x);
        }

        if ((OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.8f) && !grabbing)
        {
            //Debug.Log("pressed");
            GrabObject();
        }
        if ((OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) < 0.8) && grabbing)
        {
            //Debug.Log("not pressed");
            DropObject();
        }

    }

    
}

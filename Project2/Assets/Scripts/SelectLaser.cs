using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLaser : MonoBehaviour {

    public LineRenderer line;
    public OVRInput.Controller controller;

    private List<GameObject> grabbedObject;
    private List<List<List<Material>>> materials;
    private bool grabbed = false;
    private bool keepGrabbing = false;
    private int whiteboardMode = 0; //0 for anything, 1 for whiteboard only, 2 for others only

    private List<Quaternion> lastRotation, currentRotation;

    // Use this for initialization
    void Start()
    {
        line.enabled = false;
    }

    
    void GrabObject()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject tmpObj = hit.transform.gameObject;
            if(tmpObj.layer == 8 && tmpObj.tag != "PanelObject")
            {
                grabbed = true;

                grabbedObject = new List<GameObject>();
                lastRotation = new List<Quaternion>();
                currentRotation = new List<Quaternion>();
                materials = new List<List<List<Material>>>();
                grabbedObject.Add(tmpObj);
                lastRotation.Add(tmpObj.transform.rotation);
                currentRotation.Add(tmpObj.transform.rotation);
                List<List<Material>> mats = new List<List<Material>>();
                materials.Add(mats);
                Renderer[] rs = tmpObj.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rs)
                {
                    Material[] ms = r.materials;
                    List<Material> copyMs = new List<Material>();
                    mats.Add(copyMs);
                    foreach (Material m in ms)
                    {
                        Material copyM = new Material(m);
                        copyMs.Add(copyM);
                        m.color = Color.green;
                    }
                    r.materials = ms;

                }              
                switch (tmpObj.tag)
                {
                    case "ActualWhiteboard":
                        whiteboardMode = 1;
                        tmpObj.GetComponent<Rigidbody>().isKinematic = true;
                        tmpObj.transform.parent = transform;
                        tmpObj.GetComponent<BoxCollider>().isTrigger = true;
                        //Debug.Log(tmpObj.GetComponent<BoxCollider>().isTrigger);
                        break;
                    default:
                        whiteboardMode = 2;
                        tmpObj.GetComponent<Rigidbody>().isKinematic = true;
                        tmpObj.transform.parent = transform;
                        DisableCollider(tmpObj);
                        break;
                                
                }               
            }
        }
                   
    }

    void KeepGrab()
    {
        keepGrabbing = true;
        if (grabbedObject != null)
        {
            for (int i = 0; i < grabbedObject.Count; i++)
            {
                grabbedObject[i].transform.parent = null;
            }

        }
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject tmpObj = hit.transform.gameObject;
            if (tmpObj.layer == 8 && tmpObj.tag != "PanelObject" && !grabbedObject.Contains(tmpObj))
            {
                if ((whiteboardMode == 1 && tmpObj.tag == "ActualWhiteboard") || (whiteboardMode == 2 && tmpObj.tag != "ActualWhiteboard"))
                {


                    grabbedObject.Add(tmpObj);
                    lastRotation.Add(tmpObj.transform.rotation);
                    currentRotation.Add(tmpObj.transform.rotation);
                    List<List<Material>> mats = new List<List<Material>>();
                    materials.Add(mats);
                    Renderer[] rs = tmpObj.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in rs)
                    {
                        Material[] ms = r.materials;
                        List<Material> copyMs = new List<Material>();
                        mats.Add(copyMs);
                        foreach (Material m in ms)
                        {
                            Material copyM = new Material(m);
                            copyMs.Add(copyM);
                            m.color = Color.green;
                        }
                        r.materials = ms;

                    }
                    switch (tmpObj.tag)
                    {
                        default:
                            tmpObj.GetComponent<Rigidbody>().isKinematic = true;
                            DisableCollider(tmpObj);
                            //tmpObj.transform.position = transform.position;
                            //tmpObj.transform.parent = transform;
                            break;
                    }
                }
            }
        }
    }

    void StopKeepGrab()
    {
        keepGrabbing = false;
        if (grabbedObject != null)
        {
            for (int i = 0; i < grabbedObject.Count; i++)
            {
                grabbedObject[i].transform.parent = transform;
            }

        }
    }

    void DropObject()
    {
        grabbed = false;
        keepGrabbing = false;
        whiteboardMode = 0;

        if (grabbedObject != null)
        {
            for(int i = 0; i < grabbedObject.Count; i++)
            {
                List<List<Material>> mats = materials[i];
                Renderer[] rs = grabbedObject[i].GetComponentsInChildren<Renderer>();
                for(int j = 0;j<rs.Length;j++)
                {
                    rs[j].materials = (Material[])(mats[j].ToArray());

                }
                grabbedObject[i].transform.parent = null;
                grabbedObject[i].GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject[i].GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
                grabbedObject[i].GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity(i);
                EnableCollider(grabbedObject[i]);  
                if(grabbedObject[i].tag == "ActualWhiteboard")
                {
                    grabbedObject[i].GetComponent<WhiteBoardStick>().changeRotation();
                    grabbedObject[i].GetComponent<BoxCollider>().isTrigger = false;
                }
                else
                {
                    EnableCollider(grabbedObject[i]);
                }       
            }
            grabbedObject = null;
            lastRotation = null;
            currentRotation = null;
            materials = null;

        }
    }
    

    Vector3 GetAngularVelocity(int i)
    {
        Quaternion deltaRotation = currentRotation[i] * Quaternion.Inverse(lastRotation[i]);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }
    

    // Update is called once per frame
    void Update()
    {
        line.enabled = false;
        if (grabbedObject != null)
        {
            for(int i = 0; i < grabbedObject.Count; i++)
            {
                lastRotation[i] = currentRotation[i];
                currentRotation[i] = grabbedObject[i].transform.rotation;
            }
        }

        if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger,controller) < 0.8f && grabbed)
        {
            DropObject();
        }
        if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger,controller) > 0.8f)
        {
            line.enabled = true;
            line.numPositions = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * 10);

            if (grabbed)
            {
                if (keepGrabbing)
                {
                    if (OVRInput.GetUp(OVRInput.Button.Two,controller))
                    {
                        StopKeepGrab();
                    }else
                    {
                        KeepGrab();
                    }
                }
                else
                {
                    if (OVRInput.GetDown(OVRInput.Button.Two, controller))
                    {
                        Debug.Log("Keep grab");
                        KeepGrab();
                    }
                        
                }
                
            }else
            {
                GrabObject();
            }

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

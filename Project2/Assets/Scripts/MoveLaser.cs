using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class MoveLaser : MonoBehaviour {

    public LineRenderer line;
    public GameObject player;

    private Vector3 movePoint;
    private bool set = false;
    
    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstick))
        {
            transform.Rotate(30, 0, 0, Space.Self);
            Ray ray = new Ray(transform.position, transform.forward);
            transform.Rotate(-30, 0, 0, Space.Self);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                GameObject tmpObj = hit.transform.gameObject;
                if (tmpObj.tag == "Terrian")
                {
                    float distance = 0;
                    Plane hPlane = new Plane(Vector3.up, Vector3.zero);
                    if (hPlane.Raycast(ray, out distance))
                    {
                        set = true;
                        movePoint = ray.GetPoint(distance);
                        line.enabled = true;
                        List<Vector3> bezier = BezierHelper(transform.position, ray.GetPoint(distance), transform.forward);
                        line.numPositions = bezier.Count;
                        for (int i = 0; i < bezier.Count; i++)
                        {
                            line.SetPosition(i, bezier[i]);
                        }

                    }
                    else
                        line.enabled = false;
                }
                else
                    line.enabled = false;
            }
            else
                line.enabled = false;
            
        }
        else
        {
            line.enabled = false;
            if(set == true)
            {
                set = false;
                player.transform.position = new Vector3(movePoint.x, player.transform.position.y, movePoint.z);
            }
        }

        
	}

    List<Vector3> BezierHelper (Vector3 start, Vector3 end, Vector3 startTangent)
    {
        Vector3 tmp = new Vector3();
        List<Vector3> res = new List<Vector3>();
        tmp.x = end.x;
        tmp.z = end.z;
        tmp.y = (startTangent.y / startTangent.x) * (end.x - start.x) + start.y;
        for(float i = 0f;i<=1f;i += 0.01f)
        {
            res.Add(Vector3.Lerp((Vector3.Lerp(start, tmp, i)),Vector3.Lerp(tmp,end,i),i));
        }
        return res;
    }
}

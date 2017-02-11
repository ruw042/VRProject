using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class LaserBeam : MonoBehaviour {

    public GameObject explosionPrefab;
    public GameObject bullet;
    public GameObject ins;
    public GameObject parent;

    
    public RaycastHit hit;
    public LineRenderer line;

    //gaze place 
    public int place = 0;        //0 for brick, 1 for reset, 2 for switch, 3 for on/off, 4 for somewhere else

    //bullet type
    public int mode = 1;          //1 for laser and 0 for energy ball
    public int on = 1;            //1 for with weapon 0 for not

    //laser related
    public float laserTimeLeft = 2.0f;
    public int serial = -1;

    //energy ball related
    public float ballTimeLeft = 0.5f;

    //reset button related
    public float resetTimeLeft = 2.0f;

    //switch button related
    public float switchTimeLeft = 2.0f;

    //start button related
    public float startTimeLeft = 2.0f;

    //move related
    public float moveTimeLeft = 2.0f;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.numPositions = 2;
        line.enabled = false;
    }

    void Update()
    {
        
        Transform camTrans = Camera.main.transform;

        if (on == 1)
        {
            if (mode == 1)
            {
                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, camTrans.forward * 10 + camTrans.position);
            }
            else
            {
                line.enabled = false;
                ballTimeLeft -= Time.deltaTime;
                if (ballTimeLeft < 0)
                {
                    GameObject ball = Instantiate(bullet, transform.position, Quaternion.identity);
                    ball.GetComponent<Rigidbody>().AddForce((camTrans.forward * 10 + camTrans.position - transform.position) * 10);
                    
                    ballTimeLeft = 1.5f;
                }

            }
        }else
        {
            line.enabled = false;
        }
        
        //Ray ray = new Ray(transform.position, transform.forward);
        Ray ray = new Ray(camTrans.position, camTrans.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            GameObject tmpObj = hit.transform.gameObject;
            int tmpId = tmpObj.GetInstanceID();
            if (tmpObj.tag == "reset")
            {
                place = 1;
                resetTimeLeft -= Time.deltaTime;
                if (resetTimeLeft < 0)
                {
                    //RESET HERE !!!!!
                    GameObject[] objs;
                    objs = GameObject.FindGameObjectsWithTag("shootable");
                    foreach (GameObject rem in objs)
                    {
                        Destroy(rem);
                    }
                    GameObject tmp = Instantiate(ins, new Vector3(), Quaternion.identity);
                    //Destroy(tmp);
                    resetAll();
                }
                else
                {
                    resetExceptReset();
                }
            }
            else if (tmpObj.tag == "onoff")
            {
                place = 3;
                startTimeLeft -= Time.deltaTime;
                if (startTimeLeft < 0)
                {
                    on = 1 - on;
                    resetAll();
                }
                else
                {
                    resetExceptStart();
                }
            }
            else if (tmpObj.tag == "switch")
            {
                place = 2;
                switchTimeLeft -= Time.deltaTime;
                if (switchTimeLeft < 0)
                {
                    mode = 1 - mode;
                    resetAll();
                    ballTimeLeft = 0.5f;
                }
                else
                {
                    resetExceptSwitch();
                }
                Debug.LogFormat("You hit {0}, id is {1}, current serial is {3} time left is {2},tag is {4}", hit.collider.name, tmpId, switchTimeLeft, serial, tmpObj.tag);

            }
            
            else if (tmpObj.tag == "terran")
            {
                place = 5;
                moveTimeLeft -= Time.deltaTime;
                if (moveTimeLeft < 0)
                {
                    Plane hPlane = new Plane(Vector3.up, Vector3.zero);
                    // Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
                    float distance = 0;
                    // if the ray hits the plane...
                    if (hPlane.Raycast(ray, out distance))
                    {

                        // get the hit point:
                        parent.transform.position = new Vector3(ray.GetPoint(distance).x, parent.transform.position.y, ray.GetPoint(distance).z);
                        //temp.transform.position = ray.GetPoint(distance);
                    }
                    //Camera.main.transform.position = new Vector3(hit.transform.position.x, Camera.main.transform.position.y,hit.transform.position.z);
                    resetAll();
                }
                else
                {
                    resetExceptMove();
                }
                Debug.LogFormat("You hit {0}, id is {1}, current serial is {3} time left is {2},tag is {4}", hit.collider.name, tmpId, laserTimeLeft, serial, tmpObj.tag);
            }

            else if (on == 1 && mode == 1)
            {

                if (tmpObj.tag == "shootable")
                {
                    place = 0;
                    if (serial != tmpId)
                    {

                        serial = tmpId;
                        laserTimeLeft = 2.0f;
                        resetExceptLaser();
                    }
                    else
                    {

                        laserTimeLeft -= Time.deltaTime;
                        if (laserTimeLeft < 0)
                        {
                            Destroy(tmpObj);
                            Instantiate(explosionPrefab, tmpObj.transform.position, Quaternion.identity);
                            resetAll();
                        }
                        resetExceptLaser();
                    }
                }
                else
                {
                    resetAll();
                    place = 4;
                }
                    
            }
            else
            {
                place = 4;
                resetAll();
            }

            
        }
        else
        {
            place = 4;
            resetAll();
        }
            
        

    }


    void resetAll()
    {
        serial = -1;
        laserTimeLeft = 2.0f;
        resetTimeLeft = 2.0f;
        switchTimeLeft = 2.0f;
        startTimeLeft = 2.0f;
        moveTimeLeft = 2.0f;
    }

    void resetExceptReset()
    {
        serial = -1;
        laserTimeLeft = 2.0f;
        switchTimeLeft = 2.0f;
        startTimeLeft = 2.0f;
        moveTimeLeft = 2.0f;
    }

    void resetExceptSwitch()
    {
        serial = -1;
        laserTimeLeft = 2.0f;
        resetTimeLeft = 2.0f;
        startTimeLeft = 2.0f;
        moveTimeLeft = 2.0f;
    }

    void resetExceptLaser()
    {
        resetTimeLeft = 2.0f;
        switchTimeLeft = 2.0f;
        startTimeLeft = 2.0f;
        moveTimeLeft = 2.0f;
    }

    void resetExceptStart()
    {
        serial = -1;
        laserTimeLeft = 2.0f;
        resetTimeLeft = 2.0f;
        switchTimeLeft = 2.0f;
        moveTimeLeft = 2.0f;
    }

     void resetExceptMove()
    {
        serial = -1;
        laserTimeLeft = 2.0f;
        resetTimeLeft = 2.0f;
        switchTimeLeft = 2.0f;
        startTimeLeft = 2.0f;
        
    }
}

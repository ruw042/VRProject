using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class PlaneController : MonoBehaviour {

    public GameObject gameControl;
    public GameObject parser;
    LeapProvider provider;
    private float v;
    // Use this for initialization
    void Start () {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        v = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameControl.GetComponent<GameControl>().start);
        if (gameControl.GetComponent<GameControl>().start == true)
        {
            Frame frame = provider.CurrentFrame;
            foreach (Hand hand in frame.Hands)
            {
                if (hand.IsLeft)
                {
                   
                    if(hand.Fingers[1].IsExtended)
                        transform.forward = hand.Direction.ToVector3();
                }
                if (hand.IsRight)
                {
                    float angle = Vector3.Angle(Camera.main.transform.forward, hand.PalmNormal.ToVector3());
                    v = v + Mathf.Cos(Mathf.Deg2Rad * angle) * 0.1f;
                    if (v < 0)
                        v = 0;
                    if (v > 20)
                        v = 20;
                }
            }
            if (transform.position.y > 0)
                GetComponent<Rigidbody>().velocity = transform.forward * v;
        }
        else
        {
            
            Vector3 tmp = parser.GetComponent<Parser>().positions[parser.GetComponent<Parser>().nextPoint - 1];
            Vector3 tmp2 = parser.GetComponent<Parser>().positions[parser.GetComponent<Parser>().nextPoint];
            Vector3 face = tmp2 - tmp;
            transform.forward = face;
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            v = 0;
        }
            
    }
}

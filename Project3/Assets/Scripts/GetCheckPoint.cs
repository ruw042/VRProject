using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCheckPoint : MonoBehaviour {

    public GameObject parser;
    public GameObject finished;
    public GameObject gameControl;
    private List<GameObject> points;
	// Use this for initialization
	void Start () {
        points = parser.GetComponent<Parser>().checkPoints;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision)
    {
        if (gameControl.GetComponent<GameControl>().finished == false)
        {
            if (collision.gameObject.tag != "CheckPoint")
            {
                gameControl.GetComponent<GameControl>().ResetPosition();
            }
            else if (collision.gameObject.GetInstanceID() == points[parser.GetComponent<Parser>().nextPoint].GetInstanceID())
            {
                finished.GetComponent<FinishedText>().getCheckPoint();
                parser.GetComponent<Parser>().nextPoint++;
                Destroy(collision.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameControl.GetComponent<GameControl>().finished == false)
        {
            if(collision.gameObject.tag != "CheckPoint")
            {
                gameControl.GetComponent<GameControl>().ResetPosition();
            }
            else if (collision.gameObject.GetInstanceID() == points[parser.GetComponent<Parser>().nextPoint].GetInstanceID())
            {
                finished.GetComponent<FinishedText>().getCheckPoint();
                parser.GetComponent<Parser>().nextPoint++;
                Destroy(collision.gameObject);
            }
        }
    }
}

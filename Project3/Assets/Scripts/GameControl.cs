using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public GameObject parser;
    public GameObject timeText;
    public GameObject plane;
    public GameObject player;
    float startTime = 4;
    float time = 0;
    public bool start = false;
    public bool finished = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        if(start == false)
        {
            timeText.GetComponent<TextMesh>().text = ((int)startTime).ToString();
            startTime -= Time.deltaTime;
            if (startTime <=0)
            {
                start = true;
            }
        }else
        {
            timeText.GetComponent<TextMesh>().text = time.ToString(".0#") +"s";
            if (finished == false)
            {
                if (parser.GetComponent<Parser>().nextPoint >= parser.GetComponent<Parser>().checkPoints.Count)
                    finished = true;
                time += Time.deltaTime;
            }
        }
        
	}

    public void ResetPosition()
    {
        if(finished == false)
        {
            start = false;
            startTime = 4;
            Vector3 tmp = parser.GetComponent<Parser>().positions[parser.GetComponent<Parser>().nextPoint - 1];
            Vector3 tmp2 = parser.GetComponent<Parser>().positions[parser.GetComponent<Parser>().nextPoint];
            Vector3 face = tmp2- tmp;
            //Debug.Log(face);
            //Debug.Log(parser.GetComponent<Parser>().nextPoint - 1);
            plane.transform.position = tmp;
            player.transform.position = tmp;
            plane.transform.forward = face;
            player.transform.forward = face;
        }
    }
}

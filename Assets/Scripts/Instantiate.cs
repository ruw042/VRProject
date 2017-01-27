using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour {

    public GameObject brick;
    public GameObject brick2;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 90;
        /*
        var tmp = brick.transform;
        //tmp.Translate(new Vector3(0, y, 0));
        for (int i = 0; i < 30; i++)
        {
            Instantiate(brick, tmp.position, tmp.rotation);
            tmp.Translate(new Vector3(-1, 0, 0));
            tmp.Rotate(new Vector3(0, 12, 0));
        }*/
        GameObject tmpObj = Instantiate(brick);
        GameObject tmpObj2 = Instantiate(brick);
        var tmp2 = tmpObj2.transform;
        tmp2.Rotate(new Vector3(0,6,0));
        tmp2.Translate(new Vector3(-0.49586120506f, 0.5f, -0.02642079469f));
        var tmp = tmpObj.transform;
        Destroy(tmpObj);
        Destroy(tmpObj2);
        //float y = 0.0f;
        for (int j = 0; j < 5 ; j++)
        {
        
            //tmp.Translate(new Vector3(0, y, 0));
            for (int i = 0; i < 30; i++)
            {
                if(Random.value < 0.5)
                    Instantiate(brick, tmp.position, tmp.rotation);
                else
                    Instantiate(brick2, tmp.position, tmp.rotation);
                tmp.Translate(new Vector3(-1, 0, 0));
                tmp.Rotate(new Vector3(0, 12, 0));
            }
            tmp.Translate(new Vector3(0, 1.0f, 0));
        }

        for (int j = 0; j < 5; j++)
        {

            //tmp.Translate(new Vector3(0, y, 0));
            for (int i = 0; i < 30; i++)
            {
                if (Random.value < 0.5)
                    Instantiate(brick, tmp2.position, tmp2.rotation);
                else
                    Instantiate(brick2, tmp2.position, tmp2.rotation);
                tmp2.Translate(new Vector3(-1, 0, 0));
                tmp2.Rotate(new Vector3(0, 12, 0));
            }
            tmp2.Translate(new Vector3(0, 1.0f, 0));
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}

}

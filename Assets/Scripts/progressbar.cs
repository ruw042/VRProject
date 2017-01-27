using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressbar : MonoBehaviour {
    public UnityEngine.UI.Image crossHair;


    // Use this for initialization
    void Start () {
        crossHair.fillAmount = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject gun = GameObject.Find("GunWrapper");
        LaserBeam script = gun.GetComponent<LaserBeam>();
        int mode = script.place;
        switch (mode)
        {
            case 0:
                if (script.on == 1)
                    crossHair.fillAmount = (2.0f - script.laserTimeLeft) / 2.0f;
                else
                    crossHair.fillAmount = 1.0f;
                break;
            case 1:
                crossHair.fillAmount = (2.0f - script.resetTimeLeft) / 2.0f;
                break;
            case 2:
                crossHair.fillAmount = (2.0f - script.switchTimeLeft) / 2.0f;
                break;
            case 3:
                crossHair.fillAmount = (2.0f - script.startTimeLeft) / 2.0f;
                break;
            case 4:
                if(script.on == 1)
                {
                    if(script.mode == 1)
                        crossHair.fillAmount = 1.0f;
                    else
                        crossHair.fillAmount = (1.5f - script.ballTimeLeft) / 1.5f;
                }
                else
                    crossHair.fillAmount = 1.0f;
                break;
            case 5:
                crossHair.fillAmount = (2.0f - script.moveTimeLeft) / 2.0f;
                break;
        }
        
    }
}

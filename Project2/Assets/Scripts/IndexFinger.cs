using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexFinger : MonoBehaviour {

    public OVRInput.Controller controller;
    public GameObject closePanel;
    public GameObject savePanel;
    
	// Use this for initialization
	void Start () {
        closePanel.SetActive(false);
        savePanel.SetActive(false);

        //GetComponent<Save>().SaveObjs();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject tmp = other.gameObject;
        //Debug.Log(tmp.name);
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.8f && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) == 0)
        {
            //Debug.Log("456");
            if (tmp.name == "CloseButton")
            {
                closePanel.SetActive(true);
            }
            if(tmp.name == "ConfirmClose")
            {
                //closePanel.SetActive(false);
                Application.Quit();
            }
            if(tmp.name == "CancelClose")
            {
                closePanel.SetActive(false);
            }
            if(tmp.name == "SaveScene")
            {
                GetComponent<Save>().SaveObjs();
                savePanel.SetActive(true);
            }
            if(tmp.name == "LoadScene")
            {
                GetComponent<Load>().loadObjs();
                //print("Game loaded!");
            }
            if(tmp.name == "SaveDone")
            {
                savePanel.SetActive(false);
            }
        }
        

    }
}

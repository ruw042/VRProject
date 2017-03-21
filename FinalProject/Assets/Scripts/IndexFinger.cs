using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexFinger : MonoBehaviour {

    public OVRInput.Controller controller;
    public GameObject closePanel;
    public GameObject regenPanel;
    public GameObject player;
    public AudioClip clip;

    // Use this for initialization
    void Start()
    {
        closePanel.SetActive(false);
        regenPanel.SetActive(false);

        //GetComponent<Save>().SaveObjs();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject tmp = other.gameObject;
        //Debug.Log(tmp.name);
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.8f && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) == 0)
        {
            Vector3 pos = tmp.transform.position;
            //Debug.Log("456");
            if (tmp.name == "CloseButton")
            {
                closePanel.SetActive(true);
                AudioSource.PlayClipAtPoint(clip, pos, .1f);
            }
            if (tmp.name == "ConfirmClose")
            {
                //closePanel.SetActive(false);
                Application.Quit();
                AudioSource.PlayClipAtPoint(clip, pos, .1f);
            }
            if (tmp.name == "CancelClose")
            {
                closePanel.SetActive(false);
                AudioSource.PlayClipAtPoint(clip, pos, .1f);
            }
            
            if (tmp.name == "RegenButton")
            {
                regenPanel.SetActive(true);
                AudioSource.PlayClipAtPoint(clip, pos, .1f);
            }
            if (tmp.name == "ConfirmRegen")
            {
                player.GetComponent<GeneratePlanet>().Regen();
                regenPanel.SetActive(false);
                AudioSource.PlayClipAtPoint(clip, pos, .1f);
            }
            if (tmp.name == "CancelRegen")
            {
                regenPanel.SetActive(false);
                AudioSource.PlayClipAtPoint(clip, pos, .1f);
            }
        }


    }
}

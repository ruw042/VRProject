using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
       
	}

    void OnGUI()
    {
        
        
        if (Event.current.type == EventType.Repaint)
        {
            this.GetComponent<Camera>().Render();
        }
        
    }
	
	
}

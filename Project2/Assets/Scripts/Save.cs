using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    List<GameObject> FindGameObjectsWithLayer(int layer) {
        GameObject[] allObjs = (GameObject[])FindObjectsOfType(typeof(GameObject));
        List<GameObject> res = new List<GameObject>();
        for (int i = 0; i<allObjs.Length; i++) {
         if (allObjs[i].layer == layer) {
             res.Add(allObjs[i]);
         }
     }

        return res;
    }

    public void SaveObjs()
    {
        
        if (System.IO.File.Exists(@"data.txt"))
        {
            System.IO.File.Delete(@"data.txt");
        }

        System.IO.StreamWriter file = new System.IO.StreamWriter("data.txt");
        //System.IO.File.WriteAllText("data.txt", string.Empty);
        List<GameObject> grabs = FindGameObjectsWithLayer(8);
        for(int i = 0; i < grabs.Count; i++)
        {
            if(grabs[i].tag != "PanelObject")
                RecordData(grabs[i], file);
        }
        file.Close();
    }

    void RecordData(GameObject g, System.IO.StreamWriter file)
    {
        file.WriteLine(g.tag + "|" + g.transform.position.x + "|" + g.transform.position.y + "|" + g.transform.position.z + "|" + g.transform.rotation.x + "|" + g.transform.rotation.y + "|" + g.transform.rotation.z + "|" + g.transform.rotation.w + "|" + g.transform.localScale.x + "|" + g.transform.localScale.y + "|" + g.transform.localScale.z);
    }
}

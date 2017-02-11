using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour {

    public GameObject tv;
    public GameObject desk;
    public GameObject chair;
    public GameObject locker;
    public GameObject cabinet;
    public GameObject whiteboard;
    public GameObject workstation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    List<GameObject> FindGameObjectsWithLayer(int layer)
    {
        GameObject[] allObjs = (GameObject[])FindObjectsOfType(typeof(GameObject));
        List<GameObject> res = new List<GameObject>();
        for (int i = 0; i < allObjs.Length; i++)
        {
            if (allObjs[i].layer == layer)
            {
                res.Add(allObjs[i]);
            }
        }

        return res;
    }

    public void loadObjs()
    {
        string[] lines = System.IO.File.ReadAllLines("data.txt");
        //System.IO.StreamWriter file = new System.IO.StreamWriter("data.txt");
        List<GameObject> grabs = FindGameObjectsWithLayer(8);
        for (int i = 0; i < grabs.Count; i++)
        {
            if(grabs[i].tag != "PanelObject")
                Destroy(grabs[i]);
        }
        foreach(string line in lines)
        {
            string[] res = line.Split('|');
            switch (res[0])
            {

                case "ActualTV":
                    GameObject tmp = Instantiate(tv);
                    doStuff(tmp, res);
                    break;
                case "ActualDesk":
                    GameObject tmp1 = Instantiate(desk);
                    doStuff(tmp1, res);
                    break;
                case "ActualChair":
                    GameObject tmp2 = Instantiate(chair);
                    doStuff(tmp2, res);
                    break;
                case "ActualLocker":
                    GameObject tmp3 = Instantiate(locker);
                    doStuff(tmp3, res);
                    break;
                case "ActualCabinet":
                    GameObject tmp4 = Instantiate(cabinet);
                    doStuff(tmp4, res);
                    break;
                case "ActualWhiteboard":
                    GameObject tmp5 = Instantiate(whiteboard);
                    doStuff(tmp5, res);
                    break;
                case "ActualWorkstation":
                    GameObject tmp6 = Instantiate(workstation);
                    doStuff(tmp6, res);
                    break;                  
            }
        }
    }

    void doStuff(GameObject g,string[] res)
    {
        g.transform.position = new Vector3(float.Parse(res[1]), float.Parse(res[2]), float.Parse(res[3]));
        g.transform.rotation = new Quaternion(float.Parse(res[4]), float.Parse(res[5]), float.Parse(res[6]), float.Parse(res[7]));
        g.transform.localScale = new Vector3(float.Parse(res[8]), float.Parse(res[9]), float.Parse(res[10]));
    }
}

using UnityEngine;
using System.Collections;

public class Event_LayoutEnable : MonoBehaviour {

    public int ObjectCount;
    public Object[] LayoutObjects;
	// Use this for initialization
	void Start () {
        foreach (GameObject gameObject in LayoutObjects)
            gameObject.SetActive(false);
        if (LayoutObjects[ObjectCount]) ((GameObject)LayoutObjects[ObjectCount]).SetActive(true);
        
        Destroy(this.gameObject);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

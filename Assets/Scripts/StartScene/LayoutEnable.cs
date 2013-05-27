using UnityEngine;
using System.Collections;

public class LayoutEnable : MonoBehaviour {

    public int ObjectCount;
    public Object[] LayoutObject;
	// Use this for initialization
	void Start () {
        foreach (GameObject GameObj in LayoutObject)
            GameObj.SetActive(false);
        if (LayoutObject[ObjectCount]) ((GameObject)LayoutObject[ObjectCount]).SetActive(true);

        Destroy(this.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

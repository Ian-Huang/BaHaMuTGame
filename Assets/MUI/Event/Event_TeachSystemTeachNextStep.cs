using UnityEngine;
using System.Collections;

public class Event_TeachSystemTeachNextStep : MonoBehaviour {


    public TeachingSystem teachingSystem;
	// Use this for initialization
	void Start () {
	    StartCoroutine(teachingSystem.NextPart(0));
        Destroy(this.gameObject,0.1F);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

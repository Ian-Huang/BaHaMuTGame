using UnityEngine;
using System.Collections;

public class SetTimeScale : MonoBehaviour {

    public float TimeScale;
	// Use this for initialization
	void Start () {
        Time.timeScale = TimeScale;
        Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Event_AddMoney : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefsDictionary.script.SetValue("Money", PlayerPrefsDictionary.script.GetValue("Money") + 1000);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Event_Clear : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteAll();
        PlayerPrefsDictionary.PlayerPrefDictionary.Clear();
        Application.LoadLevel("StartScene");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

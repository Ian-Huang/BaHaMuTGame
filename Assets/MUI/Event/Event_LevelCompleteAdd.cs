using UnityEngine;
using System.Collections;

public class Event_LevelCompleteAdd : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        PlayerPrefsDictionary.script.SetValue("LevelComplete", PlayerPrefsDictionary.script.GetValue("LevelComplete") + 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

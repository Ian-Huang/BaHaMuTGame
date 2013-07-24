using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

    public string LoadLevelName;
	// Use this for initialization
	void Start () {
        
        LoadNextScene.script.LoadScene(LoadLevelName,2);
        //Application.LoadLevel(LoadLevelName);
        Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

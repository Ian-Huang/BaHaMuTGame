using UnityEngine;
using System.Collections;

public class DisableGameObject : MonoBehaviour {

    public GameObject Object;
	// Use this for initialization
	void Start () {
        Object.SetActive(false);
        Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

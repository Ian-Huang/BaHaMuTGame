using UnityEngine;
using System.Collections;

public class Event_LevelUp : MonoBehaviour {

    public GameDefinition.Role role;
    public LevelSystem.LevelUpType levelUpType;
	// Use this for initialization
	void Start () {
        LevelSystem.script.LevelUp(role, levelUpType);
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}

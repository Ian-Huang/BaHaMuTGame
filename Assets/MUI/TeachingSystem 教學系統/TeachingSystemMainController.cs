using UnityEngine;
using System.Collections;

public class TeachingSystemMainController : MonoBehaviour {

    public bool isBackgroindRunning;
    public bool isPlayerHalfHP;
    public bool isRecoverCure;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        if (!isBackgroindRunning)
            BackgroundController.script.isRunning = false;

        if (isPlayerHalfHP)
        {
           RolePropertyInfo[] rolePropertyInfo = GameObject.Find("RolesCollection").GetComponentsInChildren<RolePropertyInfo>();
           foreach (RolePropertyInfo r in rolePropertyInfo)
           {
               r.currentLife = r.currentLife / 2;
               r.cureRate = 0;
           }

        }

        if (isRecoverCure)
        {
            RolePropertyInfo[] rolePropertyInfo = GameObject.Find("RolesCollection").GetComponentsInChildren<RolePropertyInfo>();
            foreach (RolePropertyInfo r in rolePropertyInfo)
            {
                r.cureRate = 5;
            }
        }

    }

    void OnDisable()
    {
        BackgroundController.script.isRunning = true;
    }

}

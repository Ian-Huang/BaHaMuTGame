using UnityEngine;
using System.Collections;

public class TeachingSystemMainController : MonoBehaviour
{

    public bool isBackgroindRunning;
    public bool isPlayerHalfHP;
    public bool isRecoverCure;
    public bool isBossUIEffectShow;


    public GameObject UIBoss_Health;
    public GameObject UIMap_Progress;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
                r.currentLife = r.maxLife / 4;
                r.cureRate = 0;
            }
        }

        if (isRecoverCure)
        {
            RolePropertyInfo[] rolePropertyInfo = GameObject.Find("RolesCollection").GetComponentsInChildren<RolePropertyInfo>();
            foreach (RolePropertyInfo r in rolePropertyInfo)
            {
                r.cureRate = 10;
            }
        }

        if (isBossUIEffectShow)
        {
            UIBoss_Health.SetActive(true);
            UIMap_Progress.SetActive(true);
        }

    }

    void OnDisable()
    {
        BackgroundController.script.isRunning = true;
    }

}

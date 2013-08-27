using UnityEngine;
using System.Collections;

public class TeachingSystemMainController : MonoBehaviour
{
    public bool isBackgroindRunning;
    public bool isPlayerHalfHP;
    public bool isRecoverCure;
    public bool isBossUIEffectShow;
    public enum BossAttackType { None, Melee, Range };
    public BossAttackType bossAttackType;

    public GameObject UIBoss_Health;
    public GameObject UIMap_Progress;

    void OnEnable()
    {
        if (!isBackgroindRunning)
            BackgroundController.script.SetRunBackgroundState(false);

        if (isPlayerHalfHP)
        {
            RolePropertyInfo[] rolePropertyInfo = RolesCollection.script.gameObject.GetComponentsInChildren<RolePropertyInfo>();
            foreach (RolePropertyInfo r in rolePropertyInfo)
            {
                r.currentLife = r.maxLife / 5;
                r.cureRate = 0;
            }
        }

        if (isRecoverCure)
        {
            RolePropertyInfo[] rolePropertyInfo = RolesCollection.script.gameObject.GetComponentsInChildren<RolePropertyInfo>();
            foreach (RolePropertyInfo r in rolePropertyInfo)
                r.cureRate = 10;
        }

        if (isBossUIEffectShow)
        {
            UIBoss_Health.SetActive(true);
            UIMap_Progress.SetActive(true);
        }

        if (bossAttackType == BossAttackType.Melee)
            BossController.script.NearAttack();

        if (bossAttackType == BossAttackType.Range)
            BossController.script.FarAttack();
    }

    void OnDisable()
    {
        BackgroundController.script.SetRunBackgroundState(true);
    }
}
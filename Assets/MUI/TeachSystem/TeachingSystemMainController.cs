using UnityEngine;
using System.Collections;

public class TeachingSystemMainController : MonoBehaviour
{
    public bool isBackgroindRunning;
    public bool isPlayerHalfHP;
    public bool isRecoverCure;
    public bool isBossUIEffectShow;
    public bool isOver;
    public bool isAllRoleFullEnergy;
    public enum BossAttackType { None, Melee, Range };
    public BossAttackType bossAttackType;

    public GameObject UIBoss_Health;
    public GameObject UIMap_Progress;

    void OnEnable()
    {
        if (!isBackgroindRunning)
            BackgroundController.script.SetRunBackgroundState(false);
        else
            BackgroundController.script.SetRunBackgroundState(true);

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
            EffectCreator.script.isBossUIEffectShow = true;
        }

        if (bossAttackType == BossAttackType.Melee)
            BossController.script.NearAttack();

        if (bossAttackType == BossAttackType.Range)
            BossController.script.FarAttack();

        if (isOver)
        {
            MusicPlayer.script.ChangeBackgroungMusic(4);

            if (11 > PlayerPrefsDictionary.script.GetValue("LevelComplete"))
                PlayerPrefsDictionary.script.SetValue("LevelComplete", 11);
        }

        if (isAllRoleFullEnergy)
        {
            foreach (RolePropertyInfo script in GameObject.FindObjectsOfType(typeof(RolePropertyInfo)))
                script.CurrentEnergy = 1000;
        }



    }

    void OnDisable()
    {
        //BackgroundController.script.SetRunBackgroundState(true);
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modify Date：2013-08-22
/// Author：Ian
/// Description：
///     魔王控制器(樹人長老BOSS)
/// </summary>
public class BossController_TreeElder : MonoBehaviour
{
    public static BossController_TreeElder script;

    public ActionTimerData ActionTimer = new ActionTimerData();
    private float NextActionTime;

    public float CastingTime;
    public GameObject TreeRootObject;   //樹根物件

    public LayerMask AttackLayer;       //攻擊判定的Layer

    [HideInInspector]
    public SmoothMoves.BoneAnimation boneAnimation;
    [HideInInspector]
    public BossAction currentBossAction;   //確認目前魔王的動作狀態
    private BossPropertyInfo bossInfo { get; set; }


    // Use this for initialization
    void Start()
    {
        script = this;

        //載入敵人資訊
        this.bossInfo = this.GetComponent<BossPropertyInfo>();

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(UserTrigger);
        this.boneAnimation.RegisterColliderTriggerDelegate(WeaponHit);
        GameManager.script.RegisterBoneAnimation(this.boneAnimation);   //註冊BoneAnimation，GameManager統一管理

        this.boneAnimation.playAutomatically = false;
        this.boneAnimation.Play("nowake");
        this.currentBossAction = BossAction.未甦醒;
    }

    public List<int> treerootAttackList = new List<int>();


    /// <summary>
    /// 魔王準備進行遠距離攻擊
    /// </summary>
    /// <param name="time">等待秒數後開始攻擊</param>
    /// <returns></returns>
    IEnumerator ReadyFarAttack(float time)
    {
        int index;
        for (int i = 0; i < 2; i++)
        {
            do
            {
                index = Random.Range(0, 4);
            } while (this.treerootAttackList.Contains(index));
            this.treerootAttackList.Add(index);
        }

        //顯示提示提醒玩家(目標為隨機兩位角色)
        Instantiate(EffectCreator.script.道路危險提示[this.treerootAttackList[0]]);
        Instantiate(EffectCreator.script.道路危險提示[this.treerootAttackList[1]]);

        this.boneAnimation.Play("小樹根攻擊");       //開始撥放小

        yield return new WaitForSeconds(time);      //等待n秒

        //目標為兩位隨機角色
        float playRootLength = 0;
        foreach (var attackIndex in this.treerootAttackList)
        {
            Vector3 pos = RolesCollection.script.Roles[attackIndex].transform.position - new Vector3(0, 0, 0.1f);
            GameObject newObj = (GameObject)Instantiate(this.TreeRootObject, pos, this.TreeRootObject.transform.rotation);
            newObj.GetComponent<TreeElder_Root>().Damage = this.bossInfo.skillData.Find((GameDefinition.BossSkillData data) => { return data.SkillName == "小樹根攻擊"; }).Damage;
            SmoothMoves.BoneAnimation boneAnim = newObj.GetComponent<SmoothMoves.BoneAnimation>();
            boneAnim.playAutomatically = false;
            boneAnim.Play("小樹根");                   //播放"小樹根"動畫
            playRootLength = boneAnim["小樹根"].length;//計算"小樹根"長度(秒)
        }

        yield return new WaitForSeconds(playRootLength);      //小樹根動畫播完後

        this.currentBossAction = BossAction.閒置;
        this.boneAnimation.Play("idle");
        this.treerootAttackList.Clear();
        //this.boneAnimation.Play("遠距離攻擊");  //播放"遠距離攻擊"動畫
    }

    // Update is called once per frame
    void Update()
    {
        //從GameManager 確認BoneAnimation的狀態
        if (GameManager.script.GetBoneAnimationState(this.boneAnimation))
        {
            // 魔王必須未死亡
            if (!this.bossInfo.isDead)
            {
                if (this.currentBossAction == BossAction.登場中 | this.currentBossAction == BossAction.近距離攻擊)
                {
                    this.boneAnimation.Play("run");
                }
                else if (this.currentBossAction == BossAction.切換跑道)
                {
                    this.boneAnimation.Play("walk");
                }
                else if (this.currentBossAction == BossAction.閒置)
                {
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        if (this.currentBossAction != BossAction.遠距離攻擊)
                        {
                            this.currentBossAction = BossAction.遠距離攻擊;
                            StartCoroutine(ReadyFarAttack(this.CastingTime));  //等待n秒後，進行遠距離攻擊
                        }
                    }

                    if (!this.boneAnimation.isPlaying)
                        this.boneAnimation.Play("idle");
                }
            }
        }
    }

    /// <summary>
    /// 綁在敵人武器上的Collider，觸發攻擊判定(近距離攻擊使用)
    /// </summary>
    /// <param name="triggerEvent">觸發相關資訊</param>
    public void WeaponHit(SmoothMoves.ColliderTriggerEvent triggerEvent)
    {
        //確認是由"weapon"碰撞的collider
        if (triggerEvent.boneName == "weapon" && triggerEvent.triggerType == SmoothMoves.ColliderTriggerEvent.TRIGGER_TYPE.Enter)
        {
            if (((1 << triggerEvent.otherCollider.gameObject.layer) & this.AttackLayer.value) > 0)
            {
                //tag = MainBody
                if (triggerEvent.otherCollider.tag.CompareTo("MainBody") == 0)
                {
                    int damage = this.bossInfo.skillData.Find((GameDefinition.BossSkillData data) => { return data.SkillName == "近距離攻擊"; }).Damage;
                    triggerEvent.otherCollider.GetComponent<RolePropertyInfo>().DecreaseLife(damage);

                    //創建 斬擊特效BoneAnimation
                    SmoothMoves.BoneAnimation obj = (SmoothMoves.BoneAnimation)Instantiate(GameManager.script.EffectAnimationObject);
                    //設定動畫播放中心點
                    Vector3 expPos = triggerEvent.otherColliderClosestPointToBone;
                    expPos.z = triggerEvent.otherCollider.gameObject.transform.position.z - 1;
                    obj.mLocalTransform.position = expPos;
                    obj.playAutomatically = false;
                    //隨機撥放 1 或 2 動畫片段
                    if (Random.Range(0, 2) == 0)
                        obj.Play("撞擊特效01");
                    else
                        obj.Play("撞擊特效02");
                }
            }
        }
    }

    /// <summary>
    /// UserTrigger，觸發判定
    /// </summary>
    /// <param name="triggerEvent">觸發相關資訊</param>
    public void UserTrigger(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //確認是由"登場"動畫觸發的UserTrigger
        if (triggerEvent.animationName == "登場" & triggerEvent.boneName == "ChangeLayer")
        {
            //改變魔王Layer，更改為Boss Layer，啟動AI，角色開始攻擊
            this.gameObject.layer = LayerMask.NameToLayer("Boss");
            this.ActionTimer.ChangeTimerState(true);
        }
        //確認是由"遠距離攻擊"觸發的UserTrigger
        else if (triggerEvent.animationName == "遠距離攻擊")
        {
            //計算Boss與角色的距離
            float distance = Mathf.Abs(RolesCollection.script.Roles[0].transform.position.x - this.transform.position.x);
            int damage = this.bossInfo.skillData.Find((GameDefinition.BossSkillData data) => { return data.SkillName == "遠距離攻擊"; }).Damage;

            //發射遠距離攻擊物件(目標為BOSS面前兩位角色)
            //目標為第一位角色
            //Vector3 Posv3 = RolesCollection.script.Roles[this.currentBattlePositionIndex].transform.position + new Vector3(distance, 0, 0);
            //GameObject newObj = (GameObject)Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
            //newObj.GetComponent<ShootObjectInfo>().Damage = damage;
            //目標為第二位角色
            //Posv3 = RolesCollection.script.Roles[this.currentBattlePositionIndex + 1].transform.position + new Vector3(distance, 0, 0);
            //newObj = (GameObject)Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
            //newObj.GetComponent<ShootObjectInfo>().Damage = damage;

            this.currentBossAction = BossAction.閒置;
        }
    }

    /// <summary>
    /// 動作計時器
    /// </summary>
    [System.Serializable]
    public class ActionTimerData
    {
        public bool isRunTimer { get; private set; }
        public float 切換跑道後下次行動時間;
        public float 近距離攻擊後下次行動時間;
        public float 遠距離攻擊後下次行動時間;

        public void ChangeTimerState(bool state)
        {
            this.isRunTimer = state;
        }
    }

    /// <summary>
    /// 定位點資訊
    /// </summary>
    [System.Serializable]
    public class PositionData
    {
        public string PositionName; //位置在Scene中名稱
        [HideInInspector]
        public Transform PositionTransform;
    }

    public enum BossAction
    {
        未甦醒 = 0, 閒置 = 1, 登場中 = 2, 切換跑道 = 3, 近距離攻擊 = 4, 遠距離攻擊 = 5
    }
}

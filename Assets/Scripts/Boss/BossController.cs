using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modify Date：2013-08-27
/// Author：Ian
/// Description：
///     魔王控制器(巨型史萊姆BOSS、石巨人BOSS)
///     0809：新增註冊BoneAnimation到GameManager，方便管理
///     0810：新增BossAction，用來判斷目前魔王狀態
///     0815：新增石巨人BOSS AI
///     0816：從小怪資訊系統獨立出為王怪資訊系統
///     0818：新增自訂魔王控制
///     0822：透過定義的魔王招式資訊得到招式傷害值
///     0827：修正射擊物件不同腳本的判別(ShootObjectInfo_Once、ShootObjectInfo_Through)
/// </summary>
public class BossController : MonoBehaviour
{
    public static BossController script;

    public ActionTimerData ActionTimer = new ActionTimerData();
    private float NextActionTime;

    public List<PositionData> BattlePositionList = new List<PositionData>();    //戰鬥定位點資訊清單
    private int currentBattlePositionIndex = -1;                                //當前定位點的Index

    public float 登場移動時間;
    public float ChangeWalksideMoveTime;        //魔王切換跑道所需時間(秒)

    public float NearAttackRunDistance;             //近距離攻擊到玩家前要移動的距離
    public float NearAttackMoveTime;                //近距離攻擊移動時間

    public GameObject FarShootObject;   //遠距離射擊物件

    public LayerMask AttackLayer;       //攻擊判定的Layer

    private BossAction currentBossAction;   //確認目前魔王的動作狀態
    private Vector3 originScale;        //原始尺寸(物體翻轉使用)
    private BossPropertyInfo bossInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        script = this;
        GameManager.script.CurrentBossObject = this.gameObject; //紀錄當前魔王物件(使用絕技要暫停iTween)

        //載入敵人資訊
        this.bossInfo = this.GetComponent<BossPropertyInfo>();

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(UserTrigger);
        this.boneAnimation.RegisterColliderTriggerDelegate(WeaponHit);
        GameManager.script.RegisterBoneAnimation(this.boneAnimation);   //註冊BoneAnimation，GameManager統一管理

        //紀錄原始Scale
        this.originScale = this.transform.localScale;

        //抓到Boss的定位點
        foreach (var pos in this.BattlePositionList)
            pos.PositionTransform = GameObject.Find(pos.PositionName).transform;

        this.NextActionTime = 0;

        //測試用，進行魔王登場
        this.BossReadyAppear();
    }

    public void NearAttack()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
                                    "x", this.transform.position.x - this.NearAttackRunDistance,
                                    "time", this.NearAttackMoveTime,
                                    "easetype", iTween.EaseType.linear,
                                    "oncomplete", "NearAttackMoveComplete"
                                ));

        this.currentBossAction = BossAction.近距離攻擊;
    }

    public void FarAttack()
    {
        if (this.currentBossAction != BossAction.遠距離攻擊)
        {
            this.currentBossAction = BossAction.遠距離攻擊;
            StartCoroutine(ReadyFarAttack(1));  //等待n秒後，進行遠距離攻擊
        }
    }

    void ChangeWalkside()
    {
        int random;
        do
        {
            //選擇定位點，必須與當前定位點不同
            random = Random.Range(0, this.BattlePositionList.Count);
        } while (this.currentBattlePositionIndex == random);

        iTween.MoveTo(this.gameObject, iTween.Hash(
                    "position", this.BattlePositionList[random].PositionTransform.position,
                    "time", this.ChangeWalksideMoveTime,
                    "easetype", iTween.EaseType.linear,
                    "oncomplete", "ChangeWalkComplete",
                    "oncompleteparams", random
                ));

        this.currentBossAction = BossAction.切換跑道;
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
                #region 自訂BOSS

                if (this.bossInfo.Boss == GameDefinition.Boss.自訂Boss)
                {
                    if (this.currentBossAction == BossAction.登場中 | this.currentBossAction == BossAction.近距離攻擊)
                        this.boneAnimation.Play("run");

                    else if (this.currentBossAction == BossAction.切換跑道)
                        this.boneAnimation.Play("walk");

                    else if (this.currentBossAction == BossAction.閒置)
                    {
                        this.transform.localScale = this.originScale;
                        if (!this.boneAnimation.isPlaying)
                            this.boneAnimation.Play("idle");
                    }
                }
                #endregion

                #region 巨型史萊姆BOSS

                if (this.bossInfo.Boss == GameDefinition.Boss.巨型史萊姆BOSS)
                {
                    if (this.currentBossAction == BossAction.登場中 | this.currentBossAction == BossAction.近距離攻擊)
                        this.boneAnimation.Play("run");

                    else if (this.currentBossAction == BossAction.切換跑道)
                        this.boneAnimation.Play("walk");

                    else if (this.currentBossAction == BossAction.閒置)
                    {
                        if (this.ActionTimer.isRunTimer)
                        {
                            this.NextActionTime -= Time.deltaTime;
                            if (this.NextActionTime < 0)
                            {
                                switch (Random.Range(0, 3))
                                {
                                    //切換跑道
                                    case 0:
                                        this.ChangeWalkside();
                                        this.NextActionTime = this.ActionTimer.切換跑道後下次行動時間;
                                        break;
                                    //近距離攻擊
                                    case 1:
                                        this.NearAttack();
                                        this.NextActionTime = this.ActionTimer.近距離攻擊後下次行動時間;
                                        break;
                                    //遠距離攻擊
                                    case 2:
                                        this.FarAttack();
                                        this.NextActionTime = this.ActionTimer.遠距離攻擊後下次行動時間;
                                        break;
                                }
                            }
                        }

                        this.transform.localScale = this.originScale;
                        if (!this.boneAnimation.isPlaying)
                            this.boneAnimation.Play("idle");
                    }
                }
                #endregion

                #region 石巨人BOSS

                else if (this.bossInfo.Boss == GameDefinition.Boss.石巨人BOSS)
                {
                    if (this.currentBossAction == BossAction.登場中 | this.currentBossAction == BossAction.近距離攻擊 | this.currentBossAction == BossAction.切換跑道)
                        this.boneAnimation.Play("walk");

                    else if (this.currentBossAction == BossAction.閒置)
                    {
                        if (this.ActionTimer.isRunTimer)
                        {
                            this.NextActionTime -= Time.deltaTime;
                            if (this.NextActionTime < 0)
                            {
                                switch (Random.Range(0, 3))
                                {
                                    //切換跑道
                                    case 0:
                                        this.ChangeWalkside();
                                        this.NextActionTime = this.ActionTimer.切換跑道後下次行動時間;
                                        break;
                                    //近距離攻擊
                                    case 1:
                                        this.NearAttack();
                                        this.NextActionTime = this.ActionTimer.近距離攻擊後下次行動時間;
                                        break;
                                    //遠距離攻擊
                                    case 2:
                                        this.FarAttack();
                                        this.NextActionTime = this.ActionTimer.遠距離攻擊後下次行動時間;
                                        break;
                                }
                            }
                        }

                        this.transform.localScale = this.originScale;
                        if (!this.boneAnimation.isPlaying)
                            this.boneAnimation.Play("idle");
                    }
                }
                #endregion
            }
        }
    }

    #region 魔王登場相關函式

    /// <summary>
    /// 魔王登場設定，並開始執行
    /// </summary>
    void BossReadyAppear()
    {
        //魔王出現定位點為所有定位點清單中間值
        this.currentBattlePositionIndex = this.BattlePositionList.Count / 2;

        //魔王從出生點移動到定位點
        iTween.MoveTo(this.gameObject, iTween.Hash(
                    "position", this.BattlePositionList[this.currentBattlePositionIndex].PositionTransform.position,
                    "time", this.登場移動時間,   //移動過程秒數
                    "easetype", iTween.EaseType.easeInOutQuad,
                    "oncomplete", "BossAppearComplete"
                ));
        this.currentBossAction = BossAction.登場中;
    }

    /// <summary>
    /// 魔王登場定位完成後，執行此函式
    /// </summary>
    void BossAppearComplete()
    {
        this.currentBossAction = BossAction.閒置;
        this.ActionTimer.ChangeTimerState(false);
        this.boneAnimation.Play("登場");  //播放"登場"動畫
    }

    #endregion

    #region 切換跑道相關函式

    /// <summary>
    /// 當切換跑道完成後，執行此函式
    /// </summary>
    /// <param name="index">切換跑道的index</param>
    void ChangeWalkComplete(int index)
    {
        this.currentBattlePositionIndex = index;
        this.currentBossAction = BossAction.閒置;
    }

    #endregion

    #region 遠距離攻擊相關函式

    /// <summary>
    /// 魔王準備進行遠距離攻擊
    /// </summary>
    /// <param name="time">等待秒數後開始攻擊</param>
    /// <returns></returns>
    IEnumerator ReadyFarAttack(float time)
    {
        //顯示提示提醒玩家(目標為BOSS面前兩位角色)
        Instantiate(EffectCreator.script.道路危險提示[this.currentBattlePositionIndex]);
        Instantiate(EffectCreator.script.道路危險提示[this.currentBattlePositionIndex + 1]);

        yield return new WaitForSeconds(time);      //等待n秒

        this.boneAnimation.Play("遠距離攻擊");  //播放"遠距離攻擊"動畫
    }

    #endregion

    #region 近距離攻擊相關函式

    /// <summary>
    /// 從定位點移動至近距離攻擊點完成後，執行此函式
    /// </summary>
    void NearAttackMoveComplete()
    {
        this.currentBossAction = BossAction.閒置;
        this.ActionTimer.ChangeTimerState(false);
        this.boneAnimation.Play("近距離攻擊");      //播放"近距離攻擊"動畫
        StartCoroutine(BacktoOriginPosition(2.0f)); //等待n秒後，回到定位點
    }

    IEnumerator BacktoOriginPosition(float time)
    {
        yield return new WaitForSeconds(time);  //等待n秒

        iTween.MoveTo(this.gameObject, iTween.Hash(
                            "x", this.transform.position.x + this.NearAttackRunDistance,
                            "time", this.NearAttackMoveTime,
                            "easetype", iTween.EaseType.linear,
                            "oncomplete", "NearAttackFinishBackComplete"
                        ));

        //移動過程需翻轉Scale
        Vector3 v3Scale = this.originScale;
        v3Scale.x = -Mathf.Abs(v3Scale.x);
        this.boneAnimation.mLocalTransform.localScale = v3Scale;
        this.currentBossAction = BossAction.近距離攻擊;
    }

    /// <summary>
    /// 從近距離攻擊點移動至定位點完成後，執行此函式
    /// </summary>
    void NearAttackFinishBackComplete()
    {
        this.currentBossAction = BossAction.閒置;
        this.ActionTimer.ChangeTimerState(true);
    }

    #endregion

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
        if (triggerEvent.animationName == "登場" & triggerEvent.boneName != "ChangeLayer")
        {
            //鏡頭震動
            iTween.ShakePosition(Camera.main.gameObject, new Vector3(1, 1, 0), 0.15f);
        }
        //改變魔王Layer，更改為Boss Layer，啟動AI，角色開始攻擊
        else if (triggerEvent.animationName == "登場" & triggerEvent.boneName == "ChangeLayer")
        {
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
            Vector3 Posv3 = RolesCollection.script.Roles[this.currentBattlePositionIndex].transform.position + new Vector3(distance, 0, 0);
            GameObject newObj = (GameObject)Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
            if (newObj.GetComponent<ShootObjectInfo_Once>())
                newObj.GetComponent<ShootObjectInfo_Once>().Damage = damage;
            else if (newObj.GetComponent<ShootObjectInfo_Through>())
                newObj.GetComponent<ShootObjectInfo_Through>().Damage = damage;

            //目標為第二位角色
            Posv3 = RolesCollection.script.Roles[this.currentBattlePositionIndex + 1].transform.position + new Vector3(distance, 0, 0);
            newObj = (GameObject)Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
            if (newObj.GetComponent<ShootObjectInfo_Once>())
                newObj.GetComponent<ShootObjectInfo_Once>().Damage = damage;
            else if (newObj.GetComponent<ShootObjectInfo_Through>())
                newObj.GetComponent<ShootObjectInfo_Through>().Damage = damage;

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
        閒置 = 0, 登場中 = 1, 切換跑道 = 2, 近距離攻擊 = 3, 遠距離攻擊 = 4
    }
}

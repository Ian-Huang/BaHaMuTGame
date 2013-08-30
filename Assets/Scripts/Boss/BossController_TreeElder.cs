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

    private float currentAttackProbability;     //當前觸發小樹根攻擊的機率(0~1)
    public float minAttackProbability = 0.6F;  //觸發小樹根攻擊的最小可能機率(0~1) 預設0.6 => 60%

    [HideInInspector]
    public int addDamageValue;          //累積的傷害值(累積一定量後觸發開花事件)
    public int addDamageMaxValue;       //累積傷害的最大值，當前累積超過最大值觸發開花事件

    public List<GameObject> FlowerPointList = new List<GameObject>();
    private float currentAddFlowerOpenTime; //當前花朵開啟所需時間

    [HideInInspector]
    public int currentOpenFlowerCount;     //目前開出花朵的數目
    public GameObject FlowerObject;         //花朵物件

    public GameObject TreeRootObject;   //樹根物件
    public float CastingTime;           //樹根吟詠咒語時間(秒)

    public LayerMask AttackLayer;       //攻擊判定的Layer

    [HideInInspector]
    public BossAction currentBossAction;   //確認目前魔王的動作狀態    
    [HideInInspector]
    public SmoothMoves.BoneAnimation boneAnimation;
    private BossPropertyInfo bossInfo { get; set; }
    private List<int> treeRootAttackList = new List<int>();

    // Use this for initialization
    void Start()
    {
        script = this;        

        //載入敵人資訊
        this.bossInfo = this.GetComponent<BossPropertyInfo>();

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(UserTrigger);
        GameManager.script.RegisterBoneAnimation(this.boneAnimation);   //註冊BoneAnimation，GameManager統一管理

        this.boneAnimation.playAutomatically = false;
        this.boneAnimation.Play("nowake");
        this.currentBossAction = BossAction.未甦醒;

        this.currentOpenFlowerCount = 0;    //目前開出花朵的數目設定為0
        this.addDamageValue = 0;            //累積的傷害值設定為0
        this.ActionTimer.ChangeTimerState(false);                   //關閉計時器功能
        this.currentAttackProbability = this.minAttackProbability;  //當前觸發樹根攻擊的機率為最小機率
        this.currentAddFlowerOpenTime = this.ActionTimer.花朵生成時間;
        this.NextActionTime = this.ActionTimer.觸發小樹根間隔時間;
    }

    /// <summary>
    /// 執行樹根攻擊
    /// </summary>
    /// <param name="action">動作類型(大樹根攻擊or小樹根攻擊)</param>
    void TreeRootAttack(BossAction action)
    {
        this.currentBossAction = action;
        StartCoroutine(ReadyTreeRootAttack(this.CastingTime));  //等待吟詠咒語時間後，進行樹根攻擊
    }

    /// <summary>
    /// 執行開花功能
    /// </summary>
    void OpenFlower()
    {
        if (this.currentOpenFlowerCount < this.FlowerPointList.Count)
        {
            //開花，隨機一個未開花的點
            int index = -1;
            do
            {
                index = Random.Range(0, this.FlowerPointList.Count);
            } while (this.FlowerPointList[index].transform.childCount > 0);

            GameObject newObj = (GameObject)Instantiate(this.FlowerObject, this.FlowerPointList[index].transform.position, this.FlowerObject.transform.rotation);
            newObj.transform.parent = this.FlowerPointList[index].transform;
            this.currentOpenFlowerCount++;

            if (this.currentOpenFlowerCount == this.FlowerPointList.Count)
                this.NextActionTime = ActionTimer.使用樹根後等待時間;
        }
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
                if (this.currentBossAction != BossAction.未甦醒 && this.currentOpenFlowerCount < this.FlowerPointList.Count)
                {
                    this.currentAddFlowerOpenTime -= Time.deltaTime;
                    if (this.currentAddFlowerOpenTime < 0 | this.addDamageValue >= this.addDamageMaxValue)
                    {
                        this.currentAddFlowerOpenTime = ActionTimer.花朵生成時間;
                        this.addDamageValue = 0;
                        this.OpenFlower();
                    }
                }

                if (this.currentBossAction == BossAction.閒置)
                {
                    if (this.ActionTimer.isRunTimer)
                    {
                        this.NextActionTime -= Time.deltaTime;
                        if (this.NextActionTime < 0)
                        {
                            //如果當前已開最大開花數，執行大樹根攻擊
                            if (this.currentOpenFlowerCount == this.FlowerPointList.Count)
                            {
                                this.currentAttackProbability = this.minAttackProbability;  //樹根觸發機率回到最小機率
                                this.TreeRootAttack(BossAction.大樹根攻擊);

                                //關閉樹人長老BOSS頭上的花朵，相關數值初始化                                
                                foreach (var script in this.GetComponentsInChildren<TreeElder_Flower>())
                                    script.CloseFlower();
                                this.currentAddFlowerOpenTime = ActionTimer.花朵生成時間;
                                this.addDamageValue = 0;

                                return;
                            }

                            float p = Random.value;
                            //攻擊
                            if (p < this.currentAttackProbability)
                            {
                                this.currentAttackProbability = this.minAttackProbability;  //樹根觸發機率回到最小機率
                                this.TreeRootAttack(BossAction.小樹根攻擊);
                            }
                            else
                            {
                                this.currentAttackProbability += 0.1f;  //機率增加10%
                                this.NextActionTime = this.ActionTimer.觸發小樹根間隔時間;
                            }
                        }
                    }

                    if (!this.boneAnimation.isPlaying)
                        this.boneAnimation.Play("idle");
                }
            }
        }
    }

    /// <summary>
    /// 樹人長老BOSS準備進行樹根攻擊(小樹根攻擊、大樹根攻擊)
    /// </summary>
    /// <param name="time">等待秒數後開始樹根攻擊</param>
    /// <returns></returns>
    IEnumerator ReadyTreeRootAttack(float time)
    {
        int attackIndex = -1;           //攻擊目標的索引
        float playRootLength = -1;      //儲存樹根動畫秒數

        //小樹根攻擊(攻擊2位角色)
        if (this.currentBossAction == BossAction.小樹根攻擊)
        {
            for (int i = 0; i < 2; i++)
            {
                do
                {
                    attackIndex = Random.Range(0, 4);
                } while (this.treeRootAttackList.Contains(attackIndex));
                this.treeRootAttackList.Add(attackIndex);
            }

            //顯示提示提醒玩家(目標為隨機兩位角色)
            Instantiate(EffectCreator.script.道路危險提示[this.treeRootAttackList[0]]);
            Instantiate(EffectCreator.script.道路危險提示[this.treeRootAttackList[1]]);
            this.boneAnimation.Play("小樹根攻擊");       //開始播放小樹根攻擊動作

            yield return new WaitForSeconds(time);      //等待n秒(吟詠時間)

            //目標為兩位隨機角色
            foreach (var attackTarget in this.treeRootAttackList)
            {
                //產生樹根，攻擊玩家
                Vector3 pos = RolesCollection.script.Roles[attackTarget].transform.position - new Vector3(0, 0, 0.1f);
                GameObject newObj = (GameObject)Instantiate(this.TreeRootObject, pos, this.TreeRootObject.transform.rotation);
                SmoothMoves.BoneAnimation boneAnim = newObj.GetComponent<SmoothMoves.BoneAnimation>();
                newObj.GetComponent<TreeElder_Root>().Damage = this.bossInfo.skillData.Find((GameDefinition.BossSkillData data) => { return data.SkillName == "小樹根攻擊"; }).Damage;
                boneAnim.playAutomatically = false;
                boneAnim.Play("小樹根");                   //播放"小樹根"動畫
                playRootLength = boneAnim["小樹根"].length;//計算"小樹根"長度(秒)
            }
        }
        //大樹根攻擊(攻擊1位角色)
        else if (this.currentBossAction == BossAction.大樹根攻擊)
        {
            attackIndex = Random.Range(0, 4);

            //顯示提示提醒玩家(目標為隨機一位角色)
            Instantiate(EffectCreator.script.道路危險提示[attackIndex]);
            this.boneAnimation.Play("大樹根攻擊");       //開始播放大樹根攻擊動作

            yield return new WaitForSeconds(time);      //等待n秒(吟詠時間)

            //目標為隨機一位角色
            Vector3 pos = RolesCollection.script.Roles[attackIndex].transform.position - new Vector3(0, 0, 0.1f);
            GameObject newObj = (GameObject)Instantiate(this.TreeRootObject, pos, this.TreeRootObject.transform.rotation);
            SmoothMoves.BoneAnimation boneAnim = newObj.GetComponent<SmoothMoves.BoneAnimation>();
            newObj.GetComponent<TreeElder_Root>().Damage = this.bossInfo.skillData.Find((GameDefinition.BossSkillData data) => { return data.SkillName == "大樹根攻擊"; }).Damage;
            boneAnim.playAutomatically = false;
            boneAnim.Play("大樹根");                   //播放"大樹根"動畫
            playRootLength = boneAnim["大樹根"].length;//計算"大樹根"長度(秒)
        }

        yield return new WaitForSeconds(playRootLength);      //等待樹根動畫播完後

        this.currentBossAction = BossAction.閒置; //狀態切回"閒置"
        this.NextActionTime = ActionTimer.使用樹根後等待時間;
        this.boneAnimation.Play("idle");            //播放"idle"動畫
        this.treeRootAttackList.Clear();            //清除目前攻擊目標的清單
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
    }

    /// <summary>
    /// 動作計時器
    /// </summary>
    [System.Serializable]
    public class ActionTimerData
    {
        public bool isRunTimer { get; private set; }
        public float 觸發小樹根間隔時間;
        public float 使用樹根後等待時間;
        public float 花朵生成時間;

        public void ChangeTimerState(bool state)
        {
            this.isRunTimer = state;
        }
    }

    public enum BossAction
    {
        未甦醒 = 0, 閒置 = 1, 小樹根攻擊 = 2, 大樹根攻擊 = 3
    }
}

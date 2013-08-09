using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date：2013-08-04
/// Modify Date：2013-08-09
/// Author：Ian
/// Description：
///     魔王控制器(測試用)
///     0809新增：註冊BoneAnimation到GameManager，方便管理
/// </summary>
public class BossController : MonoBehaviour
{
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
    public List<PositionData> PositionList = new List<PositionData>();  //定位點資訊清單
    private int currentPositionIndex = -1;  //當前定位點的Index

    public float ChangeWalksideMoveTime;        //魔王切換跑道所需時間(秒)
    private bool checkChangeWalk = false;   //確認是否正在切換跑道


    public float NearAttackRunDistance; //近距離攻擊到玩家前要移動的距離
    public float NearAttackMoveTime;    //近距離攻擊移動時間
    private bool checkRunningNearAttack = false;    //確認是否正在移動準備近距離攻擊

    private bool checkShooting = false;        //確認現在是否為射擊狀態
    public GameObject FarShootObject;   //遠距離射擊物件

    public SmoothMoves.BoneAnimation EffectAnimation;   //效果動畫物件
    public LayerMask AttackLayer;       //攻擊判定的Layer
    private Vector3 originScale;        //原始尺寸

    private bool bossReadyAppear = false;   //魔王出現確認(true => 魔王正在出現中)

    private MoveController moveController { get; set; }
    private EnemyPropertyInfo enemyInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        //載入敵人資訊
        this.enemyInfo = this.GetComponent<EnemyPropertyInfo>();
        this.moveController = this.GetComponent<MoveController>();

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(UserTrigger);
        this.boneAnimation.RegisterColliderTriggerDelegate(WeaponHit);
        GameManager.script.RegisterBoneAnimation(this.boneAnimation);   //註冊BoneAnimation，GameManager統一管理

        //紀錄原始Scale
        this.originScale = this.transform.localScale;

        //抓到Boss的定位點
        foreach (var pos in this.PositionList)
            pos.PositionTransform = GameObject.Find(pos.PositionName).transform;

        //測試用，進行魔王登場
        this.BossReadyAppear();
    }

    /// <summary>
    /// 魔王登場定位完成後，執行此函式
    /// </summary>
    void BossAppearComplete()
    {
        this.bossReadyAppear = false;
        this.boneAnimation.Play("出現");  //播放"出現"動畫
    }

    /// <summary>
    /// 魔王登場設定，並開始執行
    /// </summary>
    void BossReadyAppear()
    {
        //魔王出現定位點為所有定位點清單中間值
        this.currentPositionIndex = this.PositionList.Count / 2;

        //魔王從出生點移動到定位點
        iTween.MoveTo(this.gameObject, iTween.Hash(
                    "position", this.PositionList[this.currentPositionIndex].PositionTransform.position,
                    "time", 2.5f,   //移動過程秒數
                    "easetype", iTween.EaseType.easeInOutQuad,
                    "oncomplete", "BossAppearComplete"
                ));
        this.bossReadyAppear = true;
    }

    /// <summary>
    /// 魔王準備進行遠距離攻擊
    /// </summary>
    /// <param name="time">等待秒數後開始攻擊</param>
    /// <returns></returns>
    IEnumerator ReadyFarAttack(float time)
    {
        this.checkShooting = true;

        //顯示提示提醒玩家(目標為BOSS面前兩位角色)
        Instantiate(EffectCreator.script.道路危險提示[this.currentPositionIndex]);
        Instantiate(EffectCreator.script.道路危險提示[this.currentPositionIndex + 1]);

        yield return new WaitForSeconds(time);      //等待n秒

        this.boneAnimation.Play("發射");  //播放"發射"動畫
    }

    /// <summary>
    /// 當切換跑道完成後，執行此函式
    /// </summary>
    /// <param name="index">切換跑道的index</param>
    void ChangeWalkComplete(int index)
    {
        this.currentPositionIndex = index;
        this.checkChangeWalk = false;
    }

    /// <summary>
    /// 從定位點移動至近距離攻擊點完成後，執行此函式
    /// </summary>
    void NearAttackMoveComplete()
    {
        this.checkRunningNearAttack = false;
        this.boneAnimation.Play("突刺");      //播放"突刺"動畫
        StartCoroutine(BacktoOriginPosition(2.0f)); //等待n秒後，回到定位點
    }

    /// <summary>
    /// 從近距離攻擊點移動至定位點完成後，執行此函式
    /// </summary>
    void NearAttackFinishBackComplete()
    {
        this.checkRunningNearAttack = false;
        this.boneAnimation.Play("idle");
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
        this.checkRunningNearAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        //從GameManager 確認BoneAnimation的狀態
        if (GameManager.script.GetBoneAnimationState(this.boneAnimation))
        {
            if (this.bossReadyAppear)
            {
                this.moveController.ChangeSpeed(0);
                this.boneAnimation.Play("run");
            }
            else if (this.checkChangeWalk)
            {
                this.moveController.ChangeSpeed(0);
                this.boneAnimation.Play("walk");
            }
            else if (this.checkRunningNearAttack)
            {
                this.moveController.ChangeSpeed(0);
                this.boneAnimation.Play("run");
            }
            else
            {

                if (Input.GetKeyDown(KeyCode.I))
                {
                    iTween.MoveTo(this.gameObject, iTween.Hash(
                                "x", this.transform.position.x - this.NearAttackRunDistance,
                                "time", this.NearAttackMoveTime,
                                "easetype", iTween.EaseType.linear,
                                "oncomplete", "NearAttackMoveComplete"
                            ));

                    this.checkRunningNearAttack = true;
                }

                if (Input.GetKeyDown(KeyCode.U))
                {
                    int random;
                    do
                    {
                        //選擇定位點，必須與當前定位點不同
                        random = Random.Range(0, this.PositionList.Count);
                    } while (this.currentPositionIndex == random);

                    iTween.MoveTo(this.gameObject, iTween.Hash(
                                "position", this.PositionList[random].PositionTransform.position,
                                "time", this.ChangeWalksideMoveTime,
                                "easetype", iTween.EaseType.linear,
                                "oncomplete", "ChangeWalkComplete",
                                "oncompleteparams", random
                            ));
                    this.checkChangeWalk = true;
                }


                if (Input.GetKeyDown(KeyCode.Y))
                {
                    if (!this.checkShooting)
                        StartCoroutine(ReadyFarAttack(1));  //等待n秒後，進行遠距離攻擊
                }

                this.transform.localScale = this.originScale;

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.boneAnimation.Play("run");
                    this.moveController.ChangeSpeed(15);

                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.boneAnimation.Play("run");
                    Vector3 v3Scale = this.originScale;
                    v3Scale.x = -Mathf.Abs(v3Scale.x);
                    this.boneAnimation.mLocalTransform.localScale = v3Scale;
                    this.moveController.ChangeSpeed(-15);
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.boneAnimation.Play("walk");
                    this.transform.Translate(0, Time.deltaTime, 0);
                    this.moveController.ChangeSpeed(0);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.boneAnimation.Play("walk");
                    this.transform.Translate(0, -Time.deltaTime, 0);
                    this.moveController.ChangeSpeed(0);
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    this.boneAnimation.Play("出現");
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    this.boneAnimation.Play("突刺");
                }
                else
                {
                    if (!this.animation.isPlaying)
                        this.boneAnimation.Play("idle");
                    this.moveController.ChangeSpeed(0);
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
                    triggerEvent.otherCollider.GetComponent<RolePropertyInfo>().DecreaseLife(this.enemyInfo.nearDamage);

                    //創建 斬擊特效BoneAnimation
                    SmoothMoves.BoneAnimation obj = (SmoothMoves.BoneAnimation)Instantiate(this.EffectAnimation);
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
        //確認是由"出現"觸發的UserTrigger
        if (triggerEvent.animationName == "出現" & triggerEvent.boneName != "ChangeLayer")
        {
            //鏡頭震動
            iTween.ShakePosition(Camera.main.gameObject, new Vector3(1, 1, 0), 0.15f);
        }
        //改變魔王Layer，更改為Enemy Layer，角色開始攻擊
        else if (triggerEvent.animationName == "出現" & triggerEvent.boneName == "ChangeLayer")
        {
            this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
        //確認是由"發射"觸發的UserTrigger
        else if (triggerEvent.animationName == "發射")
        {
            //計算Boss與角色的距離
            float distance = Mathf.Abs(RolesCollection.script.Roles[0].transform.position.x - this.transform.position.x);

            //發射史萊姆砲(目標為BOSS面前兩位角色)
            //目標為第一位角色
            Vector3 Posv3 = RolesCollection.script.Roles[this.currentPositionIndex].transform.position + new Vector3(distance, 0, 0);
            GameObject newObj = (GameObject)Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
            newObj.GetComponent<ShootObjectInfo>().Damage = this.enemyInfo.farDamage;
            //目標為第二位角色
            Posv3 = RolesCollection.script.Roles[this.currentPositionIndex + 1].transform.position + new Vector3(distance, 0, 0);
            newObj = (GameObject)Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
            newObj.GetComponent<ShootObjectInfo>().Damage = this.enemyInfo.farDamage;

            this.checkShooting = false;
        }
    }
}

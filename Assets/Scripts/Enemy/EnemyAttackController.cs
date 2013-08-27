using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-27
/// Author：Ian
/// Description：
///     敵人攻擊控制器
///     0809新增：註冊BoneAnimation到GameManager，方便管理
///     0816修改：不分近or遠距離攻擊值
///     0827：修正射擊物件不同腳本的判別(ShootObjectInfo_Once、ShootObjectInfo_Through)
/// </summary>
public class EnemyAttackController : MonoBehaviour
{
    public float AttackDistance;        //攻擊距離
    public GameObject ShootObject;      //遠距離攻擊發射出的物件
    public LayerMask AttackLayer;       //攻擊判定的Layer

    private EnemyPropertyInfo enemyInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;
    private RaycastHit hitData;

    // Use this for initialization
    void Start()
    {
        //載入敵人資訊
        this.enemyInfo = this.GetComponent<EnemyPropertyInfo>();

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterColliderTriggerDelegate(WeaponHit);
        this.boneAnimation.RegisterUserTriggerDelegate(ShootEvent);
        GameManager.script.RegisterBoneAnimation(this.boneAnimation);   //註冊BoneAnimation，GameManager統一管理
    }

    // Update is called once per frame
    void Update()
    {
        //從GameManager 確認BoneAnimation的狀態
        if (GameManager.script.GetBoneAnimationState(this.boneAnimation))
        {
            // 怪物必須未死亡
            if (!this.enemyInfo.isDead)
            {
                if (Physics.Raycast(this.transform.position, Vector3.left, out this.hitData, this.AttackDistance, this.AttackLayer))
                {
                    //tag = MainBody
                    if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                        if (!this.boneAnimation.IsPlaying("attack"))
                        {
                            this.boneAnimation.Play("attack");
                            this.GetComponent<MoveController>().isRunning = false;
                        }
                }
                else
                {
                    //確認目前動畫狀態(必須沒在播其他動畫)
                    if (!this.boneAnimation.isPlaying)
                    {
                        this.boneAnimation.Play("walk");
                        this.GetComponent<MoveController>().isRunning = true;
                    }
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
                    triggerEvent.otherCollider.GetComponent<RolePropertyInfo>().DecreaseLife(this.enemyInfo.damage);

                    //創建 斬擊特效BoneAnimation
                    SmoothMoves.BoneAnimation obj = (SmoothMoves.BoneAnimation)Instantiate(GameManager.script.EffectAnimationObject);

                    //設定動畫播放中心點
                    Vector3 expPos = triggerEvent.otherColliderClosestPointToBone;
                    expPos.z = triggerEvent.otherCollider.gameObject.transform.position.z - 1;
                    obj.mLocalTransform.position = expPos;
                    obj.playAutomatically = false;

                    //不同種類的怪物產生不同的攻擊特效
                    switch (this.enemyInfo.Enemy)
                    {
                        case GameDefinition.Enemy.史萊姆:
                        case GameDefinition.Enemy.火焰史萊姆:
                        case GameDefinition.Enemy.硬化史萊姆:
                        case GameDefinition.Enemy.中型史萊姆:
                        case GameDefinition.Enemy.中型火焰史萊姆:
                        case GameDefinition.Enemy.中型硬化史萊姆:
                            //隨機撥放 1 或 2 動畫片段
                            if (Random.Range(0, 2) == 0)
                                obj.Play("撞擊特效01");
                            else
                                obj.Play("撞擊特效02");
                            break;

                        default:
                            //隨機撥放 1 或 2 動畫片段
                            if (Random.Range(0, 2) == 0)
                                obj.Play("斬擊特效01");
                            else
                                obj.Play("斬擊特效02");
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 綁在敵人武器上的UserTrigger，觸發攻擊判定(遠距離攻擊使用)
    /// </summary>
    /// <param name="triggerEvent">觸發相關資訊</param>
    public void ShootEvent(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //確認是由"weapon"觸發的UserTrigger
        if (triggerEvent.boneName == "weapon")
        {
            //產生射擊物件
            GameObject obj = (GameObject)Instantiate(this.ShootObject, this.transform.position - new Vector3(0, 0, 0.1f), this.ShootObject.transform.rotation);

            //設定物件的parent 、 layer 、 Damage
            obj.layer = LayerMask.NameToLayer("ShootObject");
            obj.transform.parent = GameObject.Find("UselessObjectCollection").transform;
            if (obj.GetComponent<ShootObjectInfo_Once>())
                obj.GetComponent<ShootObjectInfo_Once>().Damage = this.enemyInfo.damage;
            else if (obj.GetComponent<ShootObjectInfo_Through>())
                obj.GetComponent<ShootObjectInfo_Through>().Damage = this.enemyInfo.damage;
        }
    }

    void OnDrawGizmos()
    {
        //畫出偵測線
        Gizmos.color = Color.black;
        Gizmos.DrawRay(this.transform.position, Vector3.left * this.AttackDistance);
    }
}
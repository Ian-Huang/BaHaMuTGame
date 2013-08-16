using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-17
/// Author：Ian
/// Description：
///     角色攻擊控制器 (敵人、障礙物)
///     0809新增：註冊BoneAnimation到GameManager，方便管理
///     0816修改：不分近or遠距離攻擊值
///     0817新增魔王資訊的判斷
/// </summary>
public class RoleAttackController : MonoBehaviour
{
    public float AttackDistance;        //攻擊距離
    public GameObject ShootObject;      //遠距離攻擊發射出的物件
    public SmoothMoves.BoneAnimation EffectAnimation;   //效果動畫物件
    public LayerMask AttackLayer;       //判定是否攻擊的Layer (Enemy、Boss、Obstacle)

    private RolePropertyInfo roleInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;
    private RaycastHit hitData;

    // Use this for initialization
    void Start()
    {
        //載入角色資訊
        this.roleInfo = this.GetComponent<RolePropertyInfo>();

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
            // 角色必須未虛弱
            if (!this.roleInfo.isWeak)
            {
                //判別物件為何？  敵人與障礙物有不同的處理
                if (Physics.Raycast(this.transform.position, Vector3.right, out this.hitData, this.AttackDistance, this.AttackLayer))
                {
                    //tag = MainBody  (物件主體)
                    if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                    {
                        string layerName = LayerMask.LayerToName(this.hitData.collider.gameObject.layer);
                        bool isCheck = false;

                        //判別物件為何？  敵人、障礙物、魔王有不同的處理
                        if (layerName.CompareTo("Enemy") == 0)
                        {
                            if (!this.hitData.collider.GetComponent<EnemyPropertyInfo>().isDead)    //確認Enemy是否已經死亡
                                isCheck = true;
                        }
                        else if (layerName.CompareTo("Boss") == 0)
                        {
                            if (!this.hitData.collider.GetComponent<BossPropertyInfo>().isDead)    //確認Boss是否已經死亡
                                isCheck = true;
                        }
                        else if (layerName.CompareTo("Obstacle") == 0)
                        {
                            if (!this.hitData.collider.GetComponent<ObstaclePropertyInfo>().isDisappear)    //確認Obstacle是否已經消失
                                if (this.GetComponent<ObstacleSystem>().ObstacleList.Contains(this.hitData.collider.GetComponent<ObstaclePropertyInfo>().Obstacle))
                                    isCheck = true;
                        }

                        if (isCheck)
                            if (!this.boneAnimation.IsPlaying("attack"))
                                this.boneAnimation.Play("attack");
                    }
                }

                //確認目前動畫狀態(必須沒再播attack)
                if (!this.boneAnimation.IsPlaying("attack"))
                {
                    //判定背景是否有在動，以此決定角色的動作狀態
                    if (BackgroundController.script.isRunning)
                        this.boneAnimation.Play("walk");
                    else
                        this.boneAnimation.Play("idle");
                }
            }
        }
    }

    /// <summary>
    /// 綁在角色武器上的Collider，觸發攻擊判定(近距離攻擊使用)
    /// </summary>
    /// <param name="triggerEvent">觸發相關資訊</param>
    public void WeaponHit(SmoothMoves.ColliderTriggerEvent triggerEvent)
    {
        //確認是由"weapon"碰撞的collider
        if (triggerEvent.boneName == "weapon" && triggerEvent.triggerType == SmoothMoves.ColliderTriggerEvent.TRIGGER_TYPE.Enter)
        {
            //tag = MainBody  (物件主體)
            if (triggerEvent.otherCollider.tag.CompareTo("MainBody") == 0)
            {
                string layerName = LayerMask.LayerToName(triggerEvent.otherCollider.gameObject.layer);
                bool isCheck = false;

                //判別物件為何？  敵人、障礙物、魔王有不同的處理
                if (layerName.CompareTo("Enemy") == 0)
                {
                    triggerEvent.otherCollider.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.roleInfo.damage);
                    isCheck = true;
                }
                else if (layerName.CompareTo("Boss") == 0)
                {
                    triggerEvent.otherCollider.GetComponent<BossPropertyInfo>().DecreaseLife(this.roleInfo.damage);
                    isCheck = true;
                }
                else if (layerName.CompareTo("Obstacle") == 0)
                {
                    triggerEvent.otherCollider.GetComponent<ObstaclePropertyInfo>().CheckObstacle(true);
                    isCheck = true;
                }

                if (isCheck)
                {
                    //創建 斬擊特效BoneAnimation
                    SmoothMoves.BoneAnimation obj = (SmoothMoves.BoneAnimation)Instantiate(this.EffectAnimation);
                    //設定動畫播放中心點
                    Vector3 expPos = triggerEvent.otherColliderClosestPointToBone;
                    expPos.z = triggerEvent.otherCollider.gameObject.transform.position.z - 1;
                    obj.mLocalTransform.position = expPos;
                    obj.playAutomatically = false;
                    //隨機撥放 1 或 2 動畫片段
                    if (Random.Range(0, 2) == 0)
                        obj.Play("斬擊特效01");
                    else
                        obj.Play("斬擊特效02");
                }
            }
        }
    }

    /// <summary>
    /// 綁在角色武器上的UserTrigger，觸發攻擊判定(遠距離攻擊使用)
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

            ShootObjectInfo info = obj.GetComponent<ShootObjectInfo>();
            info.Damage = this.roleInfo.damage;
        }
    }

    void OnDrawGizmos()
    {
        //畫出偵測線
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.AttackDistance);
    }
}

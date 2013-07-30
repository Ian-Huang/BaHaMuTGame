using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-23
/// Modify Date：2013-07-31
/// Author：Ian
/// Description：
///     角色攻擊控制器 (敵人、障礙物)
/// </summary>
public class RoleAttackController : MonoBehaviour
{
    public float AttackDistance;        //攻擊距離
    public GameObject ShootObject;      //遠距離攻擊發射出的物件
    public LayerMask EnemyLayer;       //判定是否攻擊的敵人Layer
    public LayerMask ObstacleLayer;       //判定是否攻擊的障礙物Layer

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
    }

    // Update is called once per frame
    void Update()
    {
        // 角色必須未虛弱
        if (!this.roleInfo.isWeak)
        {
            //判別物件為何？  敵人與障礙物有不同的處理
            if (Physics.Raycast(this.transform.position, Vector3.right, out this.hitData, this.AttackDistance, this.EnemyLayer))
            {
                //tag = MainBody  (物件主體)
                if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                    if (!this.hitData.collider.GetComponent<EnemyPropertyInfo>().isDead)    //確認Enemy是否已經死亡
                        if (!this.boneAnimation.IsPlaying("attack"))
                            this.boneAnimation.Play("attack");
            }
            else if (Physics.Raycast(this.transform.position, Vector3.right, out this.hitData, this.AttackDistance, this.ObstacleLayer))
            {
                //tag = MainBody  (物件主體)
                if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                    if (!this.hitData.collider.GetComponent<ObstaclePropertyInfo>().isDisappear)    //確認Obstacle是否已經消失
                        if (this.GetComponent<ObstacleSystem>().ObstacleList.Contains(this.hitData.collider.GetComponent<ObstaclePropertyInfo>().Obstacle))
                            if (!this.boneAnimation.IsPlaying("attack"))
                                this.boneAnimation.Play("attack");
            }
            else
            {
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
                //判別物件為何？  敵人與障礙物有不同的處理
                if (((1 << triggerEvent.otherCollider.gameObject.layer) & this.EnemyLayer.value) > 0)
                    triggerEvent.otherCollider.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.roleInfo.nearDamage);

                else if (((1 << triggerEvent.otherCollider.gameObject.layer) & this.ObstacleLayer.value) > 0)
                    triggerEvent.otherCollider.GetComponent<ObstaclePropertyInfo>().CheckObstacle(true);
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
            info.Damage = this.roleInfo.farDamage;
        }
    }

    void OnDrawGizmos()
    {
        //畫出偵測線
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.AttackDistance);
    }
}

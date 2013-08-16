﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-17
/// Author：Ian
/// Description：
///     遠距離(角色/敵人)發射的物件資訊
///     0817新增魔王資訊的判斷
/// </summary>
public class ShootObjectInfo : MonoBehaviour
{
    public int Damage;                              //造成的傷害值
    public GameDefinition.AttackType AttackType;    //攻擊的類型(物理、魔法)
    public LayerMask ExplosiveLayer;                //爆破的對象

    private bool isExplosion { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;

    void OnTriggerEnter(Collider other)
    {
        if (!this.isExplosion)
        {
            if (((1 << other.collider.gameObject.layer) & this.ExplosiveLayer.value) > 0)   //判定爆炸的Layer
            {
                if (other.collider.tag.CompareTo("MainBody") == 0)
                {
                    //如對象為敵人，需再檢查敵人本身是否已經死亡(EnemyPropertyInfo.isDead)
                    if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        if (other.GetComponent<EnemyPropertyInfo>().isDead)
                            return;
                    }
                    else if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
                    {
                        if (other.GetComponent<BossPropertyInfo>().isDead)
                            return;
                    }

                    //以下產生爆炸事件
                    this.isExplosion = true;

                    //爆炸中心為Collider交錯點
                    Vector3 expPos = other.ClosestPointOnBounds(this.transform.position);
                    expPos.z = other.gameObject.transform.position.z - 1;
                    this.boneAnimation.mLocalTransform.position = expPos;

                    this.boneAnimation.Play("explosion");

                    //移除Script，使爆炸位置固定、換圖正常
                    Destroy(this.GetComponent<MoveController>());

                    //處理不同碰撞物的部分(小怪、魔王、遊戲角色)
                    if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                        other.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.Damage);
                    else if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
                        other.GetComponent<BossPropertyInfo>().DecreaseLife(this.Damage);
                    else if (other.gameObject.layer == LayerMask.NameToLayer("Role"))
                        other.GetComponent<RolePropertyInfo>().DecreaseLife(this.Damage);
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        this.isExplosion = false;

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(ExplosionDestroy);
    }

    /// <summary>
    /// SmoothMove UserTrigger(當播完動畫後刪除自己)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void ExplosionDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //確認已進入爆炸狀態且爆炸死亡動畫後，才可刪除
        if (this.isExplosion && triggerEvent.animationName.CompareTo("explosion") == 0)
            Destroy(this.gameObject);
    }
}
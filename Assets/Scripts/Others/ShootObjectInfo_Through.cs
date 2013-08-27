using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-27
/// Author：Ian
/// Description：
///     遠距離(角色/敵人)發射的物件資訊(穿透性使用)
///     0817新增：魔王資訊的判斷
///     0827新增：依不同需求，分為兩個Script (ShootObjectInfo_Once.cs、ShootObjectInfo_Through.cs)
/// </summary>
public class ShootObjectInfo_Through : MonoBehaviour
{
    public int Damage;                              //造成的傷害值
    public GameDefinition.AttackType AttackType;    //攻擊的類型(物理、魔法)
    public LayerMask ThroughLayer;                  //爆破的對象

    private SmoothMoves.BoneAnimation boneAnimation;

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.collider.gameObject.layer) & this.ThroughLayer.value) > 0)   //判定爆炸的Layer
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

                //創建特效BoneAnimation
                SmoothMoves.BoneAnimation obj = (SmoothMoves.BoneAnimation)Instantiate(GameManager.script.EffectAnimationObject);

                //爆炸中心為Collider交錯點
                Vector3 expPos = other.ClosestPointOnBounds(this.transform.position);
                expPos.z = other.gameObject.transform.position.z - 1;
                obj.mLocalTransform.position = expPos;
                obj.playAutomatically = false;
                //this.boneAnimation.mLocalTransform.position = expPos; 

                if (Random.Range(0, 2) == 0)
                    obj.Play("撞擊特效01");
                else
                    obj.Play("撞擊特效02");


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

    // Use this for initialization
    void Start()
    {
        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
    }
}
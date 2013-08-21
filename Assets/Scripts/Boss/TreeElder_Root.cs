using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-22
/// Author：Ian
/// Description：
///     樹人長老的樹根攻擊
/// </summary>
public class TreeElder_Root : MonoBehaviour
{
    public int Damage;                              //造成的傷害值
    public GameDefinition.AttackType AttackType;    //攻擊的類型(物理、魔法)
    public LayerMask AttackLayer;                   //攻擊的對象

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterColliderTriggerDelegate(RootHit);
        this.boneAnimation.RegisterUserTriggerDelegate(AttackDestroy);
    }

    /// <summary>
    /// 綁在敵人武器上的Collider，觸發攻擊判定(近距離攻擊使用)
    /// </summary>
    /// <param name="triggerEvent">觸發相關資訊</param>
    public void RootHit(SmoothMoves.ColliderTriggerEvent triggerEvent)
    {
        //確認是由"weapon"碰撞的collider
        if (triggerEvent.boneName.Contains("TreeRoot") && triggerEvent.triggerType == SmoothMoves.ColliderTriggerEvent.TRIGGER_TYPE.Enter)
        {
            if (((1 << triggerEvent.otherCollider.gameObject.layer) & this.AttackLayer.value) > 0)
            {
                //tag = MainBody
                if (triggerEvent.otherCollider.tag.CompareTo("MainBody") == 0)
                {
                    triggerEvent.otherCollider.GetComponent<RolePropertyInfo>().DecreaseLife(this.Damage);

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
    /// SmoothMove UserTrigger(當播完動畫後刪除自己)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void AttackDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //確認已進入攻擊狀態且攻擊動畫後，才可刪除
        if (triggerEvent.animationName.CompareTo("小樹根") == 0 || triggerEvent.animationName.CompareTo("大樹根") == 0)
            Destroy(this.gameObject);
    }
}

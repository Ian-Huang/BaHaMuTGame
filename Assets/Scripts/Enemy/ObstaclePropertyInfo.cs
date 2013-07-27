using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-27
/// Modify Date：2013-07-27
/// Author：Ian
/// Description：
///     障礙物的屬性資訊
/// </summary>
public class ObstaclePropertyInfo : MonoBehaviour
{
    public GameDefinition.Obstacle Obstacle;

    public int Damage;   //陷阱傷害值

    public bool isDead { get; private set; }

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isDead = false;

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(DeadDestroy);

        //讀取系統儲存的障礙物屬性資料
        GameDefinition.ObstacleData getData = GameDefinition.ObstacleList.Find((GameDefinition.ObstacleData data) => { return data.ObstacleName == Obstacle; });
        this.Damage = getData.Damage;
    }

    /// <summary>
    /// SmoothMove UserTrigger(當播完死亡動畫後刪除自己)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void DeadDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //確認已進入死亡狀態且撥放死亡動畫後，才可刪除
        if (this.isDead && triggerEvent.animationName.CompareTo("defeat") == 0)
            Destroy(this.gameObject);
    }

    ///// <summary>
    ///// 減少敵人血量函式
    ///// </summary>
    ///// <param name="deLife">減少的數值</param>
    //public void DecreaseLife(int deLife)
    //{
    //    deLife -= this.defence; //扣除防禦力
    //    if (deLife <= 0)
    //        deLife = 0;

    //    this.currentLife -= deLife;

    //    //當生命小於0，刪除物件
    //    if (!this.isDead && this.currentLife <= 0)
    //    {
    //        this.isDead = true;
    //        this.boneAnimation.Play("defeat");
    //        Destroy(this.GetComponent<MoveController>());
    //    }
    //}
}
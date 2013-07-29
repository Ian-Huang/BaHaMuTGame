using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-24
/// Modify Date：2013-07-25
/// Author：Ian
/// Description：
///     敵人的屬性資訊
/// </summary>
public class EnemyPropertyInfo : MonoBehaviour
{
    public GameDefinition.Enemy Enemy;
    public int currentLife; //當前生命值
    public int maxLife;     //最大生命值
    public int cureRate;    //每秒回復生命速率
    public int defence;     //防禦力
    public int nearDamage;  //近距離攻擊傷害值
    public int farDamage;   //遠距離攻擊傷害值

    public bool isDead { get; private set; }

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isDead = false;

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(DeadDestroy);

        //讀取系統儲存的怪物屬性資料
        GameDefinition.EnemyData getData = GameDefinition.EnemyList.Find((GameDefinition.EnemyData data) => { return data.EnemyName == Enemy; });
        this.maxLife = getData.Life;
        this.currentLife = getData.Life;
        this.cureRate = getData.CureRate;
        this.defence = getData.Defence;
        this.nearDamage = getData.NearDamage;
        this.farDamage = getData.FarDamage;

        InvokeRepeating("RestoreLifePersecond", 0.1f, 1);
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

    /// <summary>
    /// 每秒固定回復生命
    /// </summary>
    void RestoreLifePersecond()
    {
        this.currentLife += this.cureRate;
        if (this.currentLife >= this.maxLife)
            this.currentLife = this.maxLife;
    }

    /// <summary>
    /// 減少敵人血量函式
    /// </summary>
    /// <param name="deLife">減少的數值</param>
    public void DecreaseLife(int deLife)
    {
        deLife -= this.defence; //扣除防禦力
        if (deLife <= 0)
            deLife = 0;

        this.currentLife -= deLife;

        //當生命小於0，刪除物件
        if (!this.isDead && this.currentLife <= 0)
        {
            this.isDead = true;
            this.boneAnimation.Play("defeat");
            Destroy(this.GetComponent<MoveController>());   //死亡:停止敵人移動
            CancelInvoke("RestoreLifePersecond");           //死亡:停止敵人回復生命
        }
    }
}
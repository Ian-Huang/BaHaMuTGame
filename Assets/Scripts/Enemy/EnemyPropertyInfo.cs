using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-24
/// Modify Date：2013-08-08
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

    public Material CurrentMaterial;    //設定物件Material (null => 使用預設Material)

    public bool isDead { get; private set; }

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isDead = false;

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(DeadDestroy);
        //如CurrentMaterial有值，則設定當前物件Material
        if (this.CurrentMaterial != null)
        {
            this.boneAnimation.RestoreOriginalMaterials();
            this.boneAnimation.SwapMaterial(this.boneAnimation.mMaterialSource[0], this.CurrentMaterial);
        }

        //讀取系統儲存的怪物屬性資料
        if (this.Enemy != GameDefinition.Enemy.自訂)  //如是"自訂"怪物，則不讀取系統資料
        {
            GameDefinition.EnemyData getData = GameDefinition.EnemyList.Find((GameDefinition.EnemyData data) => { return data.EnemyName == Enemy; });
            this.maxLife = getData.Life;
            this.currentLife = getData.Life;
            this.cureRate = getData.CureRate;
            this.defence = getData.Defence;
            this.nearDamage = getData.NearDamage;
            this.farDamage = getData.FarDamage;
        }

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
            //特定enemy死亡(王怪死亡，連同小兵一起死亡)
            if (this.Enemy == GameDefinition.Enemy.自訂)
                foreach (var script in this.transform.parent.gameObject.GetComponentsInChildren<EnemyPropertyInfo>())
                    script.EnemyDead();
            //一般enemy死亡
            else
                this.EnemyDead();
        }
    }

    public void EnemyDead()
    {
        this.currentLife = 0;
        this.isDead = true;
        this.boneAnimation.Play("defeat");
        Destroy(this.GetComponent<MoveController>());   //死亡:停止敵人移動
        CancelInvoke("RestoreLifePersecond");           //死亡:停止敵人回復生命
    }
}
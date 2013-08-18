using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-16
/// Author：Ian
/// Description：
///     敵人的屬性資訊
///     0809新增：移除敵人的同時，移除註冊於GameManager AllBoneAnimationList中的資訊
///     0816修改：移除敵人回血功能
///     0816：從小怪資訊系統獨立出為王怪資訊系統
///     0818：修改傷害公式(敵人被傷害低於20時，固定造成20點傷害)
/// </summary>
public class EnemyPropertyInfo : MonoBehaviour
{
    public GameDefinition.Enemy Enemy;  //怪物名稱
    public float currentLife;           //當前生命值
    public float maxLife;               //最大生命值
    public int damage;                  //攻擊傷害值
    public int defence;                 //防禦力

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
            this.damage = getData.Damage;
            this.defence = getData.Defence;
        }
    }

    /// <summary>
    /// SmoothMove UserTrigger(當播完死亡動畫後刪除自己)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void DeadDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //確認已進入死亡狀態且撥放死亡動畫後，才可刪除
        if (this.isDead && triggerEvent.animationName.CompareTo("defeat") == 0)
        {
            //移除物件的同時，移除註冊於GameManager AllBoneAnimationList中的資訊
            if (GameManager.script.AllBoneAnimationList.ContainsKey(this.boneAnimation))
                GameManager.script.AllBoneAnimationList.Remove(this.boneAnimation);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 減少敵人血量函式
    /// </summary>
    /// <param name="deLife">減少的數值</param>
    public void DecreaseLife(int deLife)
    {
        deLife -= this.defence; //扣除防禦力
        if (deLife <= 20)
            deLife = 20;

        this.currentLife -= deLife;

        //當生命小於0，刪除物件
        if (!this.isDead && this.currentLife <= 0)
            this.EnemyDead();
    }

    /// <summary>
    /// 怪物死亡執行函式
    /// </summary>
    public void EnemyDead()
    {
        this.currentLife = 0;
        this.isDead = true;
        this.boneAnimation.Play("defeat");
        Destroy(this.GetComponent<MoveController>());   //死亡:停止敵人移動

        //產生金幣物件 (金幣數未完成)
        GameObject newObj = (GameObject)Instantiate(GameManager.script.CoinObject, this.transform.position, GameManager.script.CoinObject.transform.rotation);
        newObj.GetComponent<CoinPropertyInfo>().SetCoinAmount(Random.Range(1, 15));
        newObj.transform.parent = this.transform.parent.transform;
    }
}
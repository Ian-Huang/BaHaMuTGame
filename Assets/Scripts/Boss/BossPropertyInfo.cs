using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-16
/// Author：Ian
/// Description：
///     BOSS的屬性資訊
///     0809新增：移除BOSS的同時，移除註冊於GameManager AllBoneAnimationList中的資訊
///     0816修改：移除BOSS回血功能
///     0816：從小怪資訊系統獨立出為王怪資訊系統
///     0818：修改傷害公式(BOSS被傷害低於20時，固定造成20點傷害)
/// </summary>
public class BossPropertyInfo : MonoBehaviour
{
    public GameDefinition.Boss Boss;    //BOSS名稱
    public float currentLife;           //當前生命值
    public float maxLife;               //最大生命值
    public int nearDamage;              //近距離傷害值
    public int farDamage;               //遠距離傷害值
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

        //讀取系統儲存的BOSS屬性資料
        if (this.Boss != GameDefinition.Boss.自訂)  //如是"自訂"BOSS，則不讀取系統資料
        {
            GameDefinition.BossData getData = GameDefinition.BossList.Find((GameDefinition.BossData data) => { return data.BossName == Boss; });
            this.maxLife = getData.Life;
            this.currentLife = getData.Life;
            this.nearDamage = getData.NearDamage;
            this.farDamage = getData.FarDamage;
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
    /// 減少BOSS血量函式
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
        {
            //王怪死亡，連同小兵一起死亡
            foreach (var script in this.transform.parent.gameObject.GetComponentsInChildren<EnemyPropertyInfo>())
                script.EnemyDead();

            this.BossDead();
        }
    }

    /// <summary>
    /// BOSS死亡執行函式
    /// </summary>
    void BossDead()
    {
        this.currentLife = 0;
        this.isDead = true;
        this.boneAnimation.Play("defeat");
        Destroy(this.GetComponent<MoveController>());   //死亡:停止BOSS移動

        //產生金幣物件 (金幣數未完成)
        GameObject newObj = (GameObject)Instantiate(GameManager.script.CoinObject, this.transform.position, GameManager.script.CoinObject.transform.rotation);
        newObj.GetComponent<CoinPropertyInfo>().SetCoinAmount(Random.Range(1, 15));
        newObj.transform.parent = this.transform.parent.transform;
    }
}
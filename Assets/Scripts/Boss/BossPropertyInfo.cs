using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modify Date：2013-08-27
/// Author：Ian
/// Description：
///     BOSS的屬性資訊
///     0809新增：移除BOSS的同時，移除註冊於GameManager AllBoneAnimationList中的資訊
///     0816修改：移除BOSS回血功能
///     0816：從小怪資訊系統獨立出為王怪資訊系統
///     0818：修改傷害公式(BOSS被傷害低於20時，固定造成20點傷害)
///     0822：新增魔王招式資訊資料
///     0823：新增樹人長老BOSS累積傷害判斷
///     0827：修正BOSS死亡後繼續出怪的Bug
///     0827：修正BOSS在移動過程中死亡會卡住的BUG
/// </summary>
public class BossPropertyInfo : MonoBehaviour
{
    public GameDefinition.Boss Boss;    //BOSS名稱
    public float currentLife;           //當前生命值
    public float maxLife;               //最大生命值
    public int defence;                 //防禦力
    public int coin;                    //掉落錢幣數
    public List<GameDefinition.BossSkillData> skillData;    //BOSS的招式清單

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
        if (this.Boss != GameDefinition.Boss.自訂Boss)  //如是"自訂"BOSS，則不讀取系統資料
        {
            GameDefinition.BossData getData = GameDefinition.BossList.Find((GameDefinition.BossData data) => { return data.BossName == Boss; });
            this.maxLife = getData.Life;
            this.currentLife = getData.Life;
            this.defence = getData.Defence;
            this.coin = getData.Coin;
            this.skillData = getData.SkillData;
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
        else
        {
            //樹人長老BOSS需要紀錄當前累積的傷害
            if (this.Boss == GameDefinition.Boss.樹人長老BOSS)
                BossController_TreeElder.script.addDamageValue += deLife;
        }
    }

    /// <summary>
    /// BOSS死亡執行函式
    /// </summary>
    void BossDead()
    {
        this.currentLife = 0;   //生命歸0
        this.isDead = true;     //切換為死亡狀態

        //Start---停止BOSS所有多執行緒工作
        if (BossController.script)
            BossController.script.StopAllCoroutines();
        else if (BossController_TreeElder.script)
            BossController_TreeElder.script.StopAllCoroutines();
        //End---停止BOSS所有多執行緒工作

        Destroy(this.transform.parent.gameObject.GetComponent<EnemyCreatePoint>()); //刪除EnemyCreatePoint.cs，停止繼續生小怪
        Destroy(this.GetComponent<MoveController>());   //刪除MoveController.cs，停止BOSS移動        
        this.boneAnimation.Play("defeat");              //開始播放死亡動畫
        iTween.Stop(this.gameObject);                   //停止所有ITween影響的動作

        //產生金幣物件
        GameObject newObj = (GameObject)Instantiate(GameManager.script.CoinObject, this.transform.position, GameManager.script.CoinObject.transform.rotation);
        newObj.GetComponent<CoinPropertyInfo>().SetCoinAmount(this.coin);
        newObj.transform.parent = this.transform.parent.transform;
    }
}
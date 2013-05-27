using UnityEngine;
using System.Collections;

/// <summary>
/// 敵人的屬性資訊
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

    public Texture[] DeadChangeTextures;        //攻擊的交換圖組
    public float ChangeTextureTime = 0.1f;             //交換時間間隔

    public bool isDead { get; private set; }
    private int currentTextureIndex { get; set; }
    private float addValue { get; set; }


    // Use this for initialization
    void Start()
    {
        this.isDead = false;
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
            Destroy(this.GetComponent<MoveController>());
            Destroy(this.GetComponent<RegularChangePictures>());
            Destroy(this.GetComponent<EnemyAttackController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isDead)
        {
            if (this.addValue >= this.ChangeTextureTime)
            {
                this.addValue = 0;
                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.DeadChangeTextures.Length)
                {
                    //播完爆炸圖片後，刪除物件
                    if (this.transform.parent.name.Contains("Clone"))
                        Destroy(this.transform.parent.gameObject);
                    else
                        Destroy(this.gameObject);
                    return;
                }
                this.renderer.material.mainTexture = this.DeadChangeTextures[this.currentTextureIndex];
            }
            this.addValue += Time.deltaTime;
        }
    }
}
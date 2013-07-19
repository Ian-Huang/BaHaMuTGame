using UnityEngine;
using System.Collections;
/// <summary>
/// 角色的屬性資訊
/// </summary>
public class RolePropertyInfo : MonoBehaviour
{
    public GameDefinition.Role Role;

    public int currentLife; //當前生命值
    public int maxLife;     //最大生命值
    public int cureRate;    //每秒回復生命速率
    public int defence;     //防禦力
    public int nearDamage;  //近距離攻擊傷害值
    public int farDamage;   //遠距離攻擊傷害值

    public int WeakCureScale = 10;              //Weak狀態 回復速率
    public Texture[] WeakChangeTextures;        //攻擊的交換圖組
    public float ChangeTextureTime = 0.1f;      //交換時間間隔    

    public bool isWeak { get; private set; }
    private int currentTextureIndex { get; set; }

    // Use this for initialization
    void Start()
    {
        GameDefinition.RoleData getData = GameDefinition.RoleList.Find((GameDefinition.RoleData data) => { return data.RoleName == Role; });
        this.maxLife = getData.Life;
        this.currentLife = getData.Life;
        this.cureRate = getData.CureRate;
        this.defence = getData.Defence;
        this.nearDamage = getData.NearDamage;
        this.farDamage = getData.FarDamage;

        this.isWeak = false;
        InvokeRepeating("RestoreLifePersecond", 0.1f, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 減少角色血量函式
    /// </summary>
    /// <param name="deLife">減少的數值</param>
    public void DecreaseLife(int deLife)
    {

        deLife -= this.defence; //扣除防禦力
        if (deLife <= 0)
            deLife = 1;

        if (!this.isWeak)
        {
            //角色當前未虛弱，扣角色的生命值
            this.currentLife -= deLife;
        }
        else
        {
            //角色當前虛弱，扣總士氣(未完成)
            GameManager.script.CurrentMorale -= deLife;
            if (GameManager.script.CurrentMorale <= 0)
                GameManager.script.CurrentMorale = 0;
        }

        //當生命小於0
        if (!this.isWeak)
        {
            if (this.currentLife <= 0)
            {
                this.currentLife = 0;
                switch (this.Role)
                {
                    case GameDefinition.Role.法師:
                    case GameDefinition.Role.獵人:
                        this.GetComponent<FarJobAttackController>().ChangeState(false); //將攻擊的動作暫停                        
                        break;

                    case GameDefinition.Role.狂戰士:
                    case GameDefinition.Role.騎士:
                        this.GetComponent<NearJobAttackController>().ChangeState(false);   //將攻擊的動作暫停                        
                        break;
                }
                this.GetComponent<RegularChangePictures>().ChangeState(false);  //將一般移動的換圖暫停
                this.isWeak = true;
                InvokeRepeating("ChangeWeakTexture", 0.1f, this.ChangeTextureTime);
            }
        }
    }

    void ChangeWeakTexture()
    {
        if ((this.currentTextureIndex + 1) >= this.WeakChangeTextures.Length)       //歸0，循環
            this.currentTextureIndex = 0;
        else
            this.currentTextureIndex++;

        this.renderer.material.mainTexture = this.WeakChangeTextures[this.currentTextureIndex];
    }

    /// <summary>
    /// 每秒固定回復生命
    /// </summary>
    void RestoreLifePersecond()
    {
        if (!this.isWeak)
        {
            this.currentLife += this.cureRate;
            if (this.currentLife >= this.maxLife)
                this.currentLife = this.maxLife;
        }
        else
        {
            this.currentLife += (this.cureRate * this.WeakCureScale);
            if (this.currentLife >= this.maxLife)
            {
                this.currentLife = this.maxLife;

                this.isWeak = false;
                CancelInvoke("ChangeWeakTexture");
                this.currentTextureIndex = 0;
                this.GetComponent<RegularChangePictures>().ChangeState(true);  //將一般移動的換圖恢復運作
                switch (this.Role)
                {
                    case GameDefinition.Role.法師:
                    case GameDefinition.Role.獵人:
                        this.GetComponent<FarJobAttackController>().ChangeState(true); //將攻擊的動作暫停                        
                        break;

                    case GameDefinition.Role.狂戰士:
                    case GameDefinition.Role.騎士:
                        this.GetComponent<NearJobAttackController>().ChangeState(true);   //將攻擊的動作暫停                        
                        break;
                }
            }
        }
    }
}

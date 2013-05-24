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

        InvokeRepeating("RestoreLifePersecond", 0, 1);
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

        this.currentLife -= deLife;

        //當生命小於0
        if (this.currentLife <= 0)
        {
            print(this.name + " 死亡了！");
        }
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
}

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modify Date：2013-08-29
/// Description：
///     遊戲定義檔
///     0816：新增魔王資訊系統
///     0822：新增魔王招式資訊系統
///     0829：新增升級系統部分資訊
/// </summary>
public class GameDefinition
{
    public static int MaxMorale = 5000;
    public static int MoraleRestoreRate = 1;

    public static List<RoleData> RoleList = new List<RoleData>(){ 
        new RoleData(Role.盾騎士,500,100,120,30,10,12),
        new RoleData(Role.狂戰士,550,120,100,35,12,10),
        new RoleData(Role.獵人,450,100,90,25,8,9),
        new RoleData(Role.魔法師,400,120,80,20,9,8)
    };

    public static List<EnemyData> EnemyList = new List<EnemyData>(){ 
        new EnemyData(Enemy.史萊姆,100,150,50,5),
        new EnemyData(Enemy.火焰史萊姆,120,170,60,5),
        new EnemyData(Enemy.硬化史萊姆,150,150,70,10),
        new EnemyData(Enemy.中型史萊姆,250,150,50,10),
        new EnemyData(Enemy.中型火焰史萊姆,300,170,60,15),
        new EnemyData(Enemy.中型硬化史萊姆,400,150,50,25),

        new EnemyData(Enemy.骷髏戰士,150,180,70,15),
        new EnemyData(Enemy.骷髏弓箭手,150,150,70,15),
        new EnemyData(Enemy.骷髏騎士,250,230,120,30),
        new EnemyData(Enemy.骷髏法師,180,200,80,25),

        new EnemyData(Enemy.樹人,200,150,70,20),
        new EnemyData(Enemy.胖樹人,300,200,80,30),
        new EnemyData(Enemy.長爪樹人,400,250,100,35),

        new EnemyData(Enemy.石頭人,250,200,80,20),
    };

    public static List<BossData> BossList = new List<BossData>(){
        new BossData(Boss.巨型史萊姆BOSS,2500,80,300,new List<BossSkillData>(){new BossSkillData("近距離攻擊",180),new BossSkillData("遠距離攻擊",150)}),
        new BossData(Boss.石巨人BOSS,4000,120,500,new List<BossSkillData>(){new BossSkillData("近距離攻擊",200),new BossSkillData("遠距離攻擊",180)}),
        new BossData(Boss.樹人長老BOSS,6000,150,1000,new List<BossSkillData>(){new BossSkillData("小樹根攻擊",300),new BossSkillData("大樹根攻擊",700)}),
    };

    public static List<ObstacleData> ObstacleList = new List<ObstacleData>(){ 
        new ObstacleData(Obstacle.鐵陷阱,200),
        new ObstacleData(Obstacle.火焰魔法陣,250),
        new ObstacleData(Obstacle.樹木_01,200),
        new ObstacleData(Obstacle.樹木_02,200),
        new ObstacleData(Obstacle.斷壁,250)        
    };

    //能力(生命、攻擊力、防禦力)升級花費表
    public static List<int> AbilityCostLevel = new List<int>() { 100, 200, 350, 700, 1200, 1800, 2500, 3500, 4800, 6500 };
    //攻擊等級升級花費表
    public static List<int> AttackLVCostLevel = new List<int>() { 500, 800, 1200, 1800, 2500 };
    //絕技升級花費表
    public static List<int> UltimateSkillCostLevel = new List<int>() { 2000, 4500 };

    public class RoleData
    {
        public Role RoleName;
        public int Life;
        public int Damage;
        public int Defence;
        public int LifeAdd;
        public int DamageAdd;
        public int DefenceAdd;

        /// <summary>
        /// 角色資料建構式
        /// </summary>
        /// <param name="name">角色名字</param>
        /// <param name="life">角色生命值</param>
        /// <param name="damage">角色傷害值</param>
        /// <param name="defence">角色防禦力</param>
        /// <param name="lifeAdd">角色生命增加值(升級用)</param>
        /// <param name="damageAdd">角色傷害增加值(升級用)</param>
        /// <param name="defenceAdd">角色防禦增加值(升級用)</param>
        public RoleData(Role name, int life, int damage, int defence, int lifeAdd, int damageAdd, int defenceAdd)
        {
            this.RoleName = name;
            this.Life = life;
            this.Damage = damage;
            this.Defence = defence;
            this.LifeAdd = lifeAdd;
            this.DamageAdd = damageAdd;
            this.DefenceAdd = defenceAdd;
        }

        /// <summary>
        /// 更新角色能力值
        /// </summary>
        /// <param name="life">角色生命值</param>
        /// <param name="damage">角色傷害值</param>
        /// <param name="defence">角色防禦力</param>
        public void UpdateAbilityValue(int life, int damage, int defence)
        {
            this.Life = life;
            this.Damage = damage;
            this.Defence = defence;
        }
    }

    public class EnemyData
    {
        public Enemy EnemyName;
        public int Life;
        public int Damage;
        public int Defence;
        public int Coin;

        /// <summary>
        /// 敵人資料建構式
        /// </summary>
        /// <param name="name">敵人名字</param>
        /// <param name="life">敵人生命值</param>
        /// <param name="damage">敵人傷害值</param>
        /// <param name="defence">敵人防禦力</param>
        /// <param name="coin">敵人掉落金錢數</param>
        public EnemyData(Enemy name, int life, int damage, int defence, int coin)
        {
            this.EnemyName = name;
            this.Life = life;
            this.Damage = damage;
            this.Defence = defence;
            this.Coin = coin;
        }
    }

    [System.Serializable]
    public class BossSkillData
    {
        public string SkillName;
        public int Damage;

        /// <summary>
        /// BOSS招式資料
        /// </summary>
        /// <param name="name">BOSS招式名字</param>
        /// <param name="damage">BOSS招式傷害值</param>
        public BossSkillData(string name, int damage)
        {
            this.SkillName = name;
            this.Damage = damage;
        }
    }

    public class BossData
    {
        public Boss BossName;
        public int Life;
        public int Defence;
        public int Coin;
        public List<BossSkillData> SkillData;

        /// <summary>
        /// BOSS資料建構式
        /// </summary>
        /// <param name="name">BOSS名字</param>
        /// <param name="life">BOSS生命值</param>
        /// <param name="defence">BOSS防禦力</param>
        /// <param name="coin">BOSS掉落金錢數</param>
        /// <param name="skillData">BOSS招式清單</param>
        public BossData(Boss name, int life, int defence, int coin, List<BossSkillData> skillData)
        {
            this.BossName = name;
            this.Life = life;
            this.Defence = defence;
            this.Coin = coin;
            this.SkillData = skillData;
        }
    }

    public class ObstacleData
    {
        public Obstacle ObstacleName;
        public int Damage;

        public int AddCureLifeRate;
        public int AddCureeLifeRateTime;
        public int AttackSpeed;
        public int AttackSpeedTime;

        /// <summary>
        /// 障礙物資料建構式
        /// </summary>
        /// <param name="name">障礙物名字</param>
        /// <param name="damage">障礙物傷害值</param>
        public ObstacleData(Obstacle name, int damage)
        {
            this.ObstacleName = name;
            this.Damage = damage;
        }
    }

    public enum AttackMode
    { NearAttack = 0, FarAttck }

    public enum AttackType
    { PhysicsAttack = 0, MagicAttack }

    public enum Role
    {
        狂戰士 = 0,
        獵人 = 1,
        盾騎士 = 2,
        魔法師 = 3
    }

    public enum Enemy
    {
        自訂 = 0,     //給我們自訂怪物能力值使用

        史萊姆 = 101,
        火焰史萊姆 = 102,
        硬化史萊姆 = 103,
        中型史萊姆 = 104,
        中型火焰史萊姆 = 105,
        中型硬化史萊姆 = 106,

        骷髏戰士 = 201,
        骷髏弓箭手 = 202,
        骷髏騎士 = 203,
        骷髏法師 = 204,

        樹人 = 301,
        胖樹人 = 302,
        長爪樹人 = 303,

        石頭人 = 401,

        獸人戰士 = 501,
        獸人弓箭手 = 502,
        獸人法師 = 503
    }

    public enum Boss
    {
        自訂Boss = 0,     //給我們自訂怪物能力值使用

        巨型史萊姆BOSS = 1,
        石巨人BOSS = 2,
        樹人長老BOSS = 3,
    }

    public enum Obstacle
    {
        聖女石碑 = 101,              //盾騎士處理
        鐵陷阱 = 201,                //陷阱處理
        斷壁 = 301,                  //盾騎士、狂戰士處理           
        火焰魔法陣 = 401,            //法師處理
        樹木_01 = 501, 樹木_02 = 502 //盾騎士、狂戰士處理
    }

    public enum ChangeRoleMode
    {
        OneAndTwo, TwoAndThree, ThreeAndFour
    }

    public enum EventTriggerType
    {
        出怪點 = 0, 魔王警告點 = 1, 有魔王終點 = 2, 無魔王終點 = 3
    }


    /// <summary>
    /// 暫時記錄區
    /// 
    /// *****Buff效果*****
    /// 1.Buff物品觸發方式：
    /// 正確觸發-> 角色播放觸發動作、Buff物品播放(correct animation)
    /// 錯誤觸發->角色無反應、Buff無反應(fail animation)
    /// 
    /// 2.增益內容
    /// 全體回復一定Hp、 一定時間(個體/全體)回復量+X
    /// 
    /// ※(全體回復一定Hp 寫法暫存)
    /// RolesCollection gameobject find RolePropertyInfo.cs  currentLife + cureValue
    /// 
    /// ※一定時間(個體/全體)回復量+X
    /// RolesCollection gameobject find RolePropertyInfo.cs  cureRate + addValue
    /// 
    /// 3.減益內容
    /// 全體攻速下降、一定時間(個體/全體)回復量-X
    /// 
    /// ※全體攻速下降：新增指令
    ///     this.boneAnimation["attack"].speed = newSpeed;
    ///     
    /// ※一定時間(個體/全體)回復量-X
    /// RolesCollection gameobject find RolePropertyInfo.cs  cureRate - deValue    
    /// 
    /// ※BuffData 預定會有的參數
    /// BuffName 狀態名稱
    /// Damage 傷害值 (假如要跟陷阱系統合併...)  (傷害、回復生命可以合併)
    /// CureHp  回復生命
    /// CureRateHp  回復生命速率(增加/減少)值
    /// CureRateTime    (增加/減少)持續時間
    /// AttackSpeed     攻速(增加/減少)
    /// AttackSpeedTime 攻速(增加/減少)持續時間
    /// 
    /// *****Buff效果*****
    /// </summary>

}
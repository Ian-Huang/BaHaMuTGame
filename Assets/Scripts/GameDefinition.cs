using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 遊戲定義檔
/// </summary>
public class GameDefinition
{
    public static int MaxMorale = 10000;
    public static int MoraleRestoreRate = 5;

    public static List<RoleData> RoleList = new List<RoleData>(){ 
        new RoleData(Role.狂戰士,500,10,50,130,0),
        new RoleData(Role.盾騎士,500,10,80,80,0),
        new RoleData(Role.法師,300,6,30,100,100),
        new RoleData(Role.獵人,350,8,40,60,130)
    };

    public static List<EnemyData> EnemyList = new List<EnemyData>(){ 
        new EnemyData(Enemy.史萊姆,50,5,10,100,0),
        new EnemyData(Enemy.火焰史萊姆,100,10,20,150,0),
        new EnemyData(Enemy.硬化史萊姆,200,15,50,80,0),
        new EnemyData(Enemy.中型史萊姆,500,30,10,100,0),
        new EnemyData(Enemy.中型火焰史萊姆,700,30,20,150,0),
        new EnemyData(Enemy.中型硬化史萊姆,1000,20,50,80,0),
        new EnemyData(Enemy.巨型史萊姆BOSS,2500,30,30,200,200),

        new EnemyData(Enemy.骷髏戰士,100,10,20,100,0),
        new EnemyData(Enemy.骷髏弓箭手,80,10,20,0,120),
        new EnemyData(Enemy.骷髏騎士,300,20,50,100,0),
        new EnemyData(Enemy.骷髏法師,100,5,10,0,150),

        new EnemyData(Enemy.樹人,200,10,35,150,0),
        new EnemyData(Enemy.胖樹人,300,15,45,200,0),
        new EnemyData(Enemy.長爪樹人,200,15,30,100,0),

        new EnemyData(Enemy.石頭人,300,10,50,200,0),
        new EnemyData(Enemy.石巨人BOSS,2500,30,30,400,100),
        new EnemyData(Enemy.劍石巨人BOSS,2500,30,30,400,100),

        new EnemyData(Enemy.獸人戰士,150,15,25,120,0),
        new EnemyData(Enemy.獸人弓箭手,100,10,25,0,140),
        new EnemyData(Enemy.獸人法師,100,5,15,0,170),
    };

    public static List<ObstacleData> ObstacleList = new List<ObstacleData>(){ 
        new ObstacleData(Obstacle.火焰魔法陣,250),
        new ObstacleData(Obstacle.樹木_01,200),
        new ObstacleData(Obstacle.樹木_02,200),
        new ObstacleData(Obstacle.斷壁,250),
        new ObstacleData(Obstacle.鐵陷阱,200)
    };

    public class RoleData
    {
        public Role RoleName;
        public int Life;
        public int CureRate;
        public int Defence;
        public int NearDamage;
        public int FarDamage;

        /// <summary>
        /// 角色資料建構式
        /// </summary>
        /// <param name="name">角色名字</param>
        /// <param name="life">角色生命值</param>
        /// <param name="cureRate">角色回復速率</param>
        /// <param name="defence">角色防禦力</param>
        /// <param name="nearDamage">角色近距離傷害值</param>
        /// <param name="farDamage">角色遠距離傷害值</param>
        public RoleData(Role name, int life, int cureRate, int defence, int nearDamage, int farDamage)
        {
            this.RoleName = name;
            this.Life = life;
            this.CureRate = cureRate;
            this.Defence = defence;
            this.NearDamage = nearDamage;
            this.FarDamage = farDamage;
        }
    }

    public class EnemyData
    {
        public Enemy EnemyName;
        public int Life;
        public int CureRate;
        public int Defence;
        public int NearDamage;
        public int FarDamage;

        /// <summary>
        /// 敵人資料建構式
        /// </summary>
        /// <param name="name">敵人名字</param>
        /// <param name="life">敵人生命值</param>
        /// <param name="cureRate">敵人回復速率</param>
        /// <param name="defence">敵人防禦力</param>
        /// <param name="nearDamage">敵人近距離傷害值</param>
        /// <param name="farDamage">敵人遠距離傷害值</param>
        public EnemyData(Enemy name, int life, int cureRate, int defence, int nearDamage, int farDamage)
        {
            this.EnemyName = name;
            this.Life = life;
            this.CureRate = cureRate;
            this.Defence = defence;
            this.NearDamage = nearDamage;
            this.FarDamage = farDamage;
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
        法師 = 3
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
        巨型史萊姆BOSS = 199,

        骷髏戰士 = 201,
        骷髏弓箭手 = 202,
        骷髏騎士 = 203,
        骷髏法師 = 204,

        樹人 = 301,
        胖樹人 = 302,
        長爪樹人 = 303,

        石頭人 = 401,
        石巨人BOSS = 498,
        劍石巨人BOSS = 499,

        獸人戰士 = 501,
        獸人弓箭手 = 502,
        獸人法師 = 503
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
        出怪點 = 0, 魔王警告點 = 1, 終點 = 2
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
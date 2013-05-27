using System.Collections;
using System.Collections.Generic;

public class GameDefinition
{
    public static List<RoleData> RoleList = new List<RoleData>(){ 
        new RoleData(Role.BSK,500,10,50,130,0),
        new RoleData(Role.Knight,500,10,80,80,0),
        new RoleData(Role.Witch,300,6,30,100,100),
        new RoleData(Role.Hunter,350,8,40,60,130)
    };

    public static List<EnemyData> EnemyList = new List<EnemyData>(){ 
        new EnemyData(Enemy.史萊姆,50,5,10,100,0),
        new EnemyData(Enemy.火焰史萊姆,100,10,20,150,0),
        new EnemyData(Enemy.硬化史萊姆,200,15,50,80,0),
        new EnemyData(Enemy.中型史萊姆,500,30,10,100,0),
        new EnemyData(Enemy.中型火焰史萊姆,700,30,20,150,0),
        new EnemyData(Enemy.中型硬化史萊姆,1000,20,50,80,0),

        new EnemyData(Enemy.骷髏戰士,100,10,20,100,0),
        new EnemyData(Enemy.骷髏弓箭手,80,10,20,0,120),
        new EnemyData(Enemy.骷髏騎士,300,20,50,100,0),
        new EnemyData(Enemy.骷髏法師,100,5,10,0,150),

        new EnemyData(Enemy.獸人戰士,150,15,25,120,0),
        new EnemyData(Enemy.獸人弓箭手,100,10,25,0,140),
        new EnemyData(Enemy.獸人法師,100,5,15,0,170),

        new EnemyData(Enemy.石巨人,300,10,50,200,0),
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

    public class BossData
    {
        public int Life;
        public int CureRate;
        public int Defence;
        //
    }

    public enum AttackMode
    { NearAttack = 0, FarAttck }

    public enum AttackType
    { PhysicsAttack = 0, MagicAttack }

    public enum Role
    {
        BSK = 0,
        Hunter = 1,
        Knight = 2,
        Witch = 3
    }

    public enum Enemy
    {
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

        獸人戰士 = 301,
        獸人弓箭手 = 302,
        獸人法師 = 303,

        石巨人 = 401
    }

    public enum ChangeRoleMode
    {
        OneAndTwo, TwoAndThree, ThreeAndFour
    }

    //物件Layer 數值必須跟 Game Layer相同
    public enum GameLayout
    {
        Enemy = 8,
        Role = 9,
        Obstacle = 10
    }

    public const float ShootObject_ZIndex = 11;
}
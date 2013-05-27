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
        new EnemyData(Enemy.�v�ܩi,50,5,10,100,0),
        new EnemyData(Enemy.���K�v�ܩi,100,10,20,150,0),
        new EnemyData(Enemy.�w�ƥv�ܩi,200,15,50,80,0),
        new EnemyData(Enemy.�����v�ܩi,500,30,10,100,0),
        new EnemyData(Enemy.�������K�v�ܩi,700,30,20,150,0),
        new EnemyData(Enemy.�����w�ƥv�ܩi,1000,20,50,80,0),

        new EnemyData(Enemy.�u�\�Ԥh,100,10,20,100,0),
        new EnemyData(Enemy.�u�\�}�b��,80,10,20,0,120),
        new EnemyData(Enemy.�u�\�M�h,300,20,50,100,0),
        new EnemyData(Enemy.�u�\�k�v,100,5,10,0,150),

        new EnemyData(Enemy.�~�H�Ԥh,150,15,25,120,0),
        new EnemyData(Enemy.�~�H�}�b��,100,10,25,0,140),
        new EnemyData(Enemy.�~�H�k�v,100,5,15,0,170),

        new EnemyData(Enemy.�ۥ��H,300,10,50,200,0),
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
        /// �����ƫغc��
        /// </summary>
        /// <param name="name">����W�r</param>
        /// <param name="life">����ͩR��</param>
        /// <param name="cureRate">����^�_�t�v</param>
        /// <param name="defence">���⨾�m�O</param>
        /// <param name="nearDamage">�����Z���ˮ`��</param>
        /// <param name="farDamage">���⻷�Z���ˮ`��</param>
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
        /// �ĤH��ƫغc��
        /// </summary>
        /// <param name="name">�ĤH�W�r</param>
        /// <param name="life">�ĤH�ͩR��</param>
        /// <param name="cureRate">�ĤH�^�_�t�v</param>
        /// <param name="defence">�ĤH���m�O</param>
        /// <param name="nearDamage">�ĤH��Z���ˮ`��</param>
        /// <param name="farDamage">�ĤH���Z���ˮ`��</param>
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
        �v�ܩi = 101,
        ���K�v�ܩi = 102,
        �w�ƥv�ܩi = 103,
        �����v�ܩi = 104,
        �������K�v�ܩi = 105,
        �����w�ƥv�ܩi = 106,

        �u�\�Ԥh = 201,
        �u�\�}�b�� = 202,
        �u�\�M�h = 203,
        �u�\�k�v = 204,

        �~�H�Ԥh = 301,
        �~�H�}�b�� = 302,
        �~�H�k�v = 303,

        �ۥ��H = 401
    }

    public enum ChangeRoleMode
    {
        OneAndTwo, TwoAndThree, ThreeAndFour
    }

    //����Layer �ƭȥ����� Game Layer�ۦP
    public enum GameLayout
    {
        Enemy = 8,
        Role = 9,
        Obstacle = 10
    }

    public const float ShootObject_ZIndex = 11;
}
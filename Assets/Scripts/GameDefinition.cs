using System.Collections;

public class GameDefinition
{
    public class RoleData
    {
        public int Life;
        public int CureRate;
        public int Defence;
        public int NearAttack;
        public int FarAttack;
    }

    public class EnemyData
    {
        public int Life;
        public int CureRate;
        public int Defence;
        public int NearAttack;
        public int FarAttack;
    }

    public class BossData
    {
        public int Life;
        public int CureRate;
        public int Defence;
        //
    }

    public enum AttackMode
    { Near, Far }

    public enum Role
    { BSK, Hunter, Knight, Witch }    

    public const int Enemy_Layer = 8;
    public const int Role_Layer = 9;
    public const int Obstacle = 10;

    public const float ShootObject_ZIndex = 11;
}
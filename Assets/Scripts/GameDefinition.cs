using System.Collections;

public class GameDefinition
{
    public enum AttackMode
    { Near, Far }

    public enum Role
    { BSK, Hunter, Knight, Witch }    

    public const int Enemy_Layer = 8;
    public const int Role_Layer = 9;
    public const int Obstacle = 10;

    public const float ShootObject_ZIndex = 11;
}
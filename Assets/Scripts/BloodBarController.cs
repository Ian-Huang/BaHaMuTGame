using UnityEngine;
using System.Collections;

public class BloodBarController : MonoBehaviour
{
    public enum WhichBlood
    { Role = 0, Enemy = 1 }
    public WhichBlood whichType;

    private RolePropertyInfo RolePropertyInfo_Script;
    private EnemyPropertyInfo EnemyPropertyInfo_Script;

    // Use this for initialization
    void Start()
    {
        switch (this.whichType)
        {
            case WhichBlood.Role:
                RolePropertyInfo_Script = this.transform.parent.parent.GetComponent<RolePropertyInfo>();
                break;
            case WhichBlood.Enemy:
                EnemyPropertyInfo_Script = this.transform.parent.parent.GetComponent<EnemyPropertyInfo>();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.whichType)
        {
            case WhichBlood.Role:
                this.renderer.material.mainTextureOffset = new Vector2(Mathf.Lerp(1, 0, (float)this.RolePropertyInfo_Script.currentLife / this.RolePropertyInfo_Script.maxLife), 0);
                break;
            case WhichBlood.Enemy:
                this.renderer.material.mainTextureOffset = new Vector2(Mathf.Lerp(1, 0, (float)this.EnemyPropertyInfo_Script.currentLife / this.EnemyPropertyInfo_Script.maxLife), 0);
                break;
        }
    }
}
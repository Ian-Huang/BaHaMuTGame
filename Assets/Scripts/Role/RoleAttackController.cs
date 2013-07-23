using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-23
/// Author：Ian
/// Description：
///     角色攻擊控制器(未完成)
/// </summary>
public class RoleAttackController : MonoBehaviour
{
    public bool isAction = true;
    public float AttackDistance;

    public LayerMask AttackLayer;

    private RolePropertyInfo roleInfo { get; set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        //畫出偵測線
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.AttackDistance);
    }
}

using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-23
/// Author�GIan
/// Description�G
///     ����������(������)
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
        //�e�X�����u
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.AttackDistance);
    }
}

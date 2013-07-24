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
    public float AttackDistance;        //�����Z��

    public LayerMask AttackLayer;       //�����P�w��Layer

    private RolePropertyInfo roleInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;
    private RaycastHit hitData;

    // Use this for initialization
    void Start()
    {
        //���J�����T
        this.roleInfo = this.GetComponent<RolePropertyInfo>();

        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterColliderTriggerDelegate(WeaponHit);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.roleInfo.isWeak)
        {
            if (Physics.Raycast(this.transform.position, Vector3.right, out this.hitData, this.AttackDistance, this.AttackLayer))
            {
                if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                    if (!this.hitData.collider.GetComponent<EnemyPropertyInfo>().isDead)
                        if (!this.boneAnimation.IsPlaying("attack"))
                        {
                            this.boneAnimation.Play("attack");
                        }
            }
            else
            {
                if (!this.boneAnimation.IsPlaying("walk"))
                    this.boneAnimation.Play("walk");
            }
        }
    }

    /// <summary>
    /// �j�b�}�⨭�W��Collider�AĲ�o�����P�w
    /// </summary>
    /// <param name="triggerEvent">Ĳ�o������T</param>
    public void WeaponHit(SmoothMoves.ColliderTriggerEvent triggerEvent)
    {
        if (triggerEvent.boneName == "weapon" && triggerEvent.triggerType == SmoothMoves.ColliderTriggerEvent.TRIGGER_TYPE.Enter)
        {
            if (((1 << triggerEvent.otherCollider.gameObject.layer) & this.AttackLayer.value) > 0)
            {
                //tag = MainBody
                if (triggerEvent.otherCollider.tag.CompareTo("MainBody") == 0)
                    triggerEvent.otherCollider.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.roleInfo.nearDamage);
            }
        }
    }

    void OnDrawGizmos()
    {
        //�e�X�����u
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.AttackDistance);
    }
}

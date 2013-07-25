using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-25
/// Author�GIan
/// Description�G
///     �ĤH�������
/// </summary>
public class EnemyAttackController : MonoBehaviour
{
    public float AttackDistance;        //�����Z��
    public GameObject ShootObject;      //���Z�������o�g�X������
    public LayerMask AttackLayer;       //�����P�w��Layer

    private EnemyPropertyInfo enemyInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;
    private RaycastHit hitData;

    // Use this for initialization
    void Start()
    {
        //���J�ĤH��T
        this.enemyInfo = this.GetComponent<EnemyPropertyInfo>();

        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterColliderTriggerDelegate(WeaponHit);
        this.boneAnimation.RegisterUserTriggerDelegate(ShootEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.enemyInfo.isDead)
        {
            if (Physics.Raycast(this.transform.position, Vector3.left, out this.hitData, this.AttackDistance, this.AttackLayer))
            {
                //tag = MainBody
                if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                    if (!this.boneAnimation.IsPlaying("attack"))
                        this.boneAnimation.Play("attack");
            }
            else
            {
                if (!this.boneAnimation.IsPlaying("walk"))
                    this.boneAnimation.Play("walk");
            }
        }
    }

    /// <summary>
    /// �j�b�ĤH�Z���W��Collider�AĲ�o�����P�w(��Z�������ϥ�)
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
                    triggerEvent.otherCollider.GetComponent<RolePropertyInfo>().DecreaseLife(this.enemyInfo.nearDamage);
            }
        }
    }

    /// <summary>
    /// �j�b�ĤH�Z���W��UserTrigger�AĲ�o�����P�w(���Z�������ϥ�)
    /// </summary>
    /// <param name="triggerEvent">Ĳ�o������T</param>
    public void ShootEvent(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        if (triggerEvent.boneName == "weapon")
        {
            GameObject obj = (GameObject)Instantiate(this.ShootObject, this.transform.position - new Vector3(0, 0, 0.1f), this.ShootObject.transform.rotation);

            ShootObjectInfo info = obj.GetComponent<ShootObjectInfo>();
            info.Damage = this.enemyInfo.farDamage;
        }
    }

    void OnDrawGizmos()
    {
        //�e�X�����u
        Gizmos.color = Color.black;
        Gizmos.DrawRay(this.transform.position, Vector3.left * this.AttackDistance);
    }
}
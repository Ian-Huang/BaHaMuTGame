using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-23
/// Modify Date�G2013-07-31
/// Author�GIan
/// Description�G
///     ���������� (�ĤH�B��ê��)
/// </summary>
public class RoleAttackController : MonoBehaviour
{
    public float AttackDistance;        //�����Z��
    public GameObject ShootObject;      //���Z�������o�g�X������
    public LayerMask EnemyLayer;       //�P�w�O�_�������ĤHLayer
    public LayerMask ObstacleLayer;       //�P�w�O�_��������ê��Layer

    private RolePropertyInfo roleInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;
    private RaycastHit hitData;

    // Use this for initialization
    void Start()
    {
        //���J�����T
        this.roleInfo = this.GetComponent<RolePropertyInfo>();

        //�]�wBoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterColliderTriggerDelegate(WeaponHit);
        this.boneAnimation.RegisterUserTriggerDelegate(ShootEvent);
    }

    // Update is called once per frame
    void Update()
    {
        // ���⥲������z
        if (!this.roleInfo.isWeak)
        {
            //�P�O���󬰦�H  �ĤH�P��ê�������P���B�z
            if (Physics.Raycast(this.transform.position, Vector3.right, out this.hitData, this.AttackDistance, this.EnemyLayer))
            {
                //tag = MainBody  (����D��)
                if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                    if (!this.hitData.collider.GetComponent<EnemyPropertyInfo>().isDead)    //�T�{Enemy�O�_�w�g���`
                        if (!this.boneAnimation.IsPlaying("attack"))
                            this.boneAnimation.Play("attack");
            }
            else if (Physics.Raycast(this.transform.position, Vector3.right, out this.hitData, this.AttackDistance, this.ObstacleLayer))
            {
                //tag = MainBody  (����D��)
                if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                    if (!this.hitData.collider.GetComponent<ObstaclePropertyInfo>().isDisappear)    //�T�{Obstacle�O�_�w�g����
                        if (this.GetComponent<ObstacleSystem>().ObstacleList.Contains(this.hitData.collider.GetComponent<ObstaclePropertyInfo>().Obstacle))
                            if (!this.boneAnimation.IsPlaying("attack"))
                                this.boneAnimation.Play("attack");
            }
            else
            {
                //�T�{�ثe�ʵe���A(�����S�A��attack)
                if (!this.boneAnimation.IsPlaying("attack"))
                {
                    //�P�w�I���O�_���b�ʡA�H���M�w���⪺�ʧ@���A
                    if (BackgroundController.script.isRunning)
                        this.boneAnimation.Play("walk");
                    else
                        this.boneAnimation.Play("idle");
                }
            }
        }
    }

    /// <summary>
    /// �j�b����Z���W��Collider�AĲ�o�����P�w(��Z�������ϥ�)
    /// </summary>
    /// <param name="triggerEvent">Ĳ�o������T</param>
    public void WeaponHit(SmoothMoves.ColliderTriggerEvent triggerEvent)
    {
        //�T�{�O��"weapon"�I����collider
        if (triggerEvent.boneName == "weapon" && triggerEvent.triggerType == SmoothMoves.ColliderTriggerEvent.TRIGGER_TYPE.Enter)
        {
            //tag = MainBody  (����D��)
            if (triggerEvent.otherCollider.tag.CompareTo("MainBody") == 0)
            {
                //�P�O���󬰦�H  �ĤH�P��ê�������P���B�z
                if (((1 << triggerEvent.otherCollider.gameObject.layer) & this.EnemyLayer.value) > 0)
                    triggerEvent.otherCollider.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.roleInfo.nearDamage);

                else if (((1 << triggerEvent.otherCollider.gameObject.layer) & this.ObstacleLayer.value) > 0)
                    triggerEvent.otherCollider.GetComponent<ObstaclePropertyInfo>().CheckObstacle(true);
            }
        }
    }

    /// <summary>
    /// �j�b����Z���W��UserTrigger�AĲ�o�����P�w(���Z�������ϥ�)
    /// </summary>
    /// <param name="triggerEvent">Ĳ�o������T</param>
    public void ShootEvent(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //�T�{�O��"weapon"Ĳ�o��UserTrigger
        if (triggerEvent.boneName == "weapon")
        {
            //���ͮg������
            GameObject obj = (GameObject)Instantiate(this.ShootObject, this.transform.position - new Vector3(0, 0, 0.1f), this.ShootObject.transform.rotation);

            //�]�w����parent �B layer �B Damage
            obj.layer = LayerMask.NameToLayer("ShootObject");
            obj.transform.parent = GameObject.Find("UselessObjectCollection").transform;

            ShootObjectInfo info = obj.GetComponent<ShootObjectInfo>();
            info.Damage = this.roleInfo.farDamage;
        }
    }

    void OnDrawGizmos()
    {
        //�e�X�����u
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.AttackDistance);
    }
}

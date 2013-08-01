using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-23
/// Modify Date�G2013-08-01
/// Author�GIan
/// Description�G
///     �ĤH�������
/// </summary>
public class EnemyAttackController : MonoBehaviour
{
    public float AttackDistance;        //�����Z��
    public GameObject ShootObject;      //���Z�������o�g�X������
    public SmoothMoves.BoneAnimation EffectAnimation;   //�ĪG�ʵe����
    public LayerMask AttackLayer;       //�����P�w��Layer

    private EnemyPropertyInfo enemyInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;
    private RaycastHit hitData;

    // Use this for initialization
    void Start()
    {
        //���J�ĤH��T
        this.enemyInfo = this.GetComponent<EnemyPropertyInfo>();

        //�]�wBoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterColliderTriggerDelegate(WeaponHit);
        this.boneAnimation.RegisterUserTriggerDelegate(ShootEvent);
    }

    // Update is called once per frame
    void Update()
    {
        // �Ǫ����������`
        if (!this.enemyInfo.isDead)
        {
            if (Physics.Raycast(this.transform.position, Vector3.left, out this.hitData, this.AttackDistance, this.AttackLayer))
            {
                //tag = MainBody
                if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                    if (!this.boneAnimation.IsPlaying("attack"))
                    {
                        this.boneAnimation.Play("attack");
                        this.GetComponent<MoveController>().isRunning = false;
                    }
            }
            else
            {
                //�T�{�ثe�ʵe���A(�����S�A��attack)
                if (!this.boneAnimation.IsPlaying("attack"))
                {
                    this.boneAnimation.Play("walk");
                    this.GetComponent<MoveController>().isRunning = true;
                }
            }
        }
    }

    /// <summary>
    /// �j�b�ĤH�Z���W��Collider�AĲ�o�����P�w(��Z�������ϥ�)
    /// </summary>
    /// <param name="triggerEvent">Ĳ�o������T</param>
    public void WeaponHit(SmoothMoves.ColliderTriggerEvent triggerEvent)
    {
        //�T�{�O��"weapon"�I����collider
        if (triggerEvent.boneName == "weapon" && triggerEvent.triggerType == SmoothMoves.ColliderTriggerEvent.TRIGGER_TYPE.Enter)
        {
            if (((1 << triggerEvent.otherCollider.gameObject.layer) & this.AttackLayer.value) > 0)
            {
                //tag = MainBody
                if (triggerEvent.otherCollider.tag.CompareTo("MainBody") == 0)
                {
                    triggerEvent.otherCollider.GetComponent<RolePropertyInfo>().DecreaseLife(this.enemyInfo.nearDamage);

                    //�Ы� �����S��BoneAnimation
                    SmoothMoves.BoneAnimation obj = (SmoothMoves.BoneAnimation)Instantiate(this.EffectAnimation);
                    obj.mLocalTransform.position = triggerEvent.otherColliderClosestPointToBone - new Vector3(0, 0, 0.2f);
                    obj.playAutomatically = false;
                    //�H������ 1 �� 2 �ʵe���q
                    if (Random.Range(0, 2) == 0)
                        obj.Play("�����S��01");
                    else
                        obj.Play("�����S��02");
                }
            }
        }
    }

    /// <summary>
    /// �j�b�ĤH�Z���W��UserTrigger�AĲ�o�����P�w(���Z�������ϥ�)
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
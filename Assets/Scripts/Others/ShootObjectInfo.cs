using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-25
/// Modify Date�G2013-08-05
/// Author�GIan
/// Description�G
///     ���Z��(����/�ĤH)�o�g�������T
/// </summary>
public class ShootObjectInfo : MonoBehaviour
{
    public int Damage;                              //�y�����ˮ`��
    public GameDefinition.AttackType AttackType;    //����������(���z�B�]�k)
    public LayerMask ExplosiveLayer;                //�z�}����H

    private bool isExplosion { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;

    void OnTriggerEnter(Collider other)
    {
        if (!this.isExplosion)
        {
            if (((1 << other.collider.gameObject.layer) & this.ExplosiveLayer.value) > 0)   //�P�w�z����Layer
            {
                if (other.collider.tag.CompareTo("MainBody") == 0)
                {
                    //�p��H���ĤH�A�ݦA�ˬd�ĤH�����O�_�w�g���`(EnemyPropertyInfo.isDead)
                    if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                        if (other.GetComponent<EnemyPropertyInfo>().isDead)
                            return;

                    //�H�U�����z���ƥ�
                    this.isExplosion = true;
                    this.boneAnimation.mLocalTransform.position = other.ClosestPointOnBounds(this.transform.position) - new Vector3(0, 0, 1);   //�z�����߬�Collider����I
                    this.boneAnimation.Play("explosion");

                    //����Script�A���z����m�T�w�B���ϥ��`
                    Destroy(this.GetComponent<MoveController>());

                    //�B�z���P�I����������(�ĤH�B�D��)
                    if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                        other.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.Damage);
                    else if (other.gameObject.layer == LayerMask.NameToLayer("Role"))
                        other.GetComponent<RolePropertyInfo>().DecreaseLife(this.Damage);
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        this.isExplosion = false;

        //�]�wBoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(ExplosionDestroy);
    }

    /// <summary>
    /// SmoothMove UserTrigger(�����ʵe��R���ۤv)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void ExplosionDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //�T�{�w�i�J�z�����A�B�z�����`�ʵe��A�~�i�R��
        if (this.isExplosion && triggerEvent.animationName.CompareTo("explosion") == 0)
            Destroy(this.gameObject);
    }
}
using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-27
/// Modify Date�G2013-07-27
/// Author�GIan
/// Description�G
///     ��ê�����ݩʸ�T
/// </summary>
public class ObstaclePropertyInfo : MonoBehaviour
{
    public GameDefinition.Obstacle Obstacle;

    public int Damage;   //�����ˮ`��

    public bool isDead { get; private set; }

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isDead = false;

        //�]�wBoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(DeadDestroy);

        //Ū���t���x�s����ê���ݩʸ��
        GameDefinition.ObstacleData getData = GameDefinition.ObstacleList.Find((GameDefinition.ObstacleData data) => { return data.ObstacleName == Obstacle; });
        this.Damage = getData.Damage;
    }

    /// <summary>
    /// SmoothMove UserTrigger(�������`�ʵe��R���ۤv)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void DeadDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //�T�{�w�i�J���`���A�B���񦺤`�ʵe��A�~�i�R��
        if (this.isDead && triggerEvent.animationName.CompareTo("defeat") == 0)
            Destroy(this.gameObject);
    }

    ///// <summary>
    ///// ��ּĤH��q�禡
    ///// </summary>
    ///// <param name="deLife">��֪��ƭ�</param>
    //public void DecreaseLife(int deLife)
    //{
    //    deLife -= this.defence; //�������m�O
    //    if (deLife <= 0)
    //        deLife = 0;

    //    this.currentLife -= deLife;

    //    //��ͩR�p��0�A�R������
    //    if (!this.isDead && this.currentLife <= 0)
    //    {
    //        this.isDead = true;
    //        this.boneAnimation.Play("defeat");
    //        Destroy(this.GetComponent<MoveController>());
    //    }
    //}
}
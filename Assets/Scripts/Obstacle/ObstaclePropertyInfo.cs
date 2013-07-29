using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-27
/// Modify Date�G2013-07-29
/// Author�GIan
/// Description�G
///     ��ê�����ݩʸ�T
/// </summary>
public class ObstaclePropertyInfo : MonoBehaviour
{
    public GameDefinition.Obstacle Obstacle;

    public int Damage;   //�����ˮ`��

    public bool isDisappear { get; private set; }

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isDisappear = false;

        //�]�wBoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(DisappearDestroy);

        //Ū���t���x�s����ê���ݩʸ��
        GameDefinition.ObstacleData getData = GameDefinition.ObstacleList.Find((GameDefinition.ObstacleData data) => { return data.ObstacleName == Obstacle; });
        this.Damage = getData.Damage;
    }

    /// <summary>
    /// SmoothMove UserTrigger(���������ʵe��R���ۤv)
    /// </summary>
    /// <param name="triggerEvent"></param>
    void DisappearDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //�T�{�w�i�J�������A�B��������ʵe��A�~�i�R��
        if (this.isDisappear && (triggerEvent.animationName.CompareTo("correct") == 0 || triggerEvent.animationName.CompareTo("fail") == 0))
            Destroy(this.gameObject);
    }

    public void CheckObstacle(bool state)
    {
        if (state)
            this.boneAnimation.Play("correct");

        else
            this.boneAnimation.Play("fail");
        this.isDisappear = true;
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date�G2013-07-29
/// Modify Date�G2013-08-03
/// Author�GIan
/// Description�G
///     ��ê���t�� 
/// </summary>
public class ObstacleSystem : MonoBehaviour
{
    public List<GameDefinition.Obstacle> ObstacleList = new List<GameDefinition.Obstacle>();    //�i�B�z����ê���M��
    public SmoothMoves.BoneAnimation EffectAnimation;   //�ĪG�ʵe����

    private RolePropertyInfo roleInfo { get; set; }

    void OnTriggerEnter(Collider other)
    {
        ObstaclePropertyInfo infoScript = other.gameObject.GetComponent<ObstaclePropertyInfo>();

        //�p�G�i�JTrigger����LObstaclePropertyinfo �A�h���}���禡
        if (infoScript == null)
            return;

        //�T�{���⥻���O�_�������T����ê��
        if (this.ObstacleList.Contains(infoScript.Obstacle))
        {
            infoScript.CheckObstacle(true);
        }
        else
        {
            //�Ы� �����S��BoneAnimation
            SmoothMoves.BoneAnimation obj = (SmoothMoves.BoneAnimation)Instantiate(this.EffectAnimation);
            obj.mLocalTransform.position = other.ClosestPointOnBounds(this.transform.position) - Vector3.forward;
            obj.playAutomatically = false;
            //�H������ 1 �� 2 �ʵe���q
            if (Random.Range(0, 2) == 0)
                obj.Play("�����S��01");
            else
                obj.Play("�����S��02");

            infoScript.CheckObstacle(false);
            //��������ˮ`
            this.roleInfo.DecreaseLife(infoScript.Damage);
        }
    }

    // Use this for initialization
    void Start()
    {
        //���J�����T
        this.roleInfo = this.GetComponent<RolePropertyInfo>();
    }
}
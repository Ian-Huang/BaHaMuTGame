using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modify Date：2013-08-03
/// Author：Ian
/// Description：
///     障礙物系統 
/// </summary>
public class ObstacleSystem : MonoBehaviour
{
    public List<GameDefinition.Obstacle> ObstacleList = new List<GameDefinition.Obstacle>();    //可處理的障礙物清單
    public SmoothMoves.BoneAnimation EffectAnimation;   //效果動畫物件

    private RolePropertyInfo roleInfo { get; set; }

    void OnTriggerEnter(Collider other)
    {
        ObstaclePropertyInfo infoScript = other.gameObject.GetComponent<ObstaclePropertyInfo>();

        //如果進入Trigger物件無ObstaclePropertyinfo ，則離開此函式
        if (infoScript == null)
            return;

        //確認角色本身是否對應正確的障礙物
        if (this.ObstacleList.Contains(infoScript.Obstacle))
        {
            infoScript.CheckObstacle(true);
        }
        else
        {
            //創建 撞擊特效BoneAnimation
            SmoothMoves.BoneAnimation obj = (SmoothMoves.BoneAnimation)Instantiate(this.EffectAnimation);
            obj.mLocalTransform.position = other.ClosestPointOnBounds(this.transform.position) - Vector3.forward;
            obj.playAutomatically = false;
            //隨機撥放 1 或 2 動畫片段
            if (Random.Range(0, 2) == 0)
                obj.Play("撞擊特效01");
            else
                obj.Play("撞擊特效02");

            infoScript.CheckObstacle(false);
            //給予角色傷害
            this.roleInfo.DecreaseLife(infoScript.Damage);
        }
    }

    // Use this for initialization
    void Start()
    {
        //載入角色資訊
        this.roleInfo = this.GetComponent<RolePropertyInfo>();
    }
}
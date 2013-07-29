using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-27
/// Modify Date：2013-07-29
/// Author：Ian
/// Description：
///     障礙物的屬性資訊
/// </summary>
public class ObstaclePropertyInfo : MonoBehaviour
{
    public GameDefinition.Obstacle Obstacle;

    public int Damage;   //陷阱傷害值

    public bool isDisappear { get; private set; }

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isDisappear = false;

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(DisappearDestroy);

        //讀取系統儲存的障礙物屬性資料
        GameDefinition.ObstacleData getData = GameDefinition.ObstacleList.Find((GameDefinition.ObstacleData data) => { return data.ObstacleName == Obstacle; });
        this.Damage = getData.Damage;
    }

    /// <summary>
    /// SmoothMove UserTrigger(當播完消失動畫後刪除自己)
    /// </summary>
    /// <param name="triggerEvent"></param>
    void DisappearDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //確認已進入消失狀態且撥放消失動畫後，才可刪除
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
using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-22
/// Author：Ian
/// Description：
///     樹人長老頭上開的花
/// </summary>
public class TreeElder_Flower : MonoBehaviour
{
    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(FlowerCloseDestroy);

        this.boneAnimation.playAutomatically = false;
        this.boneAnimation.Play("FlowerOpen");
    }

    /// <summary>
    /// 播放花閉合動畫
    /// </summary>
    public void CloseFlower()
    {
        this.boneAnimation.Play("FlowerClose");
    }

    /// <summary>
    /// SmoothMove UserTrigger(當播完動畫後刪除自己)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void FlowerCloseDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //花閉合的動畫播完後，才可刪除
        if (triggerEvent.animationName.CompareTo("FlowerClose") == 0)
        {
            BossController_TreeElder.script.currentOpenFlowerCount--;
            Destroy(this.gameObject);
        }
    }
}

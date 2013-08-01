using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-08-01
/// Modify Date：2013-08-01
/// Author：Ian
/// Description：
///     自動刪除BoneAnimation(觸發UserTrigger)
/// </summary>
public class AutoDestroyBoneAnimation : MonoBehaviour
{
    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(AutoDestroy);
    }

    /// <summary>
    /// SmoothMove UserTrigger(當播完死亡動畫後刪除自己)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void AutoDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //刪除自己
        Destroy(this.gameObject);
    }
}

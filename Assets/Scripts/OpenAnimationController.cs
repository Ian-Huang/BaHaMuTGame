using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-31
/// Author：Ian
/// Description：
///     開始動畫事件控制器
/// </summary>
public class OpenAnimationController : MonoBehaviour
{
    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(AnimationEventFunction);
    }

    /// <summary>
    /// 動畫事件
    /// </summary>
    /// <param name="triggerEvent">觸發相關資訊</param>
    public void AnimationEventFunction(SmoothMoves.UserTriggerEvent triggerEvent)
    {

        //if (triggerEvent.boneName == "weapon" && triggerEvent.animationName == "attack")
        //結尾
        if (triggerEvent.normalizedTime == 1)
        {
            MUI_LoadSceneTransitionsEffect.script.LoadScene("LevelSelectScene", 0);
        }
    }
}

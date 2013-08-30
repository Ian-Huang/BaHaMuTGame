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
    public GameObject 城堡民宅_音效;
    public GameObject 大史萊姆落下_音效;
    public GameObject 小史萊姆落下_音效;
    public GameObject 邪氣降臨_音效;
    public GameObject 釋放邪氣_音效;
    public GameObject 魔王城_音效;

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
        //處理音效控制
        if (triggerEvent.boneName == "城堡民宅")
            Instantiate(this.城堡民宅_音效);
        else if (triggerEvent.boneName == "大史萊姆落下")
            Instantiate(this.大史萊姆落下_音效);
        else if (triggerEvent.boneName == "小史萊姆落下")
            Instantiate(this.小史萊姆落下_音效);
        else if (triggerEvent.boneName == "邪氣降臨")
            Instantiate(this.邪氣降臨_音效);
        else if (triggerEvent.boneName == "釋放邪氣")
            Instantiate(this.釋放邪氣_音效);
        else if (triggerEvent.boneName == "魔王城")
            Instantiate(this.魔王城_音效);

        //結尾
        if (triggerEvent.normalizedTime == 1)
        {
            MUI_LoadSceneTransitionsEffect.script.LoadScene("LevelSelectScene", 0);
        }
    }
}

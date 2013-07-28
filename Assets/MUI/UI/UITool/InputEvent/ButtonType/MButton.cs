using UnityEngine;
using System.Collections;

/// <summary>
/// 滑鼠按鍵在偵測範圍裡面按鍵的特效
/// </summary>
public class MButton : MonoBehaviour
{
    //按鈕是否實作狀態
    public bool ButtonEnable = true;

    public Object DisplayObject;

    [HideInInspector]
    public Rect rect;

    public GameObject EffectObjectWhenPress;
    public GameObject EffectObjectWhenRelease;

    public GameObject Event;

    [HideInInspector]
    public bool pressDown;

    //當Event被生成後，isDone = true 目前用在教學系統 用來檢視按鈕有沒有被成功使用
    [HideInInspector]
    public bool isDone;


    public void Update()
    {
        if (!ButtonEnable) return;
    }

    //Set the Button enable status
    public void SetButtonEnable(bool status)
    {
        ButtonEnable = status;
    }
}

using UnityEngine;
using System.Collections;

/// <summary>
/// 滑鼠按鍵在偵測範圍裡面按鍵的特效
/// </summary>
public class MButton : MonoBehaviour
{
    //全域靜態變數 深度門檻 當MUI深度在門檻之上 則不能作用(僅顯示，但不能被點擊)
    public static int DepthThreshold;
    //按鈕是否實作狀態
    [HideInInspector]
    public bool ButtonEnable = true;

    [HideInInspector]
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


    public void Start()
    {
        GetMUI();
    }

    public void Update()
    {
        //當MUI深度在門檻之上 則不能作用(顯示，但不能被點擊)
        int depth = (int)(DisplayObject.GetType().GetField("depth").GetValue(DisplayObject));
        if (depth > DepthThreshold)
            ButtonEnable = false;
        else
            ButtonEnable = true;

    }
    /// <summary>
    /// 自動偵測MUI物件來變成按鈕
    /// </summary>
    void GetMUI()
    {
        if (this.GetComponent<MUI_Texture_2D>()) DisplayObject = this.GetComponent<MUI_Texture_2D>();
    }
}

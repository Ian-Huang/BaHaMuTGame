using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/05/08    建置
/// 13/06/19    重新定義名稱 MUI_Effect
#endregion
/// <summary>
/// 特效基底 - 其他特效的父類別
/// </summary>
/// * 子類別   MUI_RectTo      位置與大小的特效
/// * 子類別   MUI_ColorTo     顏色的特效
/// * 子類別   MUI_ScaleTo     大小的特效(不會重新採樣)
/// * 子類別   MUI_MoveTo      純移動變量的特效
/// 
/// * 偵測ITween結束需要percentage變數，需要把ITween.cs的percentage設為Public ( iTween有oncomplete參數值可以使用但必須要傳"下一個動作"資料進去，會使資料不易整理)
/// * DisableWhenEffectDone 當特效結束將物件Disable
/// * ResetWhenDisable 當物件被Disable將物件狀態Reset到之前狀態
/// * [已移除] ResetWhenEffectDone 當特效結束將物件狀態Reset到之前狀態 (可由上述兩者代替)
public class MUI_Effect : MonoBehaviour {

    public MUI_Enum.EffectStruct _effectStruct;
    public MUI_Enum.StopEffectStruct _stopEffectStruct;

    //持續時間
    public float time;
    //延遲時間
    public float delay;
    //特效開始延遲時間
    public float EffectStartDelay;
    //Ease方式
    public MUI_Enum.EaseType easeType;
    //循環方式
    public MUI_Enum.loopType looptype;

    //物件被Disable時是否回到原本狀態
    public MUI_Enum.ResetWhenDisable _resetWhenDisable;
    //特效結束時 物件Disable
    public MUI_Enum.DisableWhenEffectDone _disableWhenEffectDone;

    [HideInInspector]
    //是否無視TimeScale
    public bool ignoretimescale;

    //特效結束後下一個效果物件
    public GameObject NextEffect;


    //錯誤修正與避免
    public void BugFix()
    {
        //* 根據 Issue 72 
        //ITween使用delay時IgnoreTimeScale無效 ， 所以當delay大於0 會使用被TimeScale影響的函式 
        //https://code.google.com/p/itween/issues/detail?id=72

        if (delay > 0) ignoretimescale = false;
        else ignoretimescale = true;
    }


    public bool isReset()
    {
        if (_resetWhenDisable >= MUI_Enum.ResetWhenDisable.True)
            return true;
        else
            return false;
    }

    public bool isReDefine()
    {
        if (_resetWhenDisable == MUI_Enum.ResetWhenDisable.True_ReDefine)
            return true;
        else
            return false;
    }


}

using UnityEngine;
using System.Collections;

public class MUI_EffectTo : MonoBehaviour {

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

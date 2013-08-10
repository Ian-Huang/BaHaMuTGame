﻿using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/05/06    建置
/// 13/06/17    重新定義名稱 MUI_Base  
#endregion

/// <summary>
/// 介面基底 - 其他介面的父類別
/// </summary>
/// 特效系統 - 回傳變數並建立iTween執行動畫效果
/// * 子類別   MUI_Label  文字標籤介面
/// * 子類別   MUI_Texture_2D 2D圖像介面
public class MUI_Base : MonoBehaviour
{
    //介面皮膚
    public GUISkin guiSkin;

    //位置與大小
    public Rect rect;

    //角度
    public int angle;

    //放大倍率
    public Vector2 scale = new Vector2(1, 1);

    [HideInInspector]
    //排版放大倍率
    public Vector2 LayoutScale = new Vector2(1, 1);

    //圖案顏色
    public Color color = Color.white;

    //介面深度 - 負值越前面
    public int depth;

    //介面偏移量
    public Vector2 offset = new Vector2(1, 1);

    //設定中心點 (左上、正中央)
    public enum centerAlignment { UpperLeft, MiddleCenter };
    public centerAlignment CenterAlignment;


    /// <summary>
    /// 自動根據解析度放大縮小
    /// None - 無
    /// WidthFixed - 固定寬度
    /// HeightFixed - 固定高度
    /// AreaFixed - 根據面積
    /// </summary>
    public enum AutoResolutionFix { None, WidthFixed, HeightFixed, AreaFixed };
    public AutoResolutionFix autoResolutionFix;

    [HideInInspector]
    //中心點
    public Vector2 CenterPosition;

    [HideInInspector]
    //排版中心點
    public Vector2 LayoutCenterPosition;

    [HideInInspector]
    //解析度 - 依據1280為基準
    public int Resolution = 1280;

    [HideInInspector]
    //視窗大小
    public Vector2 _ScreenSize = new Vector2(Screen.width, Screen.height);

    [HideInInspector]
    //視窗比例
    public float screenRatio;


    //備份資訊 (BackUp)
    private Rect _rect_previousState;
    private Color _color_previousState;
    private Vector2 _scale_previousState;

    //運算後的Rect(Pixel值)
    [HideInInspector]
    public Rect _rect;



    // Use this for initialization
    public void Start()
    {
        _ScreenSize = new Vector2(Screen.width, Screen.height);
        screenRatio = Screen.width / (float)Screen.height;
        ReDefinePreviousState();

        //在Start先進行一次Update，可以避免當Enable時 顯示字體會有極短暫閃爍的情形
        Update();
    }

    public void Update()
    {
        if (angle >= 360 || angle <= -360)
            angle = 0;


        _rect = new Rect(rect.x * _ScreenSize.x
                        , rect.y * _ScreenSize.y
                        , rect.width * _ScreenSize.x
                        , rect.height * _ScreenSize.y);


        if (CenterAlignment == centerAlignment.MiddleCenter)
        {
            CenterPosition.x = _rect.x;
            CenterPosition.y = _rect.y;
            _rect = new Rect(CenterPosition.x - _rect.width / 2, CenterPosition.y - _rect.height / 2, _rect.width, _rect.height);
        }
        if (CenterAlignment == centerAlignment.UpperLeft)
        {
            CenterPosition.x = _rect.x;
            CenterPosition.y = _rect.y;
        }

        if (autoResolutionFix == AutoResolutionFix.WidthFixed)
            rect.height = rect.width * screenRatio;
        if (autoResolutionFix == AutoResolutionFix.HeightFixed)
            rect.width = rect.height / screenRatio;
        if (autoResolutionFix == AutoResolutionFix.AreaFixed)
        {
            float area = rect.width * rect.height;
            float Screen_area = _ScreenSize.x * _ScreenSize.y;
            //目標面積對畫面面積比率 開根號
            float sqrt_x = Mathf.Sqrt(area / Screen_area);

            rect.width = float.Parse((_ScreenSize.y * sqrt_x).ToString("0.0000"));
            rect.height = float.Parse((_ScreenSize.x * sqrt_x).ToString("0.0000"));
        }
    }


    //Set Backup
    public void ReDefinePreviousState()
    {
        _rect_previousState = rect;
        _color_previousState = color;
        _scale_previousState = scale;
    }




    #region #特效系統

    /// <summary>
    /// 製造Rect動畫效果 (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">特效結構</param>
    void RectTo(MUI_Enum.EffectStruct effect)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", rect,
            "to", effect.rect,
            "delay", effect.delay,
            "time", effect.time,
            "easetype", effect.easeType.ToString(),
            "onupdate", "updateRect",
             "loopType", effect.looptype.ToString(),
             "ignoretimescale", effect.ignoretimescale,
             "name", "RectTo" + effect.hashcode));
    }

    /// <summary>
    /// 製造Color動畫效果 (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">特效結構</param>
    void ColorTo(MUI_Enum.EffectStruct effect)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
           "from", color,
           "to", effect.color,
           "delay", effect.delay,
           "time", effect.time,
           "easetype", effect.easeType.ToString(),
           "onupdate", "updateColor",
           "loopType", effect.looptype.ToString(),
           "ignoretimescale", effect.ignoretimescale,
           "name", "ColorTo" + effect.hashcode));
    }

    /// <summary>
    /// 製造Scale動畫效果 (Create)
    /// Name - ScaleTo
    /// </summary>
    /// <param name="effect">特效結構</param>
    void ScaleTo(MUI_Enum.EffectStruct effect)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
           "from", scale,
           "to", effect.scale,
           "delay", effect.delay,
           "time", effect.time,
           "easetype", effect.easeType.ToString(),
           "onupdate", "updateScale",
           "loopType", effect.looptype.ToString(),
           "ignoretimescale", effect.ignoretimescale,
           "name", "ScaleTo" + effect.hashcode));
    }

    /// <summary>
    /// 製造LayoutScale動畫效果 (Create)
    /// Name - ScaleTo
    /// </summary>
    /// <param name="effect">特效結構</param>
    void LayoutScaleTo(MUI_Enum.EffectStruct effect)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
           "from", scale,
           "to", effect.scale,
           "delay", effect.delay,
           "time", effect.time,
           "easetype", effect.easeType.ToString(),
           "onupdate", "updateLayoutScale",
           "loopType", effect.looptype.ToString(),
           "ignoretimescale", effect.ignoretimescale,
           "name", "ScaleTo" + effect.hashcode));
    }


    void StopRectTo(MUI_Enum.StopEffectStruct stopEffect)
    {
        if (stopEffect.isReDefine) _rect_previousState = stopEffect.rect;
        if (stopEffect.isReset) rect = _rect_previousState;
        iTween.StopByName(this.gameObject, "RectTo" + stopEffect.hashcode);
    }
    void StopColorTo(MUI_Enum.StopEffectStruct stopEffect)
    {
        if (stopEffect.isReDefine) _color_previousState = stopEffect.color;
        if (stopEffect.isReset) color = _color_previousState;
        iTween.StopByName(this.gameObject, "ColorTo" + stopEffect.hashcode);
    }
    void StopScaleTo(MUI_Enum.StopEffectStruct stopEffect)
    {
        if (stopEffect.isReDefine) _scale_previousState = stopEffect.scale;
        if (stopEffect.isReset) scale = _scale_previousState;
        iTween.StopByName(this.gameObject, "ScaleTo" + stopEffect.hashcode);
    }
    void StopLayoutScaleTo(MUI_Enum.StopEffectStruct stopEffect)
    {
        if (stopEffect.isReDefine) _scale_previousState = stopEffect.scale;
        if (stopEffect.isReset) scale = _scale_previousState;
        iTween.StopByName(this.gameObject, "LayoutScaleTo" + stopEffect.hashcode);
    }

    // Update callback for iTween
    void updateRect(Rect input)
    {
        rect = input;
    }
    void updateColor(Color input)
    {
        color = input;
    }
    void updateScale(Vector2 input)
    {
        scale = input;
    }
    void updateLayoutScale(Vector2 input)
    {
        LayoutScale = input;
    }
    #endregion 特效系統
}

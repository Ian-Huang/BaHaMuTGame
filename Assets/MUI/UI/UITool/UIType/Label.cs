using UnityEngine;
using System.Collections;

/// <summary>
/// 介面 - 顯示文字
/// 13/03/20 Updated.
/// 13/04/06 角度與倍率.
/// 13/05/05 繼承 UIBase 
/// </summary>
public class Label : UIBase
{
    //文字
    public string Text = "Type Text here";
    //文字大小
    public int FontSize = 10;
    //文字對準方式
    public TextAnchor Alignment;


    // Use this for initialization
    void Start()
    {
        if (FontSize == 0) Debug.LogWarning(this.name + "-FontSize" + "-Unset");

        _fontSize_backup = FontSize;
        UIBase_Start();
    }

    // Update is called once per frame

    void Update()
    {
        //UIBase auto update
        UIBase_Update();
    }


    void OnGUI()
    {
        if (guiSkin)
            GUI.skin = this.guiSkin;

        _rect = new Rect(rect.x * _ScreenSize.x
                        , rect.y * _ScreenSize.y
                        , rect.width * _ScreenSize.x
                        , rect.height * _ScreenSize.y);

        GUI.skin.label.fontSize = (int)((_ScreenSize.x / Resolution) * FontSize);
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.alignment = Alignment;
        GUI.depth = depth;
        GUIUtility.RotateAroundPivot(angle, CenterPosition);
        GUIUtility.ScaleAroundPivot(scale, CenterPosition);

        GUI.Label(_rect, Text);
    }


    /// <summary>
    /// 製造FontSize動畫效果 (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">特效結構</param>
    void FontSizeTo(MEnum.EffectStruct effect)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
           "from", FontSize,
           "to", effect.fontSize,
           "delay", effect.delay,
           "time", effect.time,
           "easetype", effect.easeType.ToString(),
           "onupdate", "updateFontSize",
           "loopType", effect.looptype.ToString(),
           "name", "FontSizeTo"));

    }

    void StopFontSizeTo(bool Reset)
    {
        if (Reset)
            FontSize = _fontSize_backup;
        iTween.StopByName(this.gameObject, "FontSizeTo");
    }

    void updateFontSize(int input)
    {
        FontSize = input;
    }

}

using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/05/06    建置
/// 13/06/17    重新定義名稱 MUI_Label            
#endregion

/// <summary>
/// 介面 - 文字標籤介面
/// </summary>
/// 獨立特效系統 - 製造FontSize動畫效果
public class MUI_Label : MUI_Base
{
    //文字
    public string Text = "Type Text here";
    //文字大小
    public int FontSize = 10;
    //文字對準方式
    public TextAnchor Alignment;

    private int _fontSize_backup;

    // Use this for initialization
    void Start()
    {
        _fontSize_backup = FontSize;
        UIBase_Start();
        LogWarning();
    }

    void LogWarning()
    {
        if (FontSize == 0) Debug.LogWarning(this.name + "-FontSize" + "-Unset");
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

        GUI.skin.label.fontSize = (int)((_ScreenSize.x / Resolution) * FontSize);
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.alignment = Alignment;

        GUI.depth = depth;
        GUIUtility.RotateAroundPivot(angle, CenterPosition);

        if (scale != Vector2.zero)
            GUIUtility.ScaleAroundPivot(scale, CenterPosition);

        if (Text != null)
        {
            GUI.BeginGroup(new Rect(_rect.x, _rect.y, _rect.width * offset.x, _rect.height * offset.y));
            {
                GUI.Label(new Rect(0, 0, _rect.width, _rect.height), Text);
            }
            GUI.EndGroup();
        }
    }


    /// <summary>
    /// 製造FontSize動畫效果 (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">特效結構</param>
    void FontSizeTo(MUI_Enum.EffectStruct effect)
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

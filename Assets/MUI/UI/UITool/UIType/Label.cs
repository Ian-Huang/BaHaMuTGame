using UnityEngine;
using System.Collections;

/// <summary>
/// ���� - ��ܤ�r
/// 13/03/20 Updated.
/// 13/04/06 ���׻P���v.
/// 13/05/05 �~�� UIBase 
/// </summary>
public class Label : UIBase
{
    //��r
    public string Text = "Type Text here";
    //��r�j�p
    public int FontSize = 10;
    //��r��Ǥ覡
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
    /// �s�yFontSize�ʵe�ĪG (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">�S�ĵ��c</param>
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

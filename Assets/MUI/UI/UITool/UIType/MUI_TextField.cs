using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/05/05    建置
/// 13/06/17    重新定義名稱 MUI_TextField
#endregion

/// <summary>
/// 介面 - 輸入文字框介面
/// </summary>

public class MUI_TextField : MUI_Base
{
    //文字
    public string Text = "";
    //文字大小
    public int FontSize = 10;
    //文字對準方式
    public TextAnchor Alignment;

    // Use this for initialization
    new void Start()
    {
        if (!guiSkin) Debug.LogWarning(this.name + "-guiSkin" + "-Unset");
        if (FontSize == 0) Debug.LogWarning(this.name + "-FontSize" + "-Unset");
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }



    void OnGUI()
    {
        if (guiSkin)
            GUI.skin = this.guiSkin;

        GUI.skin.textField.fontSize = (int)((_ScreenSize.x / Resolution) * FontSize);
        GUI.skin.textField.alignment = Alignment;
        GUI.skin.textField.normal.textColor = color;
        GUI.depth = depth;
        GUIUtility.RotateAroundPivot(angle, CenterPosition);
        if (scale != Vector2.zero)
            GUIUtility.ScaleAroundPivot(scale, CenterPosition);

        if (Text != null)
        {
            GUI.BeginGroup(new Rect(_rect.x, _rect.y, _rect.width * offset.x, _rect.height * offset.y));
            {
                Text = GUI.TextField(new Rect(0, 0, _rect.width, _rect.height), Text);
            }
            GUI.EndGroup();
        }

    }

    /// <summary>
    /// 清除字串
    /// </summary>
    public void Clear()
    {
        Text = "";
    }
}

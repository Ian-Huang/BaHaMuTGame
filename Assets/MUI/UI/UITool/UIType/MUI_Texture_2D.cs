using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/05/06    建置
/// 13/06/17    重新定義名稱 MUI_Texture_2D           
#endregion

/// <summary>
/// 介面 - 2D圖像介面
/// </summary>
/// 獨立特效系統 - 製造FontSize動畫效果
public class MUI_Texture_2D : MUI_Base
{
    //圖片縮放方式
    public ScaleMode scaleMode;
    //Texture
    public Texture Texture2d;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        LogWarning();
    }

    void LogWarning()
    {
        if (!Texture2d) Debug.LogWarning(this.name + "-Texture2d" + "-Unset");
    }

    // Update is called once per frame
    new void Update()
    {
        //UIBase auto update
        base.Update();
    }

    void OnGUI()
    {
        GUI.color = color;
        GUI.depth = depth;

        GUIUtility.RotateAroundPivot(angle, CenterPosition);

        if (scale != Vector2.zero)
            GUIUtility.ScaleAroundPivot(scale, CenterPosition);

        if (LayoutScale != Vector2.zero)
            GUIUtility.ScaleAroundPivot(LayoutScale, LayoutCenterPosition);

        if (Texture2d)
        {
            GUI.BeginGroup(new Rect(_rect.x, _rect.y, _rect.width * offset.x, _rect.height * offset.y));
            {
                GUI.DrawTexture(new Rect(0, 0, _rect.width, _rect.height), Texture2d, scaleMode);
            }
            GUI.EndGroup();
        }
    }


}

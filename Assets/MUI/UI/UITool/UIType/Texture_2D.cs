using UnityEngine;
using System.Collections;

/// <summary>
/// 介面 - 顯示圖片
/// 13/03/20 Updated.
/// </summary>
public class Texture_2D : UIBase
{

    //Texture
    public Texture Texture2d;
    public ScaleMode scaleMode;

    // Use this for initialization
    void Start()
    {
        if (!Texture2d) Debug.LogWarning(this.name + "-Texture2d" + "-Unset");
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

        _rect = new Rect(rect.x * _ScreenSize.x
                        , rect.y * _ScreenSize.y
                        , rect.width * _ScreenSize.x
                        , rect.height * _ScreenSize.y);

        GUI.color = color;
        GUI.depth = depth;
        GUIUtility.RotateAroundPivot(angle, new Vector2(_rect.x + _rect.width / 2, _rect.y + _rect.height / 2));
        GUIUtility.ScaleAroundPivot(scale, new Vector2(_rect.x + _rect.width / 2, _rect.y + _rect.height / 2));


        if (Texture2d)
            GUI.DrawTexture(_rect, Texture2d, scaleMode);

    }
}

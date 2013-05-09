using UnityEngine;
using System.Collections;

/// <summary>
/// 介面 - 顯示圖片
/// 13/03/20 Updated.
/// 13/05/09 根據CenterPosition給予繪製中心點
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
        GUI.color = color;
        GUI.depth = depth;
        GUIUtility.RotateAroundPivot(angle, CenterPosition);
        GUIUtility.ScaleAroundPivot(scale, CenterPosition);


     // Vector2 DrawPosition = CenterPosition - new Vector2(_rect.width / 2, _rect.height / 2);
        if (CenterAlignment == centerAlignment.MiddleCenter)
            _rect = new Rect(CenterPosition.x - _rect.width/2, CenterPosition.y - _rect.height/2, _rect.width, _rect.height);

        if (Texture2d)
            GUI.DrawTexture(_rect, Texture2d, scaleMode);

    }
}

using UnityEngine;
using System.Collections;

/// <summary>
/// ���� - ��ܹϤ�
/// 13/03/20 Updated.
/// 13/05/09 �ھ�CenterPosition����ø�s�����I
/// </summary>
public class Texture_2D : UIBase
{

    //Texture
    public Texture Texture2d;
    public ScaleMode scaleMode;

    // Use this for initialization
    void Start()
    {
        UIBase_Start();
    }

    void LogWarning()
    {
        if (!Texture2d) Debug.LogWarning(this.name + "-Texture2d" + "-Unset");
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
        if (Texture2d)
            GUI.DrawTexture(_rect, Texture2d, scaleMode);
        
    }
}

using UnityEngine;
using System.Collections;

#region �����������������������������������������������ץ���������������������������������������������������
/// 13/05/06    �ظm
/// 13/06/17    ���s�w�q�W�� MUI_Texture_2D           
#endregion

/// <summary>
/// ���� - 2D�Ϲ�����
/// </summary>
/// �W�߯S�Ĩt�� - �s�yFontSize�ʵe�ĪG
public class MUI_Texture_2D : MUI_Base
{
    //�Ϥ��Y��覡
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

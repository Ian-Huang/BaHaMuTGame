using UnityEngine;
using System.Collections;

#region �����������������������������������������������ץ���������������������������������������������������
/// 13/05/05    �ظm
/// 13/06/17    ���s�w�q�W�� MUI_TextField
#endregion

/// <summary>
/// ���� - ��J��r�ؤ���
/// </summary>

public class MUI_TextField : MUI_Base
{
    //��r
    public string Text = "";
    //��r�j�p
    public int FontSize = 10;
    //��r��Ǥ覡
    public TextAnchor Alignment;

    // Use this for initialization
    void Start()
    {
        if (!guiSkin) Debug.LogWarning(this.name + "-guiSkin" + "-Unset");
        if (FontSize == 0) Debug.LogWarning(this.name + "-FontSize" + "-Unset");
        UIBase_Start();
    }

    // Update is called once per frame
    void Update()
    {
        UIBase_Update();
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
    /// �M���r��
    /// </summary>
    public void Clear()
    {
        Text = "";
    }
}

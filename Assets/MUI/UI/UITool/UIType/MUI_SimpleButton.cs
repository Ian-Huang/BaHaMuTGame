using UnityEngine;
using System.Collections;

#region �����������������������������������������������ץ���������������������������������������������������
/// 13/05/05    �ظm
/// 13/06/17    ���s�w�q�W�� MUI_SimpleButton
#endregion

/// <summary>
/// ���� - ²�����s����
/// </summary>
/// * Event - �гy�sEvent��SetActive �� True
public class MUI_SimpleButton : MUI_Base
{
    //��r
    public string Text = "";
    //��r�j�p
    public int FontSize = 10;
    //��r��Ǥ覡
    public TextAnchor Alignment = TextAnchor.MiddleCenter;
    //���s���^�X�A�ϥ�GameObject������Ĳ�o
    public GameObject Event;

    // Use this for initialization
    void Start()
    {
        if (FontSize == 0) Debug.LogWarning(this.name + "-FontSize" + "-Unset");
        MUI_Base_Start();
    }

    // Update is called once per frame
    void Update()
    {
        MUI_Base_Update();
    }

    void OnGUI()
    {
        if (guiSkin)
            GUI.skin = this.guiSkin;

        GUI.skin.button.fontSize = (int)((_ScreenSize.x / Resolution) * FontSize);
        GUI.skin.button.alignment = Alignment;
        GUI.color = color;
        GUI.depth = depth;
        GUIUtility.RotateAroundPivot(angle, CenterPosition);
        if (scale != Vector2.zero)
            GUIUtility.ScaleAroundPivot(scale, CenterPosition);

        if (Text != null)
        {
            GUI.BeginGroup(new Rect(_rect.x, _rect.y, _rect.width * offset.x, _rect.height * offset.y));
            {
                //�p�G���U���s
                if (GUI.Button(new Rect(0, 0, _rect.width, _rect.height), Text))
                {
                    if (Event)
                    {
                        GameObject newEvent = (GameObject)Instantiate(Event);
                        newEvent.SetActive(true);
                    }
                }
            }
            GUI.EndGroup();
        }
    }
}

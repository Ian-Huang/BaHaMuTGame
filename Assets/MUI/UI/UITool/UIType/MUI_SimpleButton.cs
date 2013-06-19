using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/05/05    建置
/// 13/06/17    重新定義名稱 MUI_SimpleButton
#endregion

/// <summary>
/// 介面 - 簡易按鈕介面
/// </summary>
/// * Event - 創造新Event並SetActive 為 True
public class MUI_SimpleButton : MUI_Base
{
    //文字
    public string Text = "";
    //文字大小
    public int FontSize = 10;
    //文字對準方式
    public TextAnchor Alignment = TextAnchor.MiddleCenter;
    //按鈕的回饋，使用GameObject做物件觸發
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
                //如果按下按鈕
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

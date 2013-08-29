using UnityEngine;
using System.Collections;

/// <summary>
/// 主要功能：可以藉由外部改變內部變數
/// Offset 監控
/// 改變　Texture2D　或　Label 的 Offset 值
///  * 以Key的方式尋找監控Dictionary對應的資料
/// </summary>
public class MUI_OffsetMonitor : MonoBehaviour
{
    public Vector2 From;
    public Vector2 To;
    public string Key;

    [HideInInspector]
    public Vector2 Percentage;
    [HideInInspector]
    public MUI_Enum.MUIType MUI_Type;

    private Vector2 offset;

    private MUI_Monitor mMonitor = new MUI_Monitor();

    //根據MUI_Monitor的值 若所指定的值為目標值，將按鈕(MButton)給打開 反之則否
    public ButtonEnableBy buttonEnableBy;
    //當按鈕可被執行時的特效
    public GameObject ButtonEnableOnEffect;

    // Use this for initialization
    void Start()
    {
        //以Key為字串註冊一個索引
        if (Key != "") mMonitor.SubmitKey(Key + "x");
        if (Key != "") mMonitor.SubmitKey(Key + "y");
    }

    // Update is called once per frame
    void Update()
    {
        if (mMonitor.isValid(Key + "x")) Percentage.x = MUI_Monitor.MonitorDictionary[Key + "x"];
        if (mMonitor.isValid(Key + "y")) Percentage.y = MUI_Monitor.MonitorDictionary[Key + "y"];
        offset.x = Mathf.Lerp(From.x, To.x, Percentage.x / 100);
        offset.y = Mathf.Lerp(From.y, To.y, Percentage.y / 100);


        //驅動介面的變化
        if (this.GetComponent<MUI_Texture_2D>()) this.GetComponent<MUI_Texture_2D>().offset = offset;
        if (this.GetComponent<MUI_Label>()) this.GetComponent<MUI_Label>().offset = offset;

        switch (buttonEnableBy)
        {
            case ButtonEnableBy.x:
                if (Percentage.x == 100)
                {
                    ButtonEnableOnEffect.SetActive(true);
                    foreach (MButton mb in this.GetComponents<MButton>())
                        mb.enabled = true;
                }
                else
                {
                    ButtonEnableOnEffect.SetActive(false);
                    foreach (MButton mb in this.GetComponents<MButton>())
                        mb.enabled = false;
                }

                break;

            case ButtonEnableBy.y:
                if (Percentage.y == 100)
                {
                    ButtonEnableOnEffect.SetActive(true);
                    foreach (MButton mb in this.GetComponents<MButton>())
                        mb.enabled = true;
                }
                else
                {
                    ButtonEnableOnEffect.SetActive(false);
                    foreach (MButton mb in this.GetComponents<MButton>())
                        mb.enabled = false;
                }

                break;
        }

    }


    public enum ButtonEnableBy
    {
        none = 0, x = 1, y = 2
    }
}

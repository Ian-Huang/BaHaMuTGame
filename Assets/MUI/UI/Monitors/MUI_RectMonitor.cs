using UnityEngine;
using System.Collections;

/// <summary>
/// Rect 監控
/// 改變　Texture2D　或　Label 的 Rect 值
///  * 以Key的方式尋找監控Dictionary對應的資料
/// </summary>
public class MUI_RectMonitor : MonoBehaviour
{
    public Vector2 From;
    public Vector2 To;
    public string Key;

    [HideInInspector]
    public Vector2 Percentage;
    [HideInInspector]
    public MUI_Enum.MUIType MUI_Type;

    private Rect rect;

    private MUI_Monitor mMonitor = new MUI_Monitor();

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
        // if (isValid(Key + "x")) Percentage.x = MonitorDictionary[Key + "x"];
        //if (isValid(Key + "y")) Percentage.y = MonitorDictionary[Key + "y"];
        //rect.x = Mathf.Lerp(From.x, To.x, Percentage.x / 100);
        //rect.y = Mathf.Lerp(From.y, To.y, Percentage.y / 100);


        if (mMonitor.isValid(Key + "x")) Percentage.x = MUI_Monitor.MonitorDictionary[Key + "x"];
        if (mMonitor.isValid(Key + "y")) Percentage.y = MUI_Monitor.MonitorDictionary[Key + "y"];
        rect.x = Mathf.Lerp(From.x, To.x, Percentage.x / 100);
        rect.y = Mathf.Lerp(From.y, To.y, Percentage.y / 100);


        //介面變化
        if (this.GetComponent<MUI_Texture_2D>())
        {
            this.GetComponent<MUI_Texture_2D>().rect.x = rect.x;
            this.GetComponent<MUI_Texture_2D>().rect.y = rect.y;

        }
        if (this.GetComponent<MUI_Label>())
        {
            this.GetComponent<MUI_Label>().rect.x = rect.x;
            this.GetComponent<MUI_Label>().rect.y = rect.y;
        }
    }
}

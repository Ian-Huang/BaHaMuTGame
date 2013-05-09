using UnityEngine;
using System.Collections;

public class UIBase : MonoBehaviour
{

    //介面皮膚
    public GUISkin guiSkin;

    //位置與大小
    public Rect rect;

    //角度
    public int angle;

    //放大倍率
    public Vector2 scale = new Vector2(1, 1);

    //圖案顏色
    public Color color = Color.white;

    //介面深度 - 正值越前面
    public int depth;

    //設定中心點
    public enum centerAlignment { UpperLeft, MiddleCenter };
    public centerAlignment CenterAlignment;

    //自動藉由解析度放大縮小
    public enum AutoResolutionFix { None,WidthFixed, HeightFixed ,AreaFixed};
    public AutoResolutionFix autoResolutionFix;

    [HideInInspector]
    public Vector2 CenterPosition;

    [HideInInspector]
    //解析度 - 依據1280為基準
    public int Resolution = 1280;

    [HideInInspector]
    //視窗大小
    public Vector2 _ScreenSize = new Vector2(Screen.width, Screen.height);

    [HideInInspector]
    //視窗比例
    public float screenRatio;

    //備份資訊 (BackUp)
    [HideInInspector]
    public Rect _rect_backup;

    [HideInInspector]
    public Color _color_backup;

    [HideInInspector]
    public int _fontSize_backup;

    [HideInInspector]
    public Rect _rect;

    void Awake()
    {
        _ScreenSize = new Vector2(Screen.width, Screen.height);
        screenRatio = Screen.width / (float)Screen.height;
    }
    // Use this for initialization
    void Start()
    {
        

    }

    /// <summary>
    /// Virtual Method UIBase_Start
    /// </summary>
    virtual public void UIBase_Start()
    {
        if (!guiSkin) Debug.LogWarning(this.name + "-guiSkin" + "-Unset");

        //Set Backup
       
        _rect_backup = rect;
        _color_backup = color;

        
    }


    /// <summary>
    /// Virtual Method UIBase_Update
    /// </summary>
    virtual public void UIBase_Update()
    {
        if (angle >= 360 || angle <= -360)
            angle = 0;


        _rect = new Rect(rect.x * _ScreenSize.x
                        , rect.y * _ScreenSize.y
                        , rect.width * _ScreenSize.x
                        , rect.height * _ScreenSize.y);


        if (CenterAlignment == centerAlignment.MiddleCenter)
        {
            CenterPosition.x = _rect.x;
            CenterPosition.y = _rect.y;
        }
        if (CenterAlignment == centerAlignment.UpperLeft)
        {
            CenterPosition.x = _rect.x;
            CenterPosition.y = _rect.y;
        }

        if (autoResolutionFix == AutoResolutionFix.WidthFixed)
            rect.height = rect.width * screenRatio;
        if (autoResolutionFix == AutoResolutionFix.HeightFixed)
            rect.width = rect.height / screenRatio;
        if (autoResolutionFix == AutoResolutionFix.AreaFixed)
        {
            float area = rect.width * rect.height;
            float Screen_area = _ScreenSize.x * _ScreenSize.y;
            //目標面積對畫面面積比率 開根號
            float sqrt_x = Mathf.Sqrt(area / Screen_area);

            rect.width = float.Parse((_ScreenSize.y * sqrt_x).ToString("0.0000"));
            rect.height = float.Parse((_ScreenSize.x * sqrt_x).ToString("0.0000"));
        }

    }

    #region #特效系統

    /// <summary>
    /// 製造Rect動畫效果 (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">特效結構</param>
    void RectTo(MEnum.EffectStruct effect)
    {

        iTween.ValueTo(gameObject, iTween.Hash(
            "from", rect,
            "to", effect.rect,
            "delay", effect.delay,
            "time", effect.time,
            "easetype", effect.easeType.ToString(),
            "onupdate", "updateRect",
             "loopType", effect.looptype.ToString(),
             "name", "RectTo"));
    }

    /// <summary>
    /// 製造Color動畫效果 (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">特效結構</param>
    void ColorTo(MEnum.EffectStruct effect)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
           "from", color,
           "to", effect.color,
           "delay", effect.delay,
           "time", effect.time,
           "easetype", effect.easeType.ToString(),
           "onupdate", "updateColor",
           "loopType", effect.looptype.ToString(),
           "name", "ColorTo"));



    }


    void StopRectTo(bool Reset)
    {
        if (Reset)
            rect = _rect_backup;
        iTween.StopByName(this.gameObject, "RectTo");
    }
    void StopColorTo(bool Reset)
    {
        if (Reset)
            color = _color_backup;
        iTween.StopByName(this.gameObject, "ColorTo");
    }


    // Update callback for iTween
    void updateRect(Rect input)
    {
        rect = input;
    }
    void updateColor(Color input)
    {
        color = input;
    }


    #endregion 特效系統
}

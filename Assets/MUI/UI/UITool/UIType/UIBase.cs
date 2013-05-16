using UnityEngine;
using System.Collections;

/// <summary>
/// 介面基底 - 其他介面的父類別
/// 13/05/06 Updated.
/// </summary>
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

    //設定中心點 (左上、正中央)
    public enum centerAlignment { UpperLeft, MiddleCenter };
    public centerAlignment CenterAlignment;

    
    /// <summary>
    /// 自動藉由解析度放大縮小
    /// None - 無
    /// WidthFixed - 固定寬度
    /// HeightFixed - 固定高度
    /// AreaFixed - 根據面積
    /// </summary>
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
    private Rect _rect_backup;
    private Color _color_backup;
    private Vector2 _scale_backup;

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
        _scale_backup = scale;

        
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
            _rect = new Rect(CenterPosition.x - _rect.width / 2, CenterPosition.y - _rect.height / 2, _rect.width, _rect.height);
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
             "name", "RectTo" + effect.hashcode));
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
           "name", "ColorTo" + effect.hashcode));
    }

    /// <summary>
    /// 製造Scale動畫效果 (Create)
    /// Name - ScaleTo
    /// </summary>
    /// <param name="effect">特效結構</param>
    void ScaleTo(MEnum.EffectStruct effect)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
           "from", scale,
           "to", effect.scale,
           "delay", effect.delay,
           "time", effect.time,
           "easetype", effect.easeType.ToString(),
           "onupdate", "updateScale",
           "loopType", effect.looptype.ToString(),
           "name", "ScaleTo" + effect.hashcode));
    }


    void StopRectTo(MEnum.StopEffectStruct stopEffect)
    {
        if (stopEffect.isReset)
            rect = _rect_backup;
        iTween.StopByName(this.gameObject, "RectTo" + stopEffect.hashcode);
    }
    void StopColorTo(MEnum.StopEffectStruct stopEffect)
    {
        if (stopEffect.isReset)
            color = _color_backup;
        iTween.StopByName(this.gameObject, "ColorTo" + stopEffect.hashcode);
    }
    void StopScaleTo(MEnum.StopEffectStruct stopEffect)
    {
        if (stopEffect.isReset)
            scale = _scale_backup;
        iTween.StopByName(this.gameObject, "ScaleTo" + stopEffect.hashcode);
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
    void updateScale(Vector2 input)
    {
        scale = input;
    }



    #endregion 特效系統
}

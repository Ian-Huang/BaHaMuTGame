using UnityEngine;
using System.Collections;

/// <summary>
/// ������ - ��L�����������O
/// 13/05/06 Updated.
/// </summary>
public class UIBase : MonoBehaviour
{
    //�����ֽ�
    public GUISkin guiSkin;

    //��m�P�j�p
    public Rect rect;

    //����
    public int angle;

    //��j���v
    public Vector2 scale = new Vector2(1, 1);

    //�Ϯ��C��
    public Color color = Color.white;

    //�����`�� - ���ȶV�e��
    public int depth;

    //�]�w�����I (���W�B������)
    public enum centerAlignment { UpperLeft, MiddleCenter };
    public centerAlignment CenterAlignment;

    
    /// <summary>
    /// �۰��ǥѸѪR�ש�j�Y�p
    /// None - �L
    /// WidthFixed - �T�w�e��
    /// HeightFixed - �T�w����
    /// AreaFixed - �ھڭ��n
    /// </summary>
    public enum AutoResolutionFix { None,WidthFixed, HeightFixed ,AreaFixed};
    public AutoResolutionFix autoResolutionFix;

    [HideInInspector]
    public Vector2 CenterPosition;

    [HideInInspector]
    //�ѪR�� - �̾�1280�����
    public int Resolution = 1280;

    [HideInInspector]
    //�����j�p
    public Vector2 _ScreenSize = new Vector2(Screen.width, Screen.height);

    [HideInInspector]
    //�������
    public float screenRatio;

    //�ƥ���T (BackUp)
    [HideInInspector]
    public Rect _rect_backup;

    [HideInInspector]
    public Color _color_backup;

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
            //�ؼЭ��n��e�����n��v �}�ڸ�
            float sqrt_x = Mathf.Sqrt(area / Screen_area);

            rect.width = float.Parse((_ScreenSize.y * sqrt_x).ToString("0.0000"));
            rect.height = float.Parse((_ScreenSize.x * sqrt_x).ToString("0.0000"));
        }

    }

    #region #�S�Ĩt��

    /// <summary>
    /// �s�yRect�ʵe�ĪG (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">�S�ĵ��c</param>
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
    /// �s�yColor�ʵe�ĪG (Create)
    /// Name - RectTo
    /// </summary>
    /// <param name="effect">�S�ĵ��c</param>
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

    /// <summary>
    /// �s�yScale�ʵe�ĪG (Create)
    /// Name - ScaleTo
    /// </summary>
    /// <param name="effect">�S�ĵ��c</param>
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
           "name", "ScaleTo"));
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



    #endregion �S�Ĩt��
}
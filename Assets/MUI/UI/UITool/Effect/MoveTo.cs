using UnityEngine;
using System.Collections;

/// <summary>
/// 設定MoveTo動畫效果變數
/// 與RectTo不同的是，MoveTo是給予一個移動變量
/// * 在 Texture2D、Lable 下一層
/// </summary>
public class MoveTo : MonoBehaviour
{
    public MEnum.EffectStruct _effectStruct;
    public MEnum.StopEffectStruct _stopEffectStruct;


    //移動向量
    public Vector2 moveV2;
    //移動向量
    //public Vector2 rect;
    //顏色變化
    //public Color color;
    //放大倍率
    //public Vector2 scale;
    //持續時間
    public float time;
    //延遲時間
    public float delay;
    //Ease方式
    public MEnum.EaseType easeType;
    //循環方式
    public MEnum.loopType looptype;

    //特效開始延遲時間
    public float EffectStartDelay;
    

    //物件被Disable時是否回到原本狀態
    public MEnum.ResetWhenDisable _resetWhenDisable;
    //特效結束時 是否 回到原本狀態
    public MEnum.ResetWhenEffectDone _resetWhenEffectDone;
    //特效結束時 物件Disable
    public MEnum.DisableWhenEffectDone _disableWhenEffectDone;

    public float ResetWhenEffectDone_TimeOffset;
    private Rect newRect;

    // Use this for initialization
    void Start()
    {



    }

    void OnEnable()
    {
        SetEffectStartCoroutine();
        //建立當特效結束協程
        SetEffectDoneCoroutine();
    }

    /// <summary>
    /// 特效開始協程
    /// </summary>
    void SetEffectStartCoroutine()
    {
        StartCoroutine(WhenEffectStart(this.EffectStartDelay));
    }


    /// <summary>
    /// 特效結束協程
    /// </summary>
    void SetEffectDoneCoroutine()
    {
        float delaytime = time + delay;
        if (looptype == MEnum.loopType.pingPong) delaytime *= 2;
        StartCoroutine(WhenEffectDone(delaytime + ResetWhenEffectDone_TimeOffset + this.EffectStartDelay));
    }


    IEnumerator WhenEffectStart(float delay)
    {
        yield return new WaitForSeconds(delay);


        //取得當前rect
        newRect = new Rect(this.transform.parent.GetComponent<Texture_2D>().rect.x + moveV2.x,
                this.transform.parent.GetComponent<Texture_2D>().rect.y + moveV2.y,
                this.transform.parent.GetComponent<Texture_2D>().rect.width,
                this.transform.parent.GetComponent<Texture_2D>().rect.height);


        _effectStruct.rect = newRect;
        _effectStruct.time = this.time;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
        _effectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

        this.transform.parent.SendMessage("RectTo", _effectStruct, SendMessageOptions.DontRequireReceiver);


    }

    IEnumerator WhenEffectDone(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_resetWhenEffectDone == MEnum.ResetWhenEffectDone.True)
            ResetOrDefine();
        if (_disableWhenEffectDone == MEnum.DisableWhenEffectDone.True)
        {
            ResetOrDefine();
            this.gameObject.SetActive(false);

        }
    }

    void ResetOrDefine()
    {

        _stopEffectStruct.isReset = this.isReset();
        _stopEffectStruct.reDefinePreviousState = this.isReDefinePreviousState();
        _stopEffectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());
        this.transform.parent.SendMessage("StopRectTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);

    }



    bool isReset()
    {
        if (_resetWhenEffectDone >= MEnum.ResetWhenEffectDone.True ||
            _resetWhenDisable >= MEnum.ResetWhenDisable.True)
            return true;
        else
            return false;
    }

    bool isReDefinePreviousState()
    {
        if (_resetWhenEffectDone == MEnum.ResetWhenEffectDone.True_ReDefinePreviousState ||
            _resetWhenDisable == MEnum.ResetWhenDisable.True_ReDefinePreviousState)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 判斷物件是否為UI物件
    /// </summary>
    /// <param name="Object">物件</param>
    /// <returns>T/F</returns>
    bool ChkObjectisUI(Transform Object)
    {
        if (Object.GetComponent<Texture_2D>() ||
            Object.GetComponent<Label>())
            return true;
        else
            return false;
    }

}
using UnityEngine;
using System.Collections;

/// <summary>
/// 設定MoveTo動畫效果變數
/// 與RectTo不同的是，MoveTo是給予一個移動變量
/// * 在 Texture2D、Lable 下一層
/// </summary>
public class MoveTo : MUI_EffectTo
{


    //移動向量
    public Vector2 moveV2;
    private Rect newRect;

    void Update()
    {
        if (this.transform.parent.GetComponent<iTween>())
        {
            if (this.transform.parent.GetComponent<iTween>().percentage >= 1)
            {
                if (_disableWhenEffectDone == MUI_Enum.DisableWhenEffectDone.True)
                {
                    ResetOrDefine();
                    this.gameObject.SetActive(false);
                }
                if (NextEffect) NextEffect.SetActive(true);
            }
        }

    }

    /// <summary>
    /// 特效開始協程
    /// </summary>
    void SetEffectStartCoroutine()
    {
        StartCoroutine(WhenEffectStart(this.EffectStartDelay));
    }

    void OnEnable()
    {
        //錯誤修正
        BugFix();
        //建立特效協程
        SetEffectStartCoroutine();
    }

    void OnDisable()
    {
        if (_resetWhenDisable == MUI_Enum.ResetWhenDisable.True)
            ResetOrDefine();
    }


    void ResetOrDefine()
    {
        _stopEffectStruct.isReset = this.isReset();
        _stopEffectStruct.isReDefine = this.isReDefine();
        _stopEffectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());
        this.transform.parent.SendMessage("StopRectTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator WhenEffectStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        //取得當前rect
        newRect = new Rect(this.transform.parent.GetComponent<MUI_Texture_2D>().rect.x + moveV2.x,
                this.transform.parent.GetComponent<MUI_Texture_2D>().rect.y + moveV2.y,
                this.transform.parent.GetComponent<MUI_Texture_2D>().rect.width,
                this.transform.parent.GetComponent<MUI_Texture_2D>().rect.height);

        _effectStruct.rect = newRect;
        _effectStruct.time = this.time;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
        _effectStruct.ignoretimescale = this.ignoretimescale;
        _effectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

        this.transform.parent.SendMessage("RectTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
    }

}
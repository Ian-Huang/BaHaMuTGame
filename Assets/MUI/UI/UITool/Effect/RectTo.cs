using UnityEngine;
using System.Collections;

/// <summary>
/// 設定RectTo動畫效果變數
/// 目前以這為主 其他慢慢修改
/// 尚待解決 ResetWhenEffectDone 的秒數與實際運行秒數不一致
/// </summary>
public class RectTo : MUI_EffectTo
{
    //位置與大小
    public Rect rect;

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

        _effectStruct.rect = this.rect;
        _effectStruct.time = this.time;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
        _effectStruct.ignoretimescale = this.ignoretimescale;
        _effectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

        this.transform.parent.SendMessage("RectTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
    }

 

}
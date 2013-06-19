using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/05/08    建置
/// 13/06/19    重新定義名稱 MUI_LayoutCenterScaleTo

#endregion
/// <summary>
/// 動畫 - 動態更變 Rect 變數
/// </summary>
/// * 以目前Rect變數去計算新的Rect來製造移動效果
/// ** Effect共同說明放置在MUI_Effect
public class MUI_LayoutCenterScaleTo : MUI_Effect
{
    //放大向量
    public Vector2 scaleV2;
    public MUI_Texture_2D Layout_MUIRect;

    void Update()
    {
        foreach (Transform child in this.transform.parent.transform)
        {
            if (Check_isMUI(child))
            {
                if (child.GetComponent<iTween>())
                {
                    //當ITween執行結束(percentage = 1)
                    if (child.GetComponent<iTween>().percentage >= 1)
                    {
                        if (_disableWhenEffectDone == MUI_Enum.DisableWhenEffectDone.True)
                        {
                            this.gameObject.SetActive(false);
                        }
                        if (NextEffect) NextEffect.SetActive(true);
                    }
                }
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
        foreach (Transform child in this.transform.parent.transform)
        {
            if (Check_isMUI(child))
            {
                if (_resetWhenDisable == MUI_Enum.ResetWhenDisable.True)
                    ResetOrReDefine(child);
            }
        }

    }

    void ResetOrReDefine(Transform child)
    {
        _stopEffectStruct.isReset = this.isReset();
        _stopEffectStruct.isReDefine = this.isReDefine();
        _stopEffectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());
        child.SendMessage("StopLayoutScaleTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator WhenEffectStart(float delay)
    {
        foreach (Transform child in this.transform.parent.transform)
        {
            if (Check_isMUI(child))
            {
                yield return new WaitForSeconds(delay);

                _effectStruct.scale = this.scaleV2;
                _effectStruct.time = this.time;
                _effectStruct.delay = this.delay;
                _effectStruct.easeType = this.easeType;
                _effectStruct.looptype = this.looptype;
                _effectStruct.ignoretimescale = this.ignoretimescale;
                _effectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

                child.SendMessage("LayoutScaleTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    /// <summary>
    /// 判斷物件是否為UI物件
    /// </summary>
    /// <param name="Object">物件</param>
    /// <returns>T/F</returns>
    bool Check_isMUI(Transform Object)
    {
        if (Object.GetComponent<MUI_Texture_2D>())
        {
            Object.GetComponent<MUI_Texture_2D>().LayoutCenterPosition = Layout_MUIRect.CenterPosition;
            return true;
        }
        else if (Object.GetComponent<MUI_Label>())
        {
            Object.GetComponent<MUI_Label>().LayoutCenterPosition = Layout_MUIRect.CenterPosition;
            return true;
        }
        else
            return false;
    }


    void SetLayoutCenterPosition()
    {

    }

}
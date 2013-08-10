using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/05/08    建置
/// 13/06/19    重新定義名稱 MUI_MoveTo

#endregion
/// <summary>
/// 動畫 - 動態更變 Rect 變數
/// </summary>
/// * 以目前Rect變數去計算新的Rect來製造移動效果
/// ** Effect共同說明放置在MUI_Effect
public class MUI_LayoutMoveTo : MUI_Effect
{
    //移動向量
    public Vector2 moveV2;
    private Rect newRect;

    void Update()
    {
        if (!isEffectStart) return;

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
        isEffectStart = false;
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
        child.SendMessage("StopRectTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator WhenEffectStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        isEffectStart = true;

        foreach (Transform child in this.transform.parent.transform)
        {
            if (Check_isMUI(child))
            {
                newRect = Get_MUI_Rect(child);
                //取得當前rect
                newRect = new Rect(
                        newRect.x + moveV2.x,
                        newRect.y + moveV2.y,
                        newRect.width,
                        newRect.height
                        );

                _effectStruct.rect = this.newRect;
                _effectStruct.time = this.time;
                _effectStruct.delay = this.delay;
                _effectStruct.easeType = this.easeType;
                _effectStruct.looptype = this.looptype;
                _effectStruct.ignoretimescale = this.ignoretimescale;
                _effectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

                child.SendMessage("RectTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    Rect Get_MUI_Rect(Transform child)
    {
        if (child.GetComponent<MUI_Texture_2D>()) return child.GetComponent<MUI_Texture_2D>().rect;
        if (child.GetComponent<MUI_Label>()) return child.GetComponent<MUI_Label>().rect;
        if (child.GetComponent<MUI_AutoType>()) return child.GetComponent<MUI_AutoType>().rect;
        else return newRect;
    }

    /// <summary>
    /// 判斷物件是否為UI物件
    /// </summary>
    /// <param name="Object">物件</param>
    /// <returns>T/F</returns>
    bool Check_isMUI(Transform Object)
    {
        if (Object.GetComponent<MUI_Texture_2D>() ||
            Object.GetComponent<MUI_Label>() ||
            Object.GetComponent<MUI_AutoType>())
            return true;
        else
            return false;
    }

}
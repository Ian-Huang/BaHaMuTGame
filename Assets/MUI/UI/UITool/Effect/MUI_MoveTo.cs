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
public class MUI_MoveTo : MUI_Effect
{
    //移動向量
    public Vector2 moveV2;
    private Rect newRect;

    void Update()
    {
        if (this.transform.parent.GetComponent<iTween>())
        {
            //當ITween執行結束(percentage = 1)
            if (this.transform.parent.GetComponent<iTween>().percentage >= 1)
            {
                if (_disableWhenEffectDone == MUI_Enum.DisableWhenEffectDone.True)
                {
                    ResetOrReDefine();
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
            ResetOrReDefine();
    }

    void ResetOrReDefine()
    {
        _stopEffectStruct.isReset = this.isReset();
        _stopEffectStruct.isReDefine = this.isReDefine();
        _stopEffectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());
        this.transform.parent.SendMessage("StopRectTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator WhenEffectStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        newRect = Get_MUI_Rect();
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

        this.transform.parent.SendMessage("RectTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
    }

    Rect Get_MUI_Rect()
    {
        if (this.transform.parent.GetComponent<MUI_Texture_2D>())   return this.transform.parent.GetComponent<MUI_Texture_2D>().rect;
        if (this.transform.parent.GetComponent<MUI_Label>())        return this.transform.parent.GetComponent<MUI_Label>().rect;
        else return newRect;
    }
}
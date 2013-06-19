using UnityEngine;
using System.Collections;



#region �����������������������������������������������ץ���������������������������������������������������
/// 13/05/08    �ظm
/// 13/06/19    ���s�w�q�W�� MUI_FontSizeTo

#endregion
/// <summary>
/// �ʵe - �ʺA���� Rect �ܼ�
/// </summary>
/// * �H�ثeRect�ܼƥh�p��s��Rect�ӻs�y���ʮĪG
/// ** Effect�@�P������m�bMUI_Effect
public class MUI_FontSizeTo : MUI_Effect
{
    //�r���j�p
    public int fontSize;

    void Update()
    {
        if (this.transform.parent.GetComponent<iTween>())
        {
            //��ITween���浲��(percentage = 1)
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
    /// �S�Ķ}�l��{
    /// </summary>
    void SetEffectStartCoroutine()
    {
        StartCoroutine(WhenEffectStart(this.EffectStartDelay));
    }

    void OnEnable()
    {
        //���~�ץ�
        BugFix();
        //�إ߯S�Ĩ�{
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
        this.transform.parent.SendMessage("StopFontSizeTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator WhenEffectStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        _effectStruct.fontSize = this.fontSize;
        _effectStruct.time = this.time;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
        _effectStruct.ignoretimescale = this.ignoretimescale;
        _effectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

        this.transform.parent.SendMessage("FontSizeTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
    }

}



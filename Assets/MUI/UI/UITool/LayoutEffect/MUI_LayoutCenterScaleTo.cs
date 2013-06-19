using UnityEngine;
using System.Collections;

#region �����������������������������������������������ץ���������������������������������������������������
/// 13/05/08    �ظm
/// 13/06/19    ���s�w�q�W�� MUI_LayoutCenterScaleTo

#endregion
/// <summary>
/// �ʵe - �ʺA���� Rect �ܼ�
/// </summary>
/// * �H�ثeRect�ܼƥh�p��s��Rect�ӻs�y���ʮĪG
/// ** Effect�@�P������m�bMUI_Effect
public class MUI_LayoutCenterScaleTo : MUI_Effect
{
    //��j�V�q
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
                    //��ITween���浲��(percentage = 1)
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
    /// �P�_����O�_��UI����
    /// </summary>
    /// <param name="Object">����</param>
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
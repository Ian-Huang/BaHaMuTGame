using UnityEngine;
using System.Collections;

#region �����������������������������������������������ץ���������������������������������������������������
/// 13/05/08    �ظm
/// 13/06/19    ���s�w�q�W�� MUI_MoveTo

#endregion
/// <summary>
/// �ʵe - �ʺA���� Rect �ܼ�
/// </summary>
/// * �H�ثeRect�ܼƥh�p��s��Rect�ӻs�y���ʮĪG
/// ** Effect�@�P������m�bMUI_Effect
public class MUI_LayoutMoveTo : MUI_Effect
{
    //���ʦV�q
    public Vector2 moveV2;
    private Rect newRect;

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
        child.SendMessage("StopRectTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator WhenEffectStart(float delay)
    {
        foreach (Transform child in this.transform.parent.transform)
        {
            if (Check_isMUI(child))
            {
                yield return new WaitForSeconds(delay);

                newRect = Get_MUI_Rect(child);
                //���o��erect
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
        else return newRect;
    }

    /// <summary>
    /// �P�_����O�_��UI����
    /// </summary>
    /// <param name="Object">����</param>
    /// <returns>T/F</returns>
    bool Check_isMUI(Transform Object)
    {
        if (Object.GetComponent<MUI_Texture_2D>() ||
            Object.GetComponent<MUI_Label>())
            return true;
        else
            return false;
    }

}
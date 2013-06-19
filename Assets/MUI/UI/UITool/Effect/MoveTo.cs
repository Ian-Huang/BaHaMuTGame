using UnityEngine;
using System.Collections;

/// <summary>
/// �]�wMoveTo�ʵe�ĪG�ܼ�
/// �PRectTo���P���O�AMoveTo�O�����@�Ӳ����ܶq
/// * �b Texture2D�BLable �U�@�h
/// </summary>
public class MoveTo : MUI_EffectTo
{


    //���ʦV�q
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

        //���o��erect
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
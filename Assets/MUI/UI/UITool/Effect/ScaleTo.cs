using UnityEngine;
using System.Collections;

/// <summary>
/// �]�wScaleTo�ʵe�ĪG�ܼ�
/// </summary>
public class ScaleTo : MUI_EffectTo
{

    //��j���v
    public Vector2 scale = new Vector2(1, 1);

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
        this.transform.parent.SendMessage("StopScaleTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator WhenEffectStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        _effectStruct.scale = this.scale;
        _effectStruct.time = this.time;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
        _effectStruct.ignoretimescale = this.ignoretimescale;
        _effectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

        this.transform.parent.SendMessage("ScaleTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
    }



}
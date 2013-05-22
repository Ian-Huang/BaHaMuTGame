using UnityEngine;
using System.Collections;

/// <summary>
/// �]�wRectTo�ʵe�ĪG�ܼ�
/// �ثe�H�o���D ��L�C�C�ק�
/// �|�ݸѨM ResetWhenEffectDone ����ƻP��ڹB���Ƥ��@�P
/// </summary>
public class RectTo : MonoBehaviour
{
    public MEnum.EffectStruct _effectStruct;
    public MEnum.StopEffectStruct _stopEffectStruct;

    //��m�P�j�p
    public Rect rect;
    //�C���ܤ�
    //public Color color;
    //��j���v
    //public Vector2 scale;
    //����ɶ�
    public float time;
    //����ɶ�
    public float delay;
    //Ease�覡
    public MEnum.EaseType easeType;
    //�`���覡
    public MEnum.loopType looptype;

    //�S�Ķ}�l����ɶ�
    public float EffectStartDelay;

    public bool on;
    float time2;

    private bool ignoretimescale;

    //����QDisable�ɬO�_�^��쥻���A
    public MEnum.ResetWhenDisable _resetWhenDisable;
    //�S�ĵ����� �O�_ �^��쥻���A
    public MEnum.ResetWhenEffectDone _resetWhenEffectDone;
    public float ResetWhenEffectDone_TimeOffset;
    //�S�ĵ����� ����Disable
    public MEnum.DisableWhenEffectDone _disableWhenEffectDone;



    void Update()
    {
        if (on)
            time2 += Time.deltaTime;
    }

    //���~�ץ��P�קK
    void BugFix()
    {
        //* �ھ� Issue 72 
        //ITween�ϥ�delay��IgnoreTimeScale�L�� �A �ҥH��delay�j��0 �|�ϥγQTimeScale�v�T���禡 
        //https://code.google.com/p/itween/issues/detail?id=72

        if (delay > 0)
            ignoretimescale = false;
        else
            ignoretimescale = true;
    }
    void OnEnable()
    {
        //���~�ץ�
        BugFix();
        //�إ߯S�Ĩ�{
        SetEffectStartCoroutine();
        //�إ߷�S�ĵ�����{
        SetEffectDoneCoroutine();
    }

    /// <summary>
    /// �S�Ķ}�l��{
    /// </summary>
    void SetEffectStartCoroutine()
    {
        StartCoroutine(WhenEffectStart(this.EffectStartDelay));
    }


    /// <summary>
    /// �S�ĵ�����{
    /// </summary>
    void SetEffectDoneCoroutine()
    {
        float delaytime = time + delay;
        if (looptype == MEnum.loopType.pingPong) delaytime *= 2;
        StartCoroutine(WhenEffectDone(delaytime + ResetWhenEffectDone_TimeOffset + this.EffectStartDelay));
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

    IEnumerator WhenEffectDone(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_resetWhenEffectDone == MEnum.ResetWhenEffectDone.True)
            ResetOrDefine();
        if (_disableWhenEffectDone == MEnum.DisableWhenEffectDone.True)
        {
            ResetOrDefine();
            this.gameObject.SetActive(false);
        }

    }

    void ResetOrDefine()
    {

        _stopEffectStruct.isReset = this.isReset();
        _stopEffectStruct.reDefinePreviousState = this.isReDefinePreviousState();
        _stopEffectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());
        this.transform.parent.SendMessage("StopRectTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);

    }



    bool isReset()
    {
        if (_resetWhenEffectDone >= MEnum.ResetWhenEffectDone.True ||
            _resetWhenDisable >= MEnum.ResetWhenDisable.True)
            return true;
        else
            return false;
    }

    bool isReDefinePreviousState()
    {
        if (_resetWhenEffectDone == MEnum.ResetWhenEffectDone.True_ReDefinePreviousState ||
            _resetWhenDisable == MEnum.ResetWhenDisable.True_ReDefinePreviousState)
            return true;
        else
            return false;
    }


 

}
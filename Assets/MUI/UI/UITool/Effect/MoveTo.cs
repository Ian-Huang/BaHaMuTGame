using UnityEngine;
using System.Collections;

/// <summary>
/// �]�wMoveTo�ʵe�ĪG�ܼ�
/// �PRectTo���P���O�AMoveTo�O�����@�Ӳ����ܶq
/// * �b Texture2D�BLable �U�@�h
/// </summary>
public class MoveTo : MonoBehaviour
{
    public MEnum.EffectStruct _effectStruct;
    public MEnum.StopEffectStruct _stopEffectStruct;


    //���ʦV�q
    public Vector2 moveV2;
    //���ʦV�q
    //public Vector2 rect;
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
    

    //����QDisable�ɬO�_�^��쥻���A
    public MEnum.ResetWhenDisable _resetWhenDisable;
    //�S�ĵ����� �O�_ �^��쥻���A
    public MEnum.ResetWhenEffectDone _resetWhenEffectDone;
    //�S�ĵ����� ����Disable
    public MEnum.DisableWhenEffectDone _disableWhenEffectDone;

    public float ResetWhenEffectDone_TimeOffset;
    private Rect newRect;

    // Use this for initialization
    void Start()
    {



    }

    void OnEnable()
    {
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


        //���o��erect
        newRect = new Rect(this.transform.parent.GetComponent<Texture_2D>().rect.x + moveV2.x,
                this.transform.parent.GetComponent<Texture_2D>().rect.y + moveV2.y,
                this.transform.parent.GetComponent<Texture_2D>().rect.width,
                this.transform.parent.GetComponent<Texture_2D>().rect.height);


        _effectStruct.rect = newRect;
        _effectStruct.time = this.time;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
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

    /// <summary>
    /// �P�_����O�_��UI����
    /// </summary>
    /// <param name="Object">����</param>
    /// <returns>T/F</returns>
    bool ChkObjectisUI(Transform Object)
    {
        if (Object.GetComponent<Texture_2D>() ||
            Object.GetComponent<Label>())
            return true;
        else
            return false;
    }

}
using UnityEngine;
using System.Collections;

/// <summary>
/// �]�wScaleTo�ʵe�ĪG�ܼ�
/// </summary>
public class ScaleTo : MonoBehaviour
{
    public MEnum.EffectStruct _effectStruct;
    public MEnum.StopEffectStruct _stopEffectStruct;

    //��m�P�j�p
    //public Rect rect;
    //�C���ܤ�
    //public Color color;
    //��j���v
    public Vector2 scale = new Vector2(1, 1);
    //����ɶ�
    public float time;
    //����ɶ�
    public float delay;
    //Ease�覡
    public MEnum.EaseType easeType;
    //�`���覡
    public MEnum.loopType looptype;
    //�S�ĵ����ɬO�_�^��쥻���A
    public bool ResetAfterEffectDone;
    public float ResetAfterEffectDone_TimeOffset;
    //����QDisable�ɬO�_�^��쥻���A
    public bool ResetAfterDisable;
    //�S�ĵ����� ����Disable
    public bool DisableWhenEffectDone;

    // Use this for initialization
    void Start()
    {

    }

    IEnumerator Recover(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }

    IEnumerator SendMessage()
    {
        yield return new WaitForSeconds(0);
        this.transform.parent.SendMessage("ScaleTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
    }


    void OnEnable()
    {
        //SendMessage("BackUp", _effectStruct, SendMessageOptions.DontRequireReceiver);

        _effectStruct.scale = this.scale;
        _effectStruct.time = this.time;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
        _effectStruct.hashcode = string.Format("{0:X}",this.GetHashCode());


        if (ResetAfterEffectDone)
        {
            float delaytime = time + delay;
            if (looptype == MEnum.loopType.pingPong) delaytime *= 2;
            StartCoroutine(Recover(delaytime + ResetAfterEffectDone_TimeOffset));
        }

        StartCoroutine(SendMessage());

    }

    void OnDisable()
    {
        if (ResetAfterEffectDone || ResetAfterDisable)
            _stopEffectStruct.isReset = true;

        _stopEffectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

        this.SendMessage("StopScaleTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
        this.transform.parent.SendMessage("StopScaleTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

}
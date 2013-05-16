using UnityEngine;
using System.Collections;

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
    //�S�ĵ����ɬO�_�^��쥻���A
    public bool ResetAfterEffectDone;
    public float ResetAfterEffectDone_TimeOffset;
    //����QDisable�ɬO�_�^��쥻���A
    public bool ResetAfterDisable;

    // Use this for initialization
    void Start()
    {

    }

    IEnumerator Recover(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        _effectStruct.rect = this.rect;
        _effectStruct.time = this.time;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
        _effectStruct.hashcode = string.Format("{0:X}",this.GetHashCode());

        this.SendMessage("RectTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
        this.transform.parent.SendMessage("RectTo", _effectStruct, SendMessageOptions.DontRequireReceiver);

        if (ResetAfterEffectDone)
        {
            float delaytime = time + delay;
            if (looptype == MEnum.loopType.pingPong)
                delaytime *= 2;
            StartCoroutine(Recover(delaytime + ResetAfterEffectDone_TimeOffset));
        }

    }

    void OnDisable()
    {
        if( ResetAfterEffectDone || ResetAfterDisable)
            _stopEffectStruct.isReset = true;

        _stopEffectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

        this.SendMessage("StopRectTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
        this.transform.parent.SendMessage("StopRectTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

}
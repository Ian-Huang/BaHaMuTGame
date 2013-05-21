using UnityEngine;
using System.Collections;

/// <summary>
/// �]�wMoveTo�ʵe�ĪG�ܼ�
/// �PRectTo���P���O�AMoveTo�O�����@�Ӳ����ܶq
/// </summary>
public class LayoutScaleTo : MonoBehaviour
{
    public MEnum.EffectStruct _effectStruct;
    public MEnum.StopEffectStruct _stopEffectStruct;

    //�C���ܤ�
    //public Color color;
    //��j���v
    public Vector2 scale;
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
        foreach (Transform child in this.transform.parent.transform)
        {
            if (child.GetComponent<Texture_2D>())
                this.gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {

        foreach (Transform child in this.transform.parent.transform)
        {
            if (child.GetComponent<Texture_2D>())
            {

                _effectStruct.scale = this.scale;
                _effectStruct.time = this.time;
                _effectStruct.delay = this.delay;
                _effectStruct.easeType = this.easeType;
                _effectStruct.looptype = this.looptype;
                _effectStruct.hashcode = string.Format("{0:X} L", this.GetHashCode());

                child.SendMessage("ScaleTo", _effectStruct, SendMessageOptions.DontRequireReceiver);


                if (ResetAfterEffectDone)
                {
                    float delaytime = time + delay;
                    if (looptype == MEnum.loopType.pingPong)
                        delaytime *= 2;
                    StartCoroutine(Recover(delaytime + ResetAfterEffectDone_TimeOffset));
                }
            }
        }

        

        
    }

    void OnDisable()
    {

        foreach (Transform child in this.transform.parent.transform)
        {
            if (child.GetComponent<Texture_2D>())
            {
                if (ResetAfterEffectDone || ResetAfterDisable)
                    _stopEffectStruct.isReset = true;

                _stopEffectStruct.hashcode = string.Format("{0:X} L", this.GetHashCode());

                child.SendMessage("StopScaleTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
            }
        }
        
         
    }

}
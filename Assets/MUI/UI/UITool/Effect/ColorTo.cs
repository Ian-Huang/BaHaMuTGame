using UnityEngine;
using System.Collections;

public class ColorTo : MonoBehaviour
{
    public MEnum.EffectStruct _effectStruct;

    //位置與大小
    //public Rect rect;
    //持續時間
    public float time;
    //顏色變化
    public Color color;
    //延遲時間
    public float delay;
    //Ease方式
    public MEnum.EaseType easeType;
    //循環方式
    public MEnum.loopType looptype;
    //特效結束時是否回到原本狀態
    public bool ResetAfterEffectDone;
    public float ResetAfterEffectDone_TimeOffset;


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
        _effectStruct.time = this.time;
        _effectStruct.color = this.color;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
        this.SendMessage("ColorTo", _effectStruct, SendMessageOptions.DontRequireReceiver);
        this.transform.parent.SendMessage("ColorTo", _effectStruct, SendMessageOptions.DontRequireReceiver);

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
        this.SendMessage("StopColorTo", ResetAfterEffectDone, SendMessageOptions.DontRequireReceiver);
        this.transform.parent.SendMessage("StopColorTo", ResetAfterEffectDone, SendMessageOptions.DontRequireReceiver);
    }

    
}
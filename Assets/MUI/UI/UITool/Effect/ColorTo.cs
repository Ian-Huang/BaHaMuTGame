using UnityEngine;
using System.Collections;

public class ColorTo : MonoBehaviour
{
    public MEnum.EffectStruct _effectStruct;
    public MEnum.StopEffectStruct _stopEffectStruct;

    //位置與大小
    //public Rect rect;
    //顏色變化
    public Color color;
    //放大倍率
    //public Vector2 scale;
    //持續時間
    public float time;
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
        _effectStruct.color = this.color;
        _effectStruct.time = this.time;
        _effectStruct.delay = this.delay;
        _effectStruct.easeType = this.easeType;
        _effectStruct.looptype = this.looptype;
        _effectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

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
        _stopEffectStruct.isReset = ResetAfterEffectDone;
        _stopEffectStruct.hashcode = string.Format("{0:X}", this.GetHashCode());

        this.SendMessage("StopColorTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
        this.transform.parent.SendMessage("StopColorTo", _stopEffectStruct, SendMessageOptions.DontRequireReceiver);
    }

    
}
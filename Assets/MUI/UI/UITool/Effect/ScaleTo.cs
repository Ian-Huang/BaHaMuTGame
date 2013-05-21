using UnityEngine;
using System.Collections;

/// <summary>
/// 設定ScaleTo動畫效果變數
/// </summary>
public class ScaleTo : MonoBehaviour
{
    public MEnum.EffectStruct _effectStruct;
    public MEnum.StopEffectStruct _stopEffectStruct;

    //位置與大小
    //public Rect rect;
    //顏色變化
    //public Color color;
    //放大倍率
    public Vector2 scale = new Vector2(1, 1);
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
    //物件被Disable時是否回到原本狀態
    public bool ResetAfterDisable;
    //特效結束時 物件Disable
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
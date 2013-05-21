using UnityEngine;
using System.Collections;

/// <summary>
/// 設定MoveTo動畫效果變數
/// 與RectTo不同的是，MoveTo是給予一個移動變量
/// </summary>
public class LayoutScaleTo : MonoBehaviour
{
    public MEnum.EffectStruct _effectStruct;
    public MEnum.StopEffectStruct _stopEffectStruct;

    //顏色變化
    //public Color color;
    //放大倍率
    public Vector2 scale;
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
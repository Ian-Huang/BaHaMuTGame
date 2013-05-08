

using UnityEngine;
using System.Collections;

public class Opening : MonoBehaviour {

    //texture2d 與 audioClip 互相配合
    public Texture2D[] texture2d;
    public AudioClip[] audioClip;

    private bool _startFadeINOUT;
    private Rect _rect = new Rect(0,0,Screen.width,Screen.height);
    private Texture2D _Image;
    private Color _GUI_ImageColor = Color.white;

    public enum FadeType { FadeIN, FadeOUT, FadeINFadeOUT };
    public FadeType fadeType;

    public float StartDelay = 2.0F;
    public float FadeTime = 4.0F;
    public float NextDelay = 2.0F;
    public bool DestroyWhenEnd = false;
    public bool AlphaTest = false;
    public string LoadLevelName;


    void Awake()
    {
    }

    IEnumerator Start()
    {
        while (true)
        {
            _GUI_ImageColor.a = 0;
            yield return new WaitForSeconds(StartDelay);
            
            for (int i = 0; i < texture2d.Length ; i++)
            {

                audio.PlayOneShot(audioClip[i]);
                _Image = texture2d[i];

                //FadeType.FadeOUT開頭延遲
                if (fadeType == FadeType.FadeOUT && NextDelay > 0)
                {
                    _GUI_ImageColor.a = 1;
                    _startFadeINOUT = false;
                    yield return new WaitForSeconds(NextDelay);
                }


                //開啟Fade
                _startFadeINOUT = true;
                yield return new WaitForSeconds(FadeTime);


                //FadeType.FadeIN結尾延遲
                if (fadeType == FadeType.FadeIN && NextDelay > 0)
                {
                    _GUI_ImageColor.a = 1;
                    _startFadeINOUT = false;
                    yield return new WaitForSeconds(NextDelay);
                }

                second0to1 = 0;
                _GUI_ImageColor.a = 0;
                
                
            }

            if(LoadLevelName != "")
                Application.LoadLevel(LoadLevelName);


            if (DestroyWhenEnd)
                Destroy(this);


            break;
        }
	}
	



	void Update () 
    {
        if(_startFadeINOUT)
            ChangeAlpha();
	}

    void OnGUI()
    {
        GUI.color = _GUI_ImageColor;

        if (_Image)
            GUI.DrawTexture(_rect, _Image, ScaleMode.StretchToFill, AlphaTest, 0);
    }



    float second0to1 = 0;
    int sign = 1;
    public void ChangeAlpha()
    {


        switch (fadeType)
        {
            case FadeType.FadeINFadeOUT:
                if (second0to1 > 1)
                    sign = -1;

                if (second0to1 < 0)
                    sign = 1;

                second0to1 += Mathf.Clamp01(Time.deltaTime / (FadeTime / 2)) * sign;
                _GUI_ImageColor.a = Mathf.Lerp(0, 1, second0to1);
                break;

            case FadeType.FadeIN:
                second0to1 += Mathf.Clamp01(Time.deltaTime / (FadeTime));
                _GUI_ImageColor.a = Mathf.Lerp(0, 1, second0to1);
                break;

            case FadeType.FadeOUT:
                second0to1 += Mathf.Clamp01(Time.deltaTime / (FadeTime));
                _GUI_ImageColor.a = Mathf.Lerp(1, 0, second0to1);
                break;
        }
    }



}

using UnityEngine;
using System.Collections;

/// <summary>
/// 平板在偵測範圍裡面按鍵的特效
/// </summary>
/// 註冊型
/// 
public class PlatformButtonTypeA : MPlatformButton
{
    //註冊ID
    int fingerID;
    bool FingerIDsubmit;
    static bool submit;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (!ButtonEnable) return;

        //取得偵測範圍
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));

        int i = 0;
        while (i < Input.touchCount)
        {
            //觸碰點在範圍內
            if (rect.Contains(new Vector2(Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y)))
            {
                //ID註冊
                if (!FingerIDsubmit)
                {
                    if (!submit)
                    {
                        if (Input.GetTouch(i).phase == TouchPhase.Began)
                        {
                            FingerIDsubmit = true;
                            fingerID = Input.GetTouch(i).fingerId;
                            submit = true;
                        }
                    }
                }

                if (submit && Input.GetTouch(i).fingerId == fingerID)
                        if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved)
                        {
                            if (EffectObjectWhenPress) EffectObjectWhenPress.SetActive(true);
                            if (EffectObjectWhenRelease) EffectObjectWhenRelease.SetActive(false);
                            isPress[i] = true;
                            submit = false;
                        }
                

                if (isPress[i])
                {

                    if (Input.GetTouch(i).fingerId == fingerID)
                        if (Input.GetTouch(i).phase == TouchPhase.Ended)
                        {
                            if (Event)
                            {
                                GameObject newGameObject = (GameObject)Instantiate(Event);
                                newGameObject.SetActive(true);
                            }
                            if (EffectObjectWhenPress) EffectObjectWhenPress.SetActive(false);
                            if (EffectObjectWhenRelease) EffectObjectWhenRelease.SetActive(true);
                        }
                }
            }
            else  //觸碰點不在範圍內
            {

                if (Input.GetTouch(i).fingerId == fingerID)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Moved && isPress[i])
                    {
                        if (EffectObjectWhenPress) EffectObjectWhenPress.SetActive(false);
                        if (EffectObjectWhenRelease) EffectObjectWhenRelease.SetActive(true);
                        isPress[i] = false;
                        submit = false;
                        FingerIDsubmit = false;
                    }
                    if (Input.GetTouch(i).phase == TouchPhase.Ended)
                    {
                        FingerIDsubmit = false;
                    }
                }

            }
            //i+1
            i++;
        }
    }
}

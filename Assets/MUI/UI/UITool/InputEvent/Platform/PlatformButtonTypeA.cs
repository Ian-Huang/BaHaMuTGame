using UnityEngine;
using System.Collections;

/// <summary>
/// 平板在偵測範圍裡面按鍵的特效
/// </summary>
/// 註冊型
public class PlatformButtonTypeA : MPlatformButton
{
    //註冊ID
    int fingerID;
    bool submit;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //取得偵測範圍
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));

        int i = 0;
        while (i < Input.touchCount)
        {
            if (rect.Contains(new Vector2(Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y)))
            {
                //ID註冊
                if (Input.GetTouch(i).phase == TouchPhase.Began && !submit)
                {
                    fingerID = Input.GetTouch(i).fingerId;
                    submit = true;
                }

                if (submit && Input.GetTouch(i).fingerId == fingerID && (Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Began))
                {
                    if (EffectObjectWhenPress)
                        EffectObjectWhenPress.SetActive(true);
                    if (EffectObjectWhenRelease)
                        EffectObjectWhenRelease.SetActive(false);
                    pressDownPlay[i] = true;
                }
                if (pressDownPlay[i])
                {

                    if (Input.GetTouch(i).fingerId == fingerID && Input.GetTouch(i).phase == TouchPhase.Ended)
                    {
                        if (Event)
                        {
                            GameObject newGameObject = (GameObject)Instantiate(Event);
                            newGameObject.SetActive(true);
                        }
                        if (EffectObjectWhenPress)
                            EffectObjectWhenPress.SetActive(false);
                        if (EffectObjectWhenRelease)
                            EffectObjectWhenRelease.SetActive(true);
                    }
                }
            }
            else
            {
                
                if (Input.GetTouch(i).fingerId == fingerID && Input.GetTouch(i).phase == TouchPhase.Moved && pressDownPlay[i])
                {
                    if (EffectObjectWhenPress)
                        EffectObjectWhenPress.SetActive(false);

                    if (EffectObjectWhenRelease)
                        EffectObjectWhenRelease.SetActive(true);

                    pressDownPlay[i] = false;
                    
                }
                if (Input.GetTouch(i).fingerId == fingerID && Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    submit = false;
                }

            }
            i++;
        }


    }
}

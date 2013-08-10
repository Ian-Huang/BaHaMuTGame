using UnityEngine;
using System.Collections;

/// <summary>
/// 平板在偵測範圍裡面按鍵的特效
/// </summary>
public class PlatformButtonTypeB : MPlatformButton
{
    // Use this for initialization
    new void Start()
    {

    }

    // Update is called once per frame
    new void Update()
    {
        //取得偵測範圍
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));

        int i = 0;
        while (i < Input.touchCount)
        {
            if (rect.Contains(new Vector2(Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y)))
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    if (EffectObjectWhenPress)
                        EffectObjectWhenPress.SetActive(true);
                    if (EffectObjectWhenRelease)
                        EffectObjectWhenRelease.SetActive(false);
                    isPress[i] = true;
                }
                if (isPress[i])
                {

                    if (Input.GetTouch(i).phase == TouchPhase.Ended)
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

                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    if (EffectObjectWhenPress)
                        EffectObjectWhenPress.SetActive(false);

                    if (EffectObjectWhenRelease)
                        EffectObjectWhenRelease.SetActive(true);

                    isPress[i] = false;
                }

            }
            i++;
        }


    }



}

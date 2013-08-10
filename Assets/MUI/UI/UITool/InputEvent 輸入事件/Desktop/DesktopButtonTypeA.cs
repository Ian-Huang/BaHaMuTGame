using UnityEngine;
using System.Collections;

/// <summary>
/// 滑鼠按鍵在偵測範圍裡面按鍵的特效
/// </summary>
public class DesktopButtonTypeA : MDesktopButton
{
    //註冊型Button
    //註冊狀態
    public static bool submit;
    // Use this for initialization
    new void Start()
    {
        //初始化
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (!ButtonEnable) return;

        //取得偵測範圍(Rect)
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));


        if (rect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
        {
            if (!submit)
            {
                if (Input.GetKey(keyCode))
                {
                    if (EffectObjectWhenPress) EffectObjectWhenPress.SetActive(true);
                    if (EffectObjectWhenRelease) EffectObjectWhenRelease.SetActive(false);
                    pressDown = true;
                    submit = true;
                }
            }

            if (pressDown)
            {
                if (Input.GetKeyUp(keyCode))
                {
                    if (Event)
                    {
                        GameObject newGameObject = (GameObject)Instantiate(Event);
                        newGameObject.SetActive(true);
                    }
                    isDone = true;
                    if (EffectObjectWhenPress) EffectObjectWhenPress.SetActive(false);
                    if (EffectObjectWhenRelease) EffectObjectWhenRelease.SetActive(true);
                    submit = false;
                }
            }

        }
        else
        {
            if (pressDown)
            {
                if (Input.GetKeyUp(keyCode))
                {
                    if (EffectObjectWhenPress) EffectObjectWhenPress.SetActive(false);
                    if (EffectObjectWhenRelease) EffectObjectWhenRelease.SetActive(true);
                    pressDown = false;
                    submit = false;
                }
            }
        }
    }
}

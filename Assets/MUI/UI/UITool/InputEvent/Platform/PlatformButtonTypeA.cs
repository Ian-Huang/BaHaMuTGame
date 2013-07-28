using UnityEngine;
using System.Collections;

/// <summary>
/// ���O�b�����d��̭����䪺�S��
/// </summary>
/// ���U��
/// 
public class PlatformButtonTypeA : MPlatformButton
{
    //���UID
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

        //���o�����d��
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));

        int i = 0;
        while (i < Input.touchCount)
        {
            //Ĳ�I�I�b�d��
            if (rect.Contains(new Vector2(Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y)))
            {
                //ID���U
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
            else  //Ĳ�I�I���b�d��
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

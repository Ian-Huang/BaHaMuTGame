using UnityEngine;
using System.Collections;

/// <summary>
/// ���O�b�����d��̭����䪺�S��
/// </summary>
/// ���U��
public class PlatformButtonTypeA : MPlatformButton
{
    //���UID
    int fingerID;
    bool submit;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //���o�����d��
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));

        int i = 0;
        while (i < Input.touchCount)
        {
            if (rect.Contains(new Vector2(Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y)))
            {
                //ID���U
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

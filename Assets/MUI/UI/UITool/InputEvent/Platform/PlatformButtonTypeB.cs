using UnityEngine;
using System.Collections;

/// <summary>
/// ���O�b�����d��̭����䪺�S��
/// </summary>
public class PlatformButtonTypeB : MPlatformButton
{
    private bool[] pressDownPlay = new bool[4];
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
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    if (EffectObjectWhenPress)
                        EffectObjectWhenPress.SetActive(true);
                    if (EffectObjectWhenRelease)
                        EffectObjectWhenRelease.SetActive(false);
                    pressDownPlay[i] = true;
                }
                if (pressDownPlay[i])
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

                    pressDownPlay[i] = false;
                }

            }
            i++;
        }

       
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 50), Input.touchCount.ToString());

        if (Input.touchCount > 0)
        {
            GUI.Label(new Rect(0, 50, 500, 50),rect.ToString());
            GUI.Label(new Rect(0, 70, 500, 50), Input.GetTouch(0).position.x.ToString() + "   " + (Screen.height - Input.GetTouch(0).position.y).ToString());
            GUI.Label(new Rect(0, 90, 500, 50), Input.GetTouch(0).position.x.ToString() + "   " + Input.GetTouch(0).position.y.ToString());
            GUI.Label(new Rect(0, 110, 500, 50), Input.GetTouch(1).position.x.ToString() + "   " + Input.GetTouch(1).position.y.ToString());
        }

    }

}

using UnityEngine;
using System.Collections;

/// <summary>
/// 滑鼠按鍵在偵測範圍裡面按鍵的特效
/// </summary>
public class ButtonTypeB : MonoBehaviour
{

    public Object DisplayObject;
    private Rect rect;

    public GameObject EffectObject;

    public GameObject Event;

    public KeyCode keyCode;

    private bool pressDown;

    private float intervalTime;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //取得偵測範圍
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));


        if (rect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
        {
            if (Input.GetKey(keyCode))
            {
                if (EffectObject)
                    EffectObject.SetActive(true);
                pressDown = true;
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
                    if (EffectObject)
                        EffectObject.SetActive(false);

                }
            }
        }
        else
        {
            if (EffectObject)
                EffectObject.SetActive(false);
 
                pressDown = false;

            
        }

    }
}

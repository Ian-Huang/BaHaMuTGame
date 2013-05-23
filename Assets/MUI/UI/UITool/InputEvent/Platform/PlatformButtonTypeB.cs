using UnityEngine;
using System.Collections;

/// <summary>
/// 平板在偵測範圍裡面按鍵的特效
/// </summary>
public class PlatformButtonTypeB : MonoBehaviour
{

    public Object DisplayObject;
    private Rect rect;

    public GameObject EffectObjectWhenPress;
    public GameObject EffectObjectWhenRelease;

    public GameObject Event;

    public KeyCode keyCode;

    private bool pressDown;

    private float intervalTime;

    Vector2[] torch;



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
            torch[i] = Input.GetTouch(i).position;
            if (rect.Contains(new Vector2(Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y)))
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    if (EffectObjectWhenPress)
                        EffectObjectWhenPress.SetActive(true);
                    if (EffectObjectWhenRelease)
                        EffectObjectWhenRelease.SetActive(false);
                    pressDown = true;
                }
                if (pressDown)
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

                if (EffectObjectWhenPress)
                    EffectObjectWhenPress.SetActive(false);

                if (EffectObjectWhenRelease)
                    EffectObjectWhenRelease.SetActive(true);

                pressDown = false;

            }
        }
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(0, 0, 100, 50), "12132111");

    }
    
}

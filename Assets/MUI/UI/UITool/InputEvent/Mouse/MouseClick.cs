using UnityEngine;
using System.Collections;

/// <summary>
/// 滑鼠按鍵在偵測範圍裡面按鍵的特效
/// </summary>
public class MouseClick : MonoBehaviour
{

    public Object DisplayObject;
    private Rect rect;

    public GameObject EffectObject;

    public GameObject Event;

    public KeyCode keyCode;

    public MEnum.CursorActionType cursorActionType;

    private bool pressDown;

    private float intervalTime;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        rect = (Rect)(DisplayObject.GetType().GetField("_rect").GetValue(DisplayObject));

        //Input.GetTouch(0).phase == TouchPhase.

        //    Camera.mainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0));

        //Camera.main.ScreenPointToRay

        if (rect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
        {
            switch (cursorActionType)
            {
                case MEnum.CursorActionType.KeyDown:

                    if (Input.GetKeyDown(keyCode))
                    {
                        
                        if (Event)
                        {
                            GameObject newGameObject = (GameObject)Instantiate(Event);
                            newGameObject.SetActive(true);
                        }
                        EffectObject.SetActive(true);
                    }

                    if (Input.GetKeyUp(keyCode))
                        EffectObject.SetActive(false);

                    break;

                case MEnum.CursorActionType.KeyUp:

                    if (Input.GetKeyUp(keyCode))
                    {
                       
                        if (Event)
                        {
                            GameObject newGameObject = (GameObject)Instantiate(Event);
                            newGameObject.SetActive(true);
                        }
                        EffectObject.SetActive(true);
                    }


                    break;


                case MEnum.CursorActionType.KeyDownAndUp:

                    if (Input.GetKeyDown(keyCode))
                        pressDown = true;

                    if (pressDown)
                    {
                       
                        if (Input.GetKeyUp(keyCode))
                        { 
                            
                            if (Event)
                            {
                                GameObject newGameObject = (GameObject)Instantiate(Event);
                                newGameObject.SetActive(true);
                            }
                            if( EffectObject)
                                EffectObject.SetActive(true);
                        }
                    }
                    break;

                case MEnum.CursorActionType.DoubleKeyDown:

                    if (Input.GetKeyDown(keyCode))
                    {

                        if (Time.time - intervalTime < 0.3)
                        {
                            
                            if (Event)
                            {
                                GameObject newGameObject = (GameObject)Instantiate(Event);
                                newGameObject.SetActive(true);
                            }
                            EffectObject.SetActive(true);
                        }
                        intervalTime = Time.time;
                    }



                    break;


            }
        }
        else
        {

                            pressDown = false;
            if( EffectObject)
                if (Input.GetKeyUp(keyCode))
                            EffectObject.SetActive(false);



                
            
   
        }

    }
}

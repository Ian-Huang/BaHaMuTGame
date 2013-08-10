using UnityEngine;
using System.Collections;

public class PlatformDebugInfo : MonoBehaviour
{
    public int fontSize;
    void OnGUI()
    {
        GUI.skin.label.fontSize = fontSize;

        GUI.Label(new Rect(0, 0, 500, 50), "TouchCount：" + Input.touchCount.ToString());
        GUI.Label(new Rect(0, 60, 500, 50), "Acceleration：" + Input.acceleration.ToString());

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).fingerId >= 0)
                GUI.Label(new Rect(0, 100 + fontSize * i, 500, 50), "fingerId" + i.ToString() + "：" + Input.GetTouch(i).fingerId.ToString());
        }
        for (int i = Input.touchCount; i < 4; i++)
        {
            GUI.Label(new Rect(0, 100 + fontSize * i, 500, 50), "fingerId" + i.ToString() + "：" + "NULL");
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            GUI.Label(new Rect(0, 200 + fontSize * i, 500, 50), "TouchCount" + i.ToString() + "：" + Input.GetTouch(i).position.x.ToString() + "   " +
                                                    (Screen.height - Input.GetTouch(i).position.y).ToString());
        }

        /*
        for (int i = Input.touchCount; i < 4; i++)
        {
            GUI.Label(new Rect(0, 200 + fontSize * i, 500, 50), "TouchCount" + i.ToString() + "：" + "NULL");
        }
        */
        GUI.Label(new Rect(0, 300, 500, 50), "deltaPosition" + "：" + Input.GetTouch(0).deltaPosition);
        GUI.Label(new Rect(0, 320, 500, 50), "deltaTime" + "：" + Input.GetTouch(0).deltaTime);
        GUI.Label(new Rect(0, 340, 500, 50), "tapCount" + "：" + Input.GetTouch(1).tapCount);

    }
}

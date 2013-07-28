using UnityEngine;
using System.Collections;

public class HightLightMUI : MonoBehaviour
{

    //圖片原本的深度
    private int oldDepth;
    private int newDepth = -1001;
    public MUI_Texture_2D Texture_2D;
    public MDesktopButton[] disableButtons_desktop;
    public MDesktopButton enableButton_desktop;

    public MPlatformButton[] disableButtons_platform;
    public MPlatformButton enableButton_platform;
    // Use this for initialization



#if UNITY_EDITOR
    void Start()
    {
        //change depth
        oldDepth = Texture_2D.depth;
        Texture_2D.depth = newDepth;


        //disable all button active
        foreach (MDesktopButton mbutton in disableButtons_desktop)
        {
            mbutton.isDone = false;
            mbutton.ButtonEnable = false;
        }
        //enable the one button you set
        enableButton_desktop.ButtonEnable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableButton_desktop.isDone)
        {
            TeachingSystem.script.NextPart();
            Texture_2D.depth = oldDepth;
            foreach (MButton mbutton in disableButtons_desktop)
            {
                mbutton.ButtonEnable = true;
            }
        }
    }


}

#elif UNITY_ANDROID
void Start()
    {
        //change depth
        oldDepth = Texture_2D.depth;
        Texture_2D.depth = -newDepth;


        //disable all button active
        foreach (MPlatformButton mbutton in disableButtons_platform)
        {
            mbutton.isDone = false;
            mbutton.ButtonEnable = false;
        }
        //enable the one button you set
        enableButton_platform.ButtonEnable = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (eableButton_platform.isDone)
        {
            TeachingSystem.script.NextPart();
            Texture_2D.depth = oldDepth;
            foreach (MPlatformButton mbutton in disableButtons_platform)
            {
                mbutton.ButtonEnable = true;
            }
        }
    }
#endif

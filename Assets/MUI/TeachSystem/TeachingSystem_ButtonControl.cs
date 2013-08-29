using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/07/28    建置

#endregion
/// <summary>
/// 教學系統 - 將指定Tex2D的深度改變成-10001而需要設定深度門檻為-10000(SetDepthThreshold)
/// </summary>
/// ** 將Texture_2D 指定depth為-1001，而黑幕depth為-1000
public class TeachingSystem_ButtonControl : MonoBehaviour
{

    //圖片原本的深度
    private int oldDepth;
    private static int newDepth = -10001;
    //指定按鈕的Texture2D
    public MUI_Texture_2D Texture_2D;

    public bool isNextPart;
    public bool isNextULT;
    public GameObject NextUltGameObject;
    //不運作的按鈕(桌上型)
    //public MDesktopButton[] disableButtons_desktop;
    //運作的按鈕(桌上型)
    //public MDesktopButton enableButton_desktop;

    //不運作的按鈕(平板型)
    //public MPlatformButton[] disableButtons_platform;
    //運作的按鈕(平板型)
    //public MPlatformButton enableButton_platform;


    // 備註：遮些按鈕繼承MButton　都有共同變數布林　isDone 但是當拖拉物件GameObject物件到MButton型態時，會以第一個出現使用MButton為主
    // 例如　我需要桌上型的MButton　但在Inspetor介面中拖曳進來的可能是平板型的 MButton　，　為了解決這種狀況
    // 只好把兩者切割，讓平板與桌上型的都能執行測試




    void Start()
    {
        //change depth
        oldDepth = Texture_2D.depth;
        Texture_2D.depth = newDepth;

        Texture_2D.gameObject.transform.GetComponent<MDesktopButton>().isDone = false;
        Texture_2D.gameObject.transform.GetComponent<MPlatformButton>().isDone = false;
        ////disable all button active
        //foreach (MDesktopButton mbutton in disableButtons_desktop)
        //{
        //    mbutton.isDone = false;
        //    mbutton.ButtonEnable = false;
        //}
        ////enable the one button you set
        //enableButton_desktop.ButtonEnable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //當按鈕isDone　= true 時　，當按鈕Event產生時
        if (Texture_2D.gameObject.transform.GetComponent<MDesktopButton>().isDone || Texture_2D.gameObject.transform.GetComponent<MPlatformButton>().isDone)
        {
            if (isNextPart)
            {
                //下一個段落
                StartCoroutine(TeachingSystem.script.NextPart(0));
                //圖片回到原本Depth
                Texture_2D.depth = oldDepth;
            }

            if (isNextULT)
            {
                NextUltGameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }

            ////enable all button you set
            //foreach (MButton mbutton in disableButtons_desktop)
            //{
            //    mbutton.ButtonEnable = true;
            //}
        }
    }


}

//#elif UNITY_ANDROID
//void Start()
//    {
//        //change depth
//        oldDepth = Texture_2D.depth;
//        Texture_2D.depth = -newDepth;


//        //disable all button active
//        foreach (MPlatformButton mbutton in disableButtons_platform)
//        {
//            mbutton.isDone = false;
//            mbutton.ButtonEnable = false;
//        }
//        //enable the one button you set
//        enableButton_platform.ButtonEnable = true;
//    }


//    // Update is called once per frame
//    void Update()
//    {
//        if (eableButton_platform.isDone)
//        {
//            TeachingSystem.script.NextPart();
//            Texture_2D.depth = oldDepth;
//            foreach (MPlatformButton mbutton in disableButtons_platform)
//            {
//                mbutton.ButtonEnable = true;
//            }
//        }
//    }
//#endif

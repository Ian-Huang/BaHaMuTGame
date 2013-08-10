//************************************
// key2Label 關鍵字找值輸出成Lable
// * 使用PlayerPrefs
//****************************

using UnityEngine;
using System.Collections;

public class MUI_KeyToText : MonoBehaviour
{
    /// 文字形式
    public MUI_Enum.TextType textType = MUI_Enum.TextType.INT;

    private string Text;
    //用字串當作金鑰來取得資料
    public string Key;


    // Use this for initialization
    void Start()
    {
        //在Start先進行一次Update，可以避免當Enable時 顯示字體會有極短暫閃爍的情形
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        switch (textType)
        {
            case MUI_Enum.TextType.INT:
                Text = PlayerPrefs.GetInt(Key).ToString();
                break;
            case MUI_Enum.TextType.FLOAT:
                Text = PlayerPrefs.GetFloat(Key).ToString();
                break;
            case MUI_Enum.TextType.STRING:
                Text = PlayerPrefs.GetString(Key).ToString();
                break;
        }

        this.GetComponent<MUI_Label>().Text = Text;
    }


}

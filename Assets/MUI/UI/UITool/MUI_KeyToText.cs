//************************************
// key2Label ����r��ȿ�X��Lable
// * �ϥ�PlayerPrefs
//****************************

using UnityEngine;
using System.Collections;

public class MUI_KeyToText : MonoBehaviour
{
    /// ��r�Φ�
    public MUI_Enum.TextType textType = MUI_Enum.TextType.INT;

    private string Text;
    //�Φr���@���_�Ө��o���
    public string Key;


    // Use this for initialization
    void Start()
    {
        //�bStart���i��@��Update�A�i�H�קK��Enable�� ��ܦr��|�����u�Ȱ{�{������
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

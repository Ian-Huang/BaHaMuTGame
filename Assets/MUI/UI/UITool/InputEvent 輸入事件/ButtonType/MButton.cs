using UnityEngine;
using System.Collections;

/// <summary>
/// �ƹ�����b�����d��̭����䪺�S��
/// </summary>
public class MButton : MonoBehaviour
{
    //�����R�A�ܼ� �`�ת��e ��MUI�`�צb���e���W �h����@��(����ܡA������Q�I��)
    public static int DepthThreshold;
    //���s�O�_��@���A
    [HideInInspector]
    public bool ButtonEnable = true;

    [HideInInspector]
    public Object DisplayObject;

    [HideInInspector]
    public Rect rect;

    public GameObject EffectObjectWhenPress;
    public GameObject EffectObjectWhenRelease;
    public GameObject Event;

    [HideInInspector]
    public bool pressDown;

    //��Event�Q�ͦ���AisDone = true �ثe�Φb�оǨt�� �Ψ��˵����s���S���Q���\�ϥ�
    [HideInInspector]
    public bool isDone;


    public void Start()
    {
        GetMUI();
    }

    public void Update()
    {
        //��MUI�`�צb���e���W �h����@��(��ܡA������Q�I��)
        int depth = (int)(DisplayObject.GetType().GetField("depth").GetValue(DisplayObject));
        if (depth > DepthThreshold)
            ButtonEnable = false;
        else
            ButtonEnable = true;

    }
    /// <summary>
    /// �۰ʰ���MUI������ܦ����s
    /// </summary>
    void GetMUI()
    {
        if (this.GetComponent<MUI_Texture_2D>()) DisplayObject = this.GetComponent<MUI_Texture_2D>();
    }
}

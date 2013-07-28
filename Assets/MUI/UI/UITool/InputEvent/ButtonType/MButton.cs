using UnityEngine;
using System.Collections;

/// <summary>
/// �ƹ�����b�����d��̭����䪺�S��
/// </summary>
public class MButton : MonoBehaviour
{
    //���s�O�_��@���A
    public bool ButtonEnable = true;

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


    public void Update()
    {
        if (!ButtonEnable) return;
    }

    //Set the Button enable status
    public void SetButtonEnable(bool status)
    {
        ButtonEnable = status;
    }
}

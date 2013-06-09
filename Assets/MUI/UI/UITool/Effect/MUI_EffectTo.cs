using UnityEngine;
using System.Collections;

public class MUI_EffectTo : MonoBehaviour {

    public MUI_Enum.EffectStruct _effectStruct;
    public MUI_Enum.StopEffectStruct _stopEffectStruct;

    //����ɶ�
    public float time;
    //����ɶ�
    public float delay;
    //�S�Ķ}�l����ɶ�
    public float EffectStartDelay;
    //Ease�覡
    public MUI_Enum.EaseType easeType;
    //�`���覡
    public MUI_Enum.loopType looptype;

    //����QDisable�ɬO�_�^��쥻���A
    public MUI_Enum.ResetWhenDisable _resetWhenDisable;
    //�S�ĵ����� ����Disable
    public MUI_Enum.DisableWhenEffectDone _disableWhenEffectDone;

    [HideInInspector]
    //�O�_�L��TimeScale
    public bool ignoretimescale;

    //�S�ĵ�����U�@�ӮĪG����
    public GameObject NextEffect;


    //���~�ץ��P�קK
    public void BugFix()
    {
        //* �ھ� Issue 72 
        //ITween�ϥ�delay��IgnoreTimeScale�L�� �A �ҥH��delay�j��0 �|�ϥγQTimeScale�v�T���禡 
        //https://code.google.com/p/itween/issues/detail?id=72

        if (delay > 0) ignoretimescale = false;
        else ignoretimescale = true;
    }


    public bool isReset()
    {
        if (_resetWhenDisable >= MUI_Enum.ResetWhenDisable.True)
            return true;
        else
            return false;
    }

    public bool isReDefine()
    {
        if (_resetWhenDisable == MUI_Enum.ResetWhenDisable.True_ReDefine)
            return true;
        else
            return false;
    }

}

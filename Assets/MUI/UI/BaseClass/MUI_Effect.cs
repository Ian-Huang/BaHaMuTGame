using UnityEngine;
using System.Collections;

#region �����������������������������������������������ץ���������������������������������������������������
/// 13/05/08    �ظm
/// 13/06/19    ���s�w�q�W�� MUI_Effect
#endregion
/// <summary>
/// �S�İ� - ��L�S�Ī������O
/// </summary>
/// * �l���O   MUI_RectTo      ��m�P�j�p���S��
/// * �l���O   MUI_ColorTo     �C�⪺�S��
/// * �l���O   MUI_ScaleTo     �j�p���S��(���|���s�ļ�)
/// * �l���O   MUI_MoveTo      �²����ܶq���S��
/// 
/// * ����ITween�����ݭnpercentage�ܼơA�ݭn��ITween.cs��percentage�]��Public ( iTween��oncomplete�Ѽƭȥi�H�ϥΦ������n��"�U�@�Ӱʧ@"��ƶi�h�A�|�ϸ�Ƥ�����z)
/// * DisableWhenEffectDone ��S�ĵ����N����Disable
/// * ResetWhenDisable ����QDisable�N���󪬺AReset�줧�e���A
/// * [�w����] ResetWhenEffectDone ��S�ĵ����N���󪬺AReset�줧�e���A (�i�ѤW�z��̥N��)
public class MUI_Effect : MonoBehaviour {

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

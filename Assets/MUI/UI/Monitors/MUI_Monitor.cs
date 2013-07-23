using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MUI_Monitor
{

    //�R�A�ƥ�
    public static MUI_Monitor script;
    //�Ѽƺʱ�Dictionary
    public static Dictionary<string, float> MonitorDictionary = new Dictionary<string, float>();

    public MUI_Monitor()
    {
        script = this;
    }

    /// <summary>
    /// ��X������T
    /// </summary>
    public void DumpAll()
    {
        foreach (var md in MonitorDictionary)
            Debug.Log("Key�G" + md.Key + "Value�G" + md.Value);
    }

    /// <summary>
    /// �O�_�s�b
    /// </summary>
    /// <param name="key">Key�r��</param>
    /// <returns></returns>
    public bool isValid(string key)
    {
        if (MonitorDictionary.ContainsKey(key))
            return true;
        else
            return false;
    }

    /// <summary>
    /// ���o�ƭ�
    /// </summary>
    /// <param name="key">Key�r��</param>
    /// <returns></returns>
    public float GetValue(string key)
    {
        if (MonitorDictionary.ContainsKey(key))
            return MonitorDictionary[key];
        else
            return 0;
    }

    /// <summary>
    /// �]�w�ƭ�
    /// </summary>
    /// <param name="key">Key�r��</param>
    /// <param name="newValue">���N���ƭ�</param>
    public void SetValue(string key, float newValue)
    {
        if (MonitorDictionary.ContainsKey(key))
            MonitorDictionary[key] = newValue;
    }


    /// <summary>
    /// ���UKey�A�������s�b�bMD���~�[�J
    /// </summary>
    /// <param name="key">Key�r��</param>
    public void SubmitKey(string key)
    {
        if (!isValid(key)) MonitorDictionary.Add(key, 0);
    }
}

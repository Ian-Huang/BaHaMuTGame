using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MUI_Monitor
{

    //繰篈捌セ
    public static MUI_Monitor script;
    //把计菏北Dictionary
    public static Dictionary<string, float> MonitorDictionary = new Dictionary<string, float>();

    public MUI_Monitor()
    {
        script = this;
    }

    /// <summary>
    /// 块场戈癟
    /// </summary>
    public void DumpAll()
    {
        foreach (var md in MonitorDictionary)
            Debug.Log("Key" + md.Key + "Value" + md.Value);
    }

    /// <summary>
    /// 琌
    /// </summary>
    /// <param name="key">Key﹃</param>
    /// <returns></returns>
    public bool isValid(string key)
    {
        if (MonitorDictionary.ContainsKey(key))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 眔计
    /// </summary>
    /// <param name="key">Key﹃</param>
    /// <returns></returns>
    public float GetValue(string key)
    {
        if (MonitorDictionary.ContainsKey(key))
            return MonitorDictionary[key];
        else
            return 0;
    }

    /// <summary>
    /// 砞﹚计
    /// </summary>
    /// <param name="key">Key﹃</param>
    /// <param name="newValue">计</param>
    public void SetValue(string key, float newValue)
    {
        if (MonitorDictionary.ContainsKey(key))
            MonitorDictionary[key] = newValue;
    }


    /// <summary>
    /// 爹Keyゲ斗ぃMDい
    /// </summary>
    /// <param name="key">Key﹃</param>
    public void SubmitKey(string key)
    {
        if (!isValid(key)) MonitorDictionary.Add(key, 0);
    }
}

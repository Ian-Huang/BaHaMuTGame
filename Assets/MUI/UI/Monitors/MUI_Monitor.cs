using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MUI_Monitor
{
    //靜態副本
    public static MUI_Monitor script;
    //參數監控Dictionary
    public static Dictionary<string, float> MonitorDictionary = new Dictionary<string, float>();

    public MUI_Monitor()
    {
        script = this;
    }

    /// <summary>
    /// 輸出全部資訊
    /// </summary>
    public void DumpAll()
    {
        foreach (var md in MonitorDictionary)
            Debug.Log("Key：" + md.Key + "Value：" + md.Value);
    }

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="key">Key字串</param>
    /// <returns></returns>
    public bool isValid(string key)
    {
        if (MonitorDictionary.ContainsKey(key))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 取得數值
    /// </summary>
    /// <param name="key">Key字串</param>
    /// <returns></returns>
    public float GetValue(string key)
    {
        if (MonitorDictionary.ContainsKey(key))
            return MonitorDictionary[key];
        else
            return 0;
    }

    /// <summary>
    /// 設定數值
    /// </summary>
    /// <param name="key">Key字串</param>
    /// <param name="newValue">取代的數值</param>
    public void SetValue(string key, float newValue)
    {
        if (MonitorDictionary.ContainsKey(key))
            MonitorDictionary[key] = newValue;
    }


    /// <summary>
    /// 註冊Key，必須不存在在MD中才加入
    /// </summary>
    /// <param name="key">Key字串</param>
    public void SubmitKey(string key)
    {
        if (!isValid(key)) MonitorDictionary.Add(key, 0);
    }
}

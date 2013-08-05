using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// PlaterPrefs的字典
/// </summary>
public class PlayerPrefsDictionary : MonoBehaviour
{

    public static PlayerPrefsDictionary script;

    //參數監控Dictionary
    public static Dictionary<string, int> PlayerPrefDictionary = new Dictionary<string, int>();


    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        script = this;
        InvokeRepeating("DumpToPlayerPref", 0,0.5F);
        //////////////////////////////////
        //金幣
        PlayerPrefDictionary.Add("Coin", 0);

        //角色類



    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 輸出全部資訊
    /// </summary>
    public void DumpAll()
    {
        foreach (var md in PlayerPrefDictionary)
            Debug.Log("Key：" + md.Key + "Value：" + md.Value);
    }

    /// <summary>
    /// 輸出全部資訊到PlayerPref
    /// </summary>
    public void DumpToPlayerPref()
    {
        foreach (var md in PlayerPrefDictionary)
            PlayerPrefs.SetInt(md.Key.ToString(), md.Value);
    }

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="key">Key字串</param>
    /// <returns></returns>
    public bool isValid(string key)
    {
        if (PlayerPrefDictionary.ContainsKey(key))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 取得數值
    /// </summary>
    /// <param name="key">Key字串</param>
    /// <returns></returns>
    public int GetValue(string key)
    {
        if (PlayerPrefDictionary.ContainsKey(key))
            return PlayerPrefDictionary[key];
        else
            return 0;
    }

    /// <summary>
    /// 設定數值
    /// </summary>
    /// <param name="key">Key字串</param>
    /// <param name="newValue">取代的數值</param>
    public void SetValue(string key, int newValue)
    {
        if (PlayerPrefDictionary.ContainsKey(key))
            PlayerPrefDictionary[key] = newValue;
    }


    /// <summary>
    /// 註冊Key，必須不存在在MD中才加入
    /// </summary>
    /// <param name="key">Key字串</param>
    public void SubmitKey(string key)
    {
        if (!isValid(key)) PlayerPrefDictionary.Add(key, 0);
    }
}



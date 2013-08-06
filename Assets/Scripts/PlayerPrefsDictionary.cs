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
        InvokeRepeating("DumpToPlayerPref", 0, 0.5F);
        //////////////////////////////////
        //金幣
        this.AddValue("Money");

        //角色類
        this.AddValue("BSK_ATK");
        this.AddValue("BSK_DEF");
        this.AddValue("BSK_HP");
        this.AddValue("BSK_ATK_ADD",10);
        this.AddValue("BSK_DEF_ADD",5);
        this.AddValue("BSK_HP_ADD",20);

        this.AddValue("WIZ_ATK");
        this.AddValue("WIZ_DEF");
        this.AddValue("WIZ_HP");
        this.AddValue("WIZ_ATK_ADD");
        this.AddValue("WIZ_DEF_ADD");
        this.AddValue("WIZ_HP_ADD");

        this.AddValue("HUN_ATK");
        this.AddValue("HUN_DEF");
        this.AddValue("HUN_HP");
        this.AddValue("HUN_ATK_ADD");
        this.AddValue("HUN_DEF_ADD");
        this.AddValue("HUN_HP_ADD");

        this.AddValue("KNI_ATK");
        this.AddValue("KNI_DEF");
        this.AddValue("KNI_HP");
        this.AddValue("KNI_ATK_ADD");
        this.AddValue("KNI_DEF_ADD");
        this.AddValue("KNI_HP_ADD");
    }

    // Update is called once per frame
    void Update()
    {
        this.SetValue("Money", 50);
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
        {
            PlayerPrefs.SetInt(md.Key.ToString(), md.Value);
        }
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
    /// 增加數值
    /// </summary>
    /// <param name="key">Key字串</param>
    public void AddValue(string key)
    {
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefDictionary.Add(key.ToString(), 0);
        else
        {
            if (isValid(key))
                SetValue(key.ToString(), GetValue(key));
            else
                PlayerPrefDictionary.Add(key.ToString(), GetValue(key));
        }
    }

    public void AddValue(string key, int value)
    {
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefDictionary.Add(key.ToString(), value);
        else
        {
            if (isValid(key))
                SetValue(key.ToString(), GetValue(key));
            else
                PlayerPrefDictionary.Add(key.ToString(), GetValue(key));
        }
    }
}



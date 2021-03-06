﻿using UnityEngine;
using System.Collections;

public class UI_CompareValueToShowObject : MonoBehaviour
{

    //根據PlayerPref的關卡目前破關數值
    //將指定的物件Enable

    public string PlayerPrefsString;
    public MUI_Label MUI_Label;
    public GameObject CanUseLevel;
    public GameObject CantUseLevel;

    private Color preColor;
    // Use this for initialization
    void Start()
    {
        preColor = MUI_Label.color;
    }

    void Update()
    {
        string s = MUI_Label.Text;
        if (s == "") s = "0";
        if (PlayerPrefsDictionary.script.GetValue(PlayerPrefsString) >= int.Parse(s))
        {
            CanUseLevel.SetActive(true);
            CantUseLevel.SetActive(false);
            MUI_Label.color = preColor;

        }
        else
        {
            CanUseLevel.SetActive(false);
            CantUseLevel.SetActive(true);
            MUI_Label.color = Color.red;
        }
    }
}

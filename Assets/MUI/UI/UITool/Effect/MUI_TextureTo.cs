﻿using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/05/08    建置
/// 13/06/19    重新定義名稱 MUI_TextureTo

#endregion
/// <summary>
/// 換圖效果
/// </summary>
/// Enable時 作用
/// Disable時 不作用
/// 
public class MUI_TextureTo : MonoBehaviour
{
    private MUI_Texture_2D texture_2D;
    private Texture textur_2D_backup;
    public Texture Texture2d;

    //物件被Disable時是否回到原本狀態
    public bool ResetAfterDisable;

    void Awake()
    {
        if (this.gameObject.GetComponent<MUI_Texture_2D>())
            texture_2D = this.gameObject.GetComponent<MUI_Texture_2D>();
        else if (this.transform.parent.GetComponent<MUI_Texture_2D>())
            texture_2D = this.transform.parent.GetComponent<MUI_Texture_2D>();
    }
    // Use this for initialization
    void Start()
    {

    }

    void OnEnable()
    {
        textur_2D_backup = this.texture_2D.Texture2d;
        this.texture_2D.Texture2d = Texture2d;
    }

    void OnDisable()
    {
        if (ResetAfterDisable)
            this.texture_2D.Texture2d = textur_2D_backup;
    }

}
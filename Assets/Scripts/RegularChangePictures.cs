﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 固定頻率的交換圖片Script
/// </summary>
public class RegularChangePictures : MonoBehaviour
{
    public bool isRunChang = true;
    public Texture[] ChangeTextures;
    public float ChangeTextureTime = 0.1f;             //交換時間間隔

    private int currentTextureIndex { get; set; }
    private float addValue { get; set; }

    // Use this for initialization
    void Start()
    {
        this.Reset();
    }

    /// <summary>
    /// 將數值回復成預設值
    /// </summary>
    void Reset()
    {
        this.addValue = 0;
        this.currentTextureIndex = 0;
        this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
    }

    /// <summary>
    /// 改變是否運行Regular Change Picture的狀態
    /// </summary>
    /// <param name="isChange">是或否</param>
    public void ChangeState(bool isChange)
    {
        this.isRunChang = isChange;
        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isRunChang)
        {
            if (this.addValue >= this.ChangeTextureTime)
            {
                this.addValue = 0;

                if ((this.currentTextureIndex + 1) >= this.ChangeTextures.Length)       //歸0，循環
                    this.currentTextureIndex = 0;
                else
                    this.currentTextureIndex++;

                this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
            }
            this.addValue += Time.deltaTime;
        }
    }
}
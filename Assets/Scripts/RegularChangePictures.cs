using UnityEngine;
using System.Collections;

/// <summary>
/// 固定頻率的交換圖片Script
/// </summary>
public class RegularChangePictures : MonoBehaviour
{
    public Texture[] ChangeTextures;
    public float ChangeTime = 0.1f;             //交換時間間隔

    private int currentTextureIndex { get; set; }
    private float addValue { get; set; }

    private bool isChanging { get; set; }

    // Use this for initialization
    void Start()
    {
        this.isChanging = true;
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
        if (isChange)
        {
            this.isChanging = true;
            this.addValue = 0;
            this.currentTextureIndex = 0;
            this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
        }
        else
        {
            this.isChanging = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isChanging)
        {
            if (this.addValue >= this.ChangeTime)
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
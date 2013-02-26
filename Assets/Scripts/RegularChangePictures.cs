using UnityEngine;
using System.Collections;

/// <summary>
/// 固定頻率交換圖片Script
/// </summary>
public class RegularChangePictures : MonoBehaviour
{
    public Texture[] ChangeTextures;
    public float ChangeTime = 0.1f;

    private int currentTexture { get; set; }
    private float addValue { get; set; }

    // Use this for initialization
    void Start()
    {
        this.addValue = this.ChangeTime;  // 一開始就觸發
        this.currentTexture = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.addValue >= this.ChangeTime)
        {
            this.addValue = 0;
            this.renderer.material.mainTexture = this.ChangeTextures[this.currentTexture];

            if ((this.currentTexture + 1) >= this.ChangeTextures.Length)
                this.currentTexture = 0;
            else
                this.currentTexture++;
        }
        this.addValue += Time.deltaTime;
    }
}
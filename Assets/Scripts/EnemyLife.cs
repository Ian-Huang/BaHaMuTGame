using UnityEngine;
using System.Collections;

/// <summary>
/// 控制敵人的血量
/// </summary>
public class EnemyLife : MonoBehaviour
{
    public int TotalLife = 1;                   //物體生命總值

    public Texture[] DeadChangeTextures;        //攻擊的交換圖組
    public float ChangeTextureTime = 0.1f;             //交換時間間隔

    public bool isDead { get; private set; }
    private int currentTextureIndex { get; set; }    
    private float addValue { get; set; }


    // Use this for initialization
    void Start()
    {
        this.isDead = false;
    }

    /// <summary>
    /// 減少物體血量函式
    /// </summary>
    /// <param name="deLife">減少的數值</param>
    public void DecreaseLife(int deLife)
    {
        this.TotalLife -= deLife;
        
        //當生命小於0，刪除物件
        if (!this.isDead && this.TotalLife <= 0)
        {
            this.isDead = true;
            Destroy(this.GetComponent<MoveController>());
            Destroy(this.GetComponent<RegularChangePictures>());
            Destroy(this.GetComponent<EnemyAttackController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isDead)
        {
            if (this.addValue >= this.ChangeTextureTime)
            {
                this.addValue = 0;
                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.DeadChangeTextures.Length)
                {
                    //播完爆炸圖片後，刪除物件
                    Destroy(this.gameObject);
                    return;
                }
                this.renderer.material.mainTexture = this.DeadChangeTextures[this.currentTextureIndex];
            }
            this.addValue += Time.deltaTime;
        }
    }
}
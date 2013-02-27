using UnityEngine;
using System.Collections;

/// <summary>
/// 爆炸物件(法師火球撞擊敵人後產生)
/// </summary>
public class ExplosiveObject : MonoBehaviour
{
    public Texture[] ChangeTextures;
    public float ChangeTime = 0.1f;             //交換時間間隔

    private int currentTextureIndex { get; set; }
    private bool isExplsion { get; set; }
    private float addValue { get; set; }

    void OnTriggerEnter(Collider other)
    {        
        this.isExplsion = true;
        this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
        
        //移除Script，使爆炸位置固定、換圖正常
        Destroy(this.GetComponent<MoveController>());
        Destroy(this.GetComponent<RegularChangePictures>());

        other.GetComponent<EnemyLife>().DecreaseLife(1);
    }

    // Use this for initialization
    void Start()
    {
        this.addValue = 0;
        this.currentTextureIndex = 0;
        this.isExplsion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isExplsion)
        {
            if (this.addValue >= this.ChangeTime)
            {
                this.addValue = 0;
                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.ChangeTextures.Length)
                {
                    //播完爆炸圖片後，刪除物件
                    Destroy(this.gameObject);
                    return;
                }
                this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
            }
            this.addValue += Time.deltaTime;
        }
    }
}
using UnityEngine;
using System.Collections;

/// <summary>
/// 爆炸物件(法師火球撞擊敵人後產生)
/// </summary>
public class ExplosiveObject : MonoBehaviour
{
    public Texture[] ChangeTextures;        //換圖的圖組
    public float ChangeTextureTime = 0.1f;  //貼圖交換時間間隔
    public LayerMask ExplosiveLayer;

    private int currentTextureIndex { get; set; }
    private bool isExplsion { get; set; }
    private float addValue { get; set; }

    void OnTriggerStay(Collider other)
    {
        if (!this.isExplsion)
        {
            if ((this.ExplosiveLayer.value & (int)Mathf.Pow(2, other.gameObject.layer)) != 0)      //判定欲爆炸的Layer
            {
                if (Mathf.Abs(other.transform.position.x - this.transform.position.x) < 1)
                {
                    this.isExplsion = true;
                    this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];

                    //移除Script，使爆炸位置固定、換圖正常
                    Destroy(this.GetComponent<MoveController>());
                    Destroy(this.GetComponent<RegularChangePictures>());

                    //處理不同碰撞物的部分(敵人、主角)
                    if (other.gameObject.layer == GameDefinition.Enemy_Layer)
                        other.GetComponent<EnemyLife>().DecreaseLife(1);
                    else if (other.gameObject.layer == GameDefinition.Role_Layer)
                        print("Role is Attacked");
                }
            }
        }
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
            if (this.addValue >= this.ChangeTextureTime)
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
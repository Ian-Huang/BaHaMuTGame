using UnityEngine;
using System.Collections;

/// <summary>
/// 遠距離(角色/敵人)發射的物件資訊
/// </summary>
public class ShootObjectInfo : MonoBehaviour
{
    public int Damage;
    public GameDefinition.AttackType AttackType;

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
                    if (other.gameObject.layer == (int)GameDefinition.GameLayout.Enemy)
                        other.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.Damage);
                    else if (other.gameObject.layer == (int)GameDefinition.GameLayout.Role)
                        other.GetComponent<RolePropertyInfo>().DecreaseLife(this.Damage);
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
using UnityEngine;
using System.Collections;

/// <summary>
/// 處理敵人的攻擊控制 (未完成)
/// </summary>
public class EnemyAttackController : MonoBehaviour
{
    public float AttackDistance = 2;            //攻擊距離
    public Texture[] AttackChangeTextures;      //攻擊的交換圖組
    public float ChangeTextureTime = 0.1f;      //交換時間間隔
    public int AttackIndex;                     //確認第N張圖的時間跑完後，判定攻擊的索引
    public float AttackMoveSpeed = 0;           //攻擊時的移動速度
    public GameDefinition.AttackMode attackMode;
    public GameObject ShootObject;
    public LayerMask AttackLayer;
   
    private int currentTextureIndex { get; set; }
    private float addValue { get; set; }
    private bool isAttacking { get; set; }

    void OnTriggerStay(Collider other)
    {
        if ((this.AttackLayer.value & (int)Mathf.Pow(2, other.gameObject.layer)) != 0)      //判定攻擊的Layer
        {
            if (!this.isAttacking)
            {
                if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.AttackDistance)
                {
                    this.isAttacking = true;
                    this.renderer.material.mainTexture = this.AttackChangeTextures[this.currentTextureIndex];
                    if (!this.GetComponent<EnemyLife>().isDead)      //判定追蹤的物體是否還存在
                    {
                        this.GetComponent<RegularChangePictures>().ChangeState(false);          //將一般移動的換圖暫停
                        this.GetComponent<MoveController>().ChangeSpeed(this.AttackMoveSpeed);  //改變攻擊時移動的速度
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isAttacking)
        {
            if (this.addValue >= this.ChangeTextureTime)
            {
                this.addValue = 0;

                if (this.currentTextureIndex == this.AttackIndex)   //攻擊判定
                {
                    //待補(敵人攻擊後給於角色的回饋)
                    if (this.attackMode == GameDefinition.AttackMode.Far)
                    {
                        Instantiate(this.ShootObject, 
                            new Vector3(this.transform.position.x,this.transform.position.y,GameDefinition.ShootObject_ZIndex),
                            this.ShootObject.transform.rotation);
                    }
                }

                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.AttackChangeTextures.Length)
                {
                    this.GetComponent<RegularChangePictures>().ChangeState(true);
                    this.Reset();
                    return;
                }
                renderer.material.mainTexture = this.AttackChangeTextures[this.currentTextureIndex];
            }

            this.addValue += Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        //畫出偵測線
        Gizmos.color = Color.black;
        Gizmos.DrawRay(this.transform.position, Vector3.left * this.AttackDistance);
    }

    /// <summary>
    /// 將數值回復成預設值
    /// </summary>
    void Reset()
    {
        this.currentTextureIndex = 0;
        this.addValue = 0;
        this.isAttacking = false;
    }
}
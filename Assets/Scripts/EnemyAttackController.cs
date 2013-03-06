using UnityEngine;
using System.Collections;

/// <summary>
/// 處理敵人的攻擊控制 (未完成)
/// </summary>
public class EnemyAttackController : MonoBehaviour
{
    public float AttackDistance = 2;
    public Texture[] AttackChangeTextures;      //攻擊的交換圖組
    public float ChangeTime = 0.1f;             //交換時間間隔
    public int AttackIndex;                    //確認第N張圖的時間跑完後，判定攻擊的索引
   
    private int currentTextureIndex { get; set; }
    private float addValue { get; set; }
    private bool isAttacking { get; set; }
    
    void OnTriggerStay(Collider other)
    {

        if (!this.isAttacking)
        {
            if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.AttackDistance)
            {
                this.isAttacking = true;
                this.renderer.material.mainTexture = this.AttackChangeTextures[this.currentTextureIndex];
                if (!this.GetComponent<EnemyLife>().isDead)      //判定追蹤的物體是否還存在
                {
                    this.GetComponent<RegularChangePictures>().ChangeState(false);  //將一般移動的換圖暫停
                    this.GetComponent<MoveController>().MovingState(false);         //將一般移動的控制暫停
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
            if (this.addValue >= this.ChangeTime)
            {
                this.addValue = 0;

                if (this.currentTextureIndex == this.AttackIndex)   //攻擊判定
                {
                    //待補(敵人攻擊後給於角色的回饋)
                }

                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.AttackChangeTextures.Length)
                {

                    this.GetComponent<RegularChangePictures>().ChangeState(true);
                    this.GetComponent<MoveController>().MovingState(true);
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
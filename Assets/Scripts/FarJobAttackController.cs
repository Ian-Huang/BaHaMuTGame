using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 遠攻角色的攻擊控制 (分為近距離攻擊、遠距離攻擊) 
/// </summary>
public class FarJobAttackController : MonoBehaviour
{
    public float FarAttackDistance = 13;
    public float NearAttackDistance = 3;

    private GameDefinition.AttackMode attackMode;        //攻擊模式
    public GameDefinition.AttackType AttackType;        //攻擊類型

    public GameObject ShootObject;

    public Texture[] FarAttackChangeTextures;   //遠距離攻擊模式圖組
    public Texture[] NearAttackChangeTextures;   //近距離攻擊模式圖組

    public float[] FarAttackChangeTextureTime;              //交換時間間隔  (對應遠距離攻擊模式圖組)
    public float[] NearAttackChangeTextureTime;              //交換時間間隔  (對應近距離攻擊模式圖組)

    public int FarAttackIndex;                 //確認第N張圖的時間跑完後，判定攻擊的索引  (對應遠距離攻擊模式圖組)
    public int NearAttackIndex;                 //確認第N張圖的時間跑完後，判定攻擊的索引  (對應近距離攻擊模式圖組)

    public LayerMask AttackLayer;

    private int currentTextureIndex { get; set; }         //當前正在使用Texture的index

    private bool isAttacking { get; set; }
    private GameObject detectedEnemyObject { get; set; }        //目前追蹤的敵人
    private float addValue { get; set; }

    private RolePropertyInfo roleInfo { get; set; }

    void OnTriggerStay(Collider other)
    {
        if ((this.AttackLayer.value & (int)Mathf.Pow(2, other.gameObject.layer)) != 0)      //判定攻擊的Layer
        {
            if (!this.isAttacking)
            {
                if (!other.gameObject.GetComponent<EnemyPropertyInfo>().isDead)
                {
                    if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.FarAttackDistance)
                    {
                        if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.NearAttackDistance)
                        {
                            this.attackMode = GameDefinition.AttackMode.NearAttack;
                            this.renderer.material.mainTexture = this.NearAttackChangeTextures[this.currentTextureIndex];
                        }
                        else
                        {
                            this.attackMode = GameDefinition.AttackMode.FarAttck;
                            this.renderer.material.mainTexture = this.FarAttackChangeTextures[this.currentTextureIndex];
                        }
                        this.isAttacking = true;
                        this.detectedEnemyObject = other.gameObject;                        //抓取進入範圍內的敵人
                        this.GetComponent<RegularChangePictures>().ChangeState(false);      //將一般移動的換圖暫停
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //載入角色資訊
        this.roleInfo = this.GetComponent<RolePropertyInfo>();

        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isAttacking)
        {
            //遠距離攻擊模式設定
            if (this.attackMode == GameDefinition.AttackMode.FarAttck)
            {
                if (this.addValue >= this.FarAttackChangeTextureTime[this.currentTextureIndex])
                {
                    this.addValue = 0;

                    if (this.currentTextureIndex == this.FarAttackIndex)   //攻擊判定
                    {
                        if (this.detectedEnemyObject != null)      //判定追蹤的物體是否還存在
                        {
                            GameObject obj = (GameObject)Instantiate(this.ShootObject,
                                new Vector3(this.transform.position.x, this.transform.position.y, GameDefinition.ShootObject_ZIndex),
                                this.ShootObject.transform.rotation
                            );
                            ShootObjectInfo info = obj.GetComponent<ShootObjectInfo>();
                            info.Damage = this.roleInfo.farDamage;
                            info.AttackType = this.AttackType;
                        }
                    }

                    this.currentTextureIndex++;
                    if (this.currentTextureIndex >= this.FarAttackChangeTextures.Length)
                    {
                        this.GetComponent<RegularChangePictures>().ChangeState(true);
                        this.Reset();
                        return;
                    }
                    this.renderer.material.mainTexture = this.FarAttackChangeTextures[this.currentTextureIndex];
                }
            }
            //近距離攻擊模式設定
            else
            {
                if (this.addValue >= this.NearAttackChangeTextureTime[this.currentTextureIndex])
                {
                    this.addValue = 0;

                    if (this.currentTextureIndex == this.NearAttackIndex)   //攻擊判定
                    {
                        if (this.detectedEnemyObject != null)      //判定追蹤的物體是否還存在
                        {
                            this.detectedEnemyObject.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.roleInfo.nearDamage);
                        }
                    }

                    this.currentTextureIndex++;
                    if (this.currentTextureIndex >= this.NearAttackChangeTextures.Length)
                    {
                        this.GetComponent<RegularChangePictures>().ChangeState(true);
                        this.Reset();
                        return;
                    }
                    renderer.material.mainTexture = this.NearAttackChangeTextures[this.currentTextureIndex];
                }
            }

            this.addValue += Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        //畫出偵測線
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.FarAttackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.NearAttackDistance);
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
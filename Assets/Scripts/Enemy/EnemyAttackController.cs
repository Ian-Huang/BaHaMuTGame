using UnityEngine;
using System.Collections;

/// <summary>
/// 處理敵人的攻擊控制 (未完成)
/// </summary>
public class EnemyAttackController : MonoBehaviour
{
    public float AttackDistance;                    //攻擊距離
    public GameDefinition.AttackMode attackMode;
    public GameObject ShootObject;
    public LayerMask AttackLayer;                   //攻擊判定的Layer

    private GameObject detectedRoleObject { get; set; }        //目前追蹤的玩家

    private EnemyPropertyInfo enemyInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;
    private RaycastHit hitData;

    //void OnTriggerStay(Collider other)
    //{
    //    if ((this.AttackLayer.value & (int)Mathf.Pow(2, other.gameObject.layer)) != 0)      //判定攻擊的Layer
    //    {
    //        if (!this.isAttacking)
    //        {
    //            if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.AttackDistance)
    //            {
    //                this.isAttacking = true;
    //                this.renderer.material.mainTexture = this.AttackChangeTextures[this.currentTextureIndex];
    //                if (!this.GetComponent<EnemyPropertyInfo>().isDead)      //判定追蹤的物體是否還存在
    //                {
    //                    this.GetComponent<RegularChangePictures>().ChangeState(false);          //將一般移動的換圖暫停
    //                    this.GetComponent<MoveController>().ChangeSpeed(this.AttackMoveSpeed);  //改變攻擊時移動的速度
    //                    this.detectedRoleObject = other.gameObject;                        //抓取進入範圍內的玩家
    //                }
    //            }
    //        }
    //    }
    //}

    // Use this for initialization
    void Start()
    {
        //載入敵人資訊
        this.enemyInfo = this.GetComponent<EnemyPropertyInfo>();

        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterColliderTriggerDelegate(WeaponHit);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.enemyInfo.isDead)
        {
            if (Physics.Raycast(this.transform.position, Vector3.left, out this.hitData, this.AttackDistance, this.AttackLayer))
            {
                if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                    if (!this.boneAnimation.IsPlaying("attack"))
                        this.boneAnimation.Play("attack");
            }
            else
            {
                if (!this.boneAnimation.IsPlaying("walk"))
                    this.boneAnimation.Play("walk");
            }
        }

        //if (this.isAttacking)
        //{
        //    if (this.addValue >= this.ChangeTextureTime)
        //    {
        //        this.addValue = 0;

        //        if (this.currentTextureIndex == this.AttackIndex)   //攻擊判定
        //        {
        //            //待補(敵人攻擊後給於角色的回饋)
        //            if (this.attackMode == GameDefinition.AttackMode.FarAttck)
        //            {
        //                GameObject obj = (GameObject)Instantiate(this.ShootObject,
        //                    new Vector3(this.transform.position.x, this.transform.position.y, GameDefinition.ShootObject_ZIndex),
        //                    this.ShootObject.transform.rotation);

        //                ShootObjectInfo info = obj.GetComponent<ShootObjectInfo>();
        //                info.Damage = this.enemyInfo.farDamage;
        //            }
        //            else
        //            {
        //                if (this.detectedRoleObject != null)      //判定追蹤的物體是否還存在
        //                {
        //                    this.detectedRoleObject.GetComponent<RolePropertyInfo>().DecreaseLife(this.enemyInfo.nearDamage);
        //                }
        //            }
        //        }

        //        this.currentTextureIndex++;
        //        if (this.currentTextureIndex >= this.AttackChangeTextures.Length)
        //        {
        //            this.GetComponent<RegularChangePictures>().ChangeState(true);
        //            this.Reset();
        //            return;
        //        }
        //        renderer.material.mainTexture = this.AttackChangeTextures[this.currentTextureIndex];
        //    }

        //    this.addValue += Time.deltaTime;
        //}
    }


    /// <summary>
    /// 綁在敵人身上的Collider，觸發攻擊判定
    /// </summary>
    /// <param name="triggerEvent">觸發相關資訊</param>
    public void WeaponHit(SmoothMoves.ColliderTriggerEvent triggerEvent)
    {
        if (triggerEvent.boneName == "weapon" && triggerEvent.triggerType == SmoothMoves.ColliderTriggerEvent.TRIGGER_TYPE.Enter)
        {
            if (((1 << triggerEvent.otherCollider.gameObject.layer) & this.AttackLayer.value) > 0)
            {
                //tag = MainBody
                if (triggerEvent.otherCollider.tag.CompareTo("MainBody") == 0)
                    triggerEvent.otherCollider.GetComponent<RolePropertyInfo>().DecreaseLife(this.enemyInfo.nearDamage);
            }
        }
    }

    void OnDrawGizmos()
    {
        //畫出偵測線
        Gizmos.color = Color.black;
        Gizmos.DrawRay(this.transform.position, Vector3.left * this.AttackDistance);
    }
}
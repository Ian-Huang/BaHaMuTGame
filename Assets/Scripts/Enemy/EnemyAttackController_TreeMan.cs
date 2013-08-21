using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-10
/// Author：Ian
/// Description：
///     敵人攻擊控制器(樹人系列專用(樹人、胖樹人))
/// </summary>
public class EnemyAttackController_TreeMan : MonoBehaviour
{
    private bool isRunning = false;     //是否開始跑
    public float RunSpeed;              //跑步速度

    public float AttackDistance;        //攻擊距離
    public LayerMask AttackLayer;       //攻擊判定的Layer

    private EnemyPropertyInfo enemyInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;
    private RaycastHit hitData;

    void OnTriggerEnter(Collider other)
    {
        // 怪物必須未死亡
        if (!this.enemyInfo.isDead)
        {
            //撞擊角色並造成角色傷害
            if (((1 << other.gameObject.layer) & this.AttackLayer.value) > 0)
            {
                //tag = MainBody
                if (other.collider.tag.CompareTo("MainBody") == 0)
                {
                    //創建 撞擊特效BoneAnimation
                    SmoothMoves.BoneAnimation obj = (SmoothMoves.BoneAnimation)Instantiate(GameManager.script.EffectAnimationObject);
                    obj.mLocalTransform.position = other.ClosestPointOnBounds(this.transform.position) - Vector3.forward;
                    obj.playAutomatically = false;
                    //隨機撥放 1 或 2 動畫片段
                    if (Random.Range(0, 2) == 0)
                        obj.Play("撞擊特效01");
                    else
                        obj.Play("撞擊特效02");

                    //給予角色傷害
                    other.GetComponent<RolePropertyInfo>().DecreaseLife(enemyInfo.damage);
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //載入敵人資訊
        this.enemyInfo = this.GetComponent<EnemyPropertyInfo>();

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        GameManager.script.RegisterBoneAnimation(this.boneAnimation);   //註冊BoneAnimation，GameManager統一管理
    }

    // Update is called once per frame
    void Update()
    {
        //從GameManager 確認BoneAnimation的狀態
        if (GameManager.script.GetBoneAnimationState(this.boneAnimation))
        {
            // 怪物必須未死亡
            if (!this.enemyInfo.isDead)
            {
                //確認樹人並未開始"Run"
                if (!this.isRunning)
                {
                    if (Physics.Raycast(this.transform.position, Vector3.left, out this.hitData, this.AttackDistance, this.AttackLayer))
                    {
                        //tag = MainBody
                        if (this.hitData.collider.tag.CompareTo("MainBody") == 0)
                        {
                            //進入視線範圍，偵測到角色，開始"run"，並增加移動速度
                            this.isRunning = true;
                            this.GetComponent<MoveController>().ChangeSpeed(this.RunSpeed);
                        }
                    }
                }
                else
                {
                    if (!this.boneAnimation.IsPlaying("run"))
                        this.boneAnimation.Play("run");
                }
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
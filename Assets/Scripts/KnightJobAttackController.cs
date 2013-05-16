using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 近戰角色的攻擊控制
/// </summary>
public class KnightJobAttackController : MonoBehaviour
{
    public float AttackDistance = 2;

    public Texture[] ChangeTextureGroup1;   //圖組1
    public Texture[] ChangeTextureGroup2;   //圖組2

    public float[] ChangeTextureTimeGroup1;              //交換時間間隔  (對應圖組1)
    public float[] ChangeTextureTimeGroup2;              //交換時間間隔  (對應圖組2)

    public int AttackIndex1;                 //確認第N張圖的時間跑完後，判定攻擊的索引  (對應圖組1)
    public int AttackIndex2;                 //確認第N張圖的時間跑完後，判定攻擊的索引  (對應圖組2)

    public LayerMask AttackLayer;

    private int currentTextureIndex { get; set; }         //當前正在使用Texture的index
    private int currentGroupIndex { get; set; }           //當前正在使用Texture Group的index

    private bool isAttacking { get; set; }
    private List<GameObject> detectedObjectList { get; set; }
    private float addValue { get; set; }

    private List<Texture[]> ChangeTextureList { get; set; }
    private List<float[]> ChangeTextureTimeList { get; set; }
    private List<int> AttackIndexList { get; set; }

    void OnTriggerStay(Collider other)
    {
        if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.AttackDistance)
        {
            if (other.gameObject.layer == GameDefinition.Enemy_Layer)           //判定敵人的Layer
            {
                if (!other.gameObject.GetComponent<EnemyLife>().isDead)
                {
                    if (!this.isAttacking)
                    {
                        this.isAttacking = true;
                        this.GetComponent<RegularChangePictures>().ChangeState(false);  //將一般移動的換圖暫停
                        this.renderer.material.mainTexture = this.ChangeTextureList[this.currentGroupIndex][this.currentTextureIndex];
                    }
                    if (!this.detectedObjectList.Contains(other.gameObject))
                        this.detectedObjectList.Add(other.gameObject);                  //抓取進入範圍內的敵人
                }
            }
            //else if (other.gameObject.layer == GameDefinition.Obstacle)      //判定障礙物的Layer
            //{
            //    if (other.tag == this.tag)  //如果障礙物Tag等於角色Tag，可對障礙物....(do something)
            //    {
            //        this.detectedObject = other.gameObject;                        //抓取進入範圍內的敵人
            //    }
            //}
        }
    }

    // Use this for initialization
    void Start()
    {
        //List放入攻擊動作的圖組
        this.ChangeTextureList = new List<Texture[]>();
        if (this.ChangeTextureGroup1.Length != 0)
            this.ChangeTextureList.Add(this.ChangeTextureGroup1);
        if (this.ChangeTextureGroup2.Length != 0)
            this.ChangeTextureList.Add(this.ChangeTextureGroup2);

        //List放入攻擊動作的時間
        this.ChangeTextureTimeList = new List<float[]>();
        this.ChangeTextureTimeList.Add(this.ChangeTextureTimeGroup1);
        this.ChangeTextureTimeList.Add(this.ChangeTextureTimeGroup2);

        //List放入判定攻擊的索引
        this.AttackIndexList = new List<int>();
        this.AttackIndexList.Add(this.AttackIndex1);
        this.AttackIndexList.Add(this.AttackIndex2);

        //初始化偵測物件清單
        this.detectedObjectList = new List<GameObject>();

        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isAttacking)
        {
            if (this.addValue >= this.ChangeTextureTimeList[this.currentGroupIndex][this.currentTextureIndex])
            {
                this.addValue = 0;

                if (this.currentTextureIndex == this.AttackIndexList[this.currentGroupIndex])   //攻擊判定
                {
                    foreach (var obj in this.detectedObjectList)
                    {
                        if (obj.layer == GameDefinition.Enemy_Layer)      //判定敵人的Layer
                        {
                            if (obj != null)
                                obj.GetComponent<EnemyLife>().DecreaseLife(1);
                        }
                        //else if (obj.layer == GameDefinition.Obstacle)      //判定障礙物的Layer
                        //{ 

                        //}
                    }
                }

                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.ChangeTextureList[this.currentGroupIndex].Length)
                {
                    this.GetComponent<RegularChangePictures>().ChangeState(true);
                    this.Reset();
                    return;
                }
                renderer.material.mainTexture = this.ChangeTextureList[this.currentGroupIndex][this.currentTextureIndex];
            }

            this.addValue += Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        //畫出偵測線
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.AttackDistance);
    }

    /// <summary>
    /// 將數值回復成預設值
    /// </summary>
    void Reset()
    {
        this.currentGroupIndex = Random.Range(0, this.ChangeTextureList.Count); //隨機選擇欲撥放的攻擊動作圖組
        this.currentTextureIndex = 0;
        this.addValue = 0;
        this.isAttacking = false;
        this.detectedObjectList.Clear();
    }
}
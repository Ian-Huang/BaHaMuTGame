using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 遠攻角色的攻擊控制 (分為近距離攻擊、遠距離攻擊) (待修正)
/// </summary>
public class FarJobAttackController : MonoBehaviour
{
    public GameDefinition.AttackMode attackMode;           //攻擊模式
    public GameObject ShootObject;

    public Texture[] ChangeTextureGroup1;   //圖組1
    public Texture[] ChangeTextureGroup2;   //圖組2

    public float[] ChangeTimeGroup1;              //交換時間間隔  (對應圖組1)
    public float[] ChangeTimeGroup2;              //交換時間間隔  (對應圖組2)

    public int AttackIndex1;                 //確認第N張圖的時間跑完後，判定攻擊的索引  (對應圖組1)
    public int AttackIndex2;                 //確認第N張圖的時間跑完後，判定攻擊的索引  (對應圖組2)

    private int currentTextureIndex { get; set; }         //當前正在使用Texture的index
    private int currentGroupIndex { get; set; }           //當前正在使用Texture Group的index

    private bool isAttacking { get; set; }
    private GameObject detectedEnemyObject { get; set; }        //目前追蹤的敵人
    private float addValue { get; set; }

    private List<Texture[]> ChangeTextureList { get; set; }
    private List<float[]> ChangeTimeList { get; set; }
    private List<int> AttackIndexList { get; set; }

    void OnTriggerStay(Collider other)
    {
        if (!this.isAttacking)
        {
            this.isAttacking = true;
            this.detectedEnemyObject = other.gameObject;                        //抓取進入範圍內的敵人
            this.GetComponent<RegularChangePictures>().ChangeState(false);      //將一般移動的換圖暫停
            this.renderer.material.mainTexture = this.ChangeTextureList[this.currentGroupIndex][this.currentTextureIndex];
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
        this.ChangeTimeList = new List<float[]>();
        this.ChangeTimeList.Add(this.ChangeTimeGroup1);
        this.ChangeTimeList.Add(this.ChangeTimeGroup2);

        //List放入判定攻擊的索引
        this.AttackIndexList = new List<int>();
        this.AttackIndexList.Add(this.AttackIndex1);
        this.AttackIndexList.Add(this.AttackIndex2);

        this.currentGroupIndex = Random.Range(0, this.ChangeTextureList.Count); //隨機選擇欲撥放的攻擊動作圖組
        this.currentTextureIndex = 0;

        this.addValue = 0;
        this.isAttacking = false;
    }

    /// <summary>
    /// 將數值回復成預設值
    /// </summary>
    void Reset()
    {
        this.currentGroupIndex = Random.Range(0, this.ChangeTextureList.Count);
        this.currentTextureIndex = 0;
        this.addValue = 0;
        this.isAttacking = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (this.isAttacking)
        {
            if (this.addValue >= this.ChangeTimeList[this.currentGroupIndex][this.currentTextureIndex])
            {
                this.addValue = 0;

                if (this.currentTextureIndex == this.AttackIndexList[this.currentGroupIndex])   //攻擊判定
                {
                    if (this.detectedEnemyObject != null)      //判定追蹤的物體是否還存在
                    {
                        //判斷是近/遠距離攻擊
                        if (this.attackMode == GameDefinition.AttackMode.Far)
                            Instantiate(this.ShootObject, this.transform.position, this.ShootObject.transform.rotation);
                        else
                            this.detectedEnemyObject.GetComponent<EnemyLife>().DecreaseLife(1);
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
}
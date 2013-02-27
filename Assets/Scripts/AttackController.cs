using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色的攻擊控制
/// </summary>
public class AttackController : MonoBehaviour
{
    public enum AttackMode
    { Near , Far }
    public AttackMode attackMode;           //攻擊模式

    public GameObject ShootObject;
    
    public Texture[] ChangeTextureGroup1;   //圖組1
    public Texture[] ChangeTextureGroup2;   //圖組2

    public float[] ChangeTimeList;              //交換時間間隔
    public int AttackIndex;                 //確認第N張圖的時間跑完後，判定攻擊的索引

    private int currentTextureIndex { get; set; }         //當前正在使用Texture的index
    private int currentGroupIndex { get; set; }           //當前正在使用Texture Group的index

    private bool isAttacking { get; set; }
    private GameObject detectedEnemyObject { get; set; }        //目前追蹤的敵人
    private float addValue { get; set; }   
    private List<Texture[]> ChangeTextureList { get; set; }   

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
        this.ChangeTextureList.Add(this.ChangeTextureGroup1);
        this.ChangeTextureList.Add(this.ChangeTextureGroup2);

        this.currentGroupIndex = Random.Range(0, this.ChangeTextureList.Count); //隨機選擇欲撥放的攻擊動作圖組
        this.currentTextureIndex = 0;

        //假如是近距離攻擊，刪除射擊物件(遠距離職業用)
        if (this.attackMode == AttackMode.Near)
            this.ShootObject = null;        
        
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

    public RaycastHit hit;
    void Update()
    {        
        if (this.isAttacking)
        {
            if (this.addValue >= this.ChangeTimeList[currentTextureIndex])
            {                
                this.addValue = 0;

                if (this.currentTextureIndex == this.AttackIndex)   //攻擊判定
                {
                    if (this.detectedEnemyObject != null)      //判定追蹤的物體是否還存在
                    {
                        //判斷是近/遠距離攻擊
                        if (this.attackMode == AttackMode.Far)
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour
{
    public GameObject ShootObject;
    
    public Texture[] ChangeTextureGroup1;   //圖組1
    public Texture[] ChangeTextureGroup2;   //圖組2

    public float[] ChangeTime;              //交換時間間隔
    public int AttackIndex;                 //確認第N張圖的時間跑完後，判定攻擊的索引

    private int currentTextureIndex { get; set; }         //當前正在使用Texture的index
    private int currentGroupIndex { get; set; }           //當前正在使用Texture Group的index

    private float addValue { get; set; }
    private bool isShoot { get; set; }
    private List<Texture[]> ChangeTextureList { get; set; }

    public GameObject obj;
    void OnTriggerStay(Collider other)
    {
        if (!this.isShoot)
        {
            this.isShoot = true;
            this.obj = other.gameObject;
            this.GetComponent<RegularChangePictures>().ChangeState(false);
            this.renderer.material.mainTexture = this.ChangeTextureList[this.currentGroupIndex][this.currentTextureIndex];
        }
    }

    // Use this for initialization
    void Start()
    {
        this.ChangeTextureList = new List<Texture[]>();
        this.ChangeTextureList.Add(this.ChangeTextureGroup1);
        this.ChangeTextureList.Add(this.ChangeTextureGroup2);

        this.currentGroupIndex = Random.Range(0, this.ChangeTextureList.Count);
        this.currentTextureIndex = 0;
        
        this.addValue = 0;  
        this.isShoot = false;
    }

    /// <summary>
    /// 數值回復成預設值
    /// </summary>
    void Reset()
    {
        this.currentGroupIndex = Random.Range(0, this.ChangeTextureList.Count);
        this.currentTextureIndex = 0;
        this.addValue = 0;
        this.isShoot = false;
    }

    // Update is called once per frame

    public RaycastHit hit;
    void Update()
    {        
        if (this.isShoot)
        {
            if (this.addValue >= this.ChangeTime[currentTextureIndex])
            {                
                this.addValue = 0;

                if (this.currentTextureIndex == this.AttackIndex)   //攻擊判定
                {
                    if (this.obj != null)      //判定追蹤的物體是否還存在
                        if (this.obj.transform.position.y == this.transform.position.y)     //判定追蹤的物體是否在同一高度(未來魔王關可能會有問題!!)
                            Instantiate(this.ShootObject, this.transform.position, this.ShootObject.transform.rotation);
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour
{
    public GameObject ShootObject;
    
    public Texture[] ChangeTextureGroup1;   //�ϲ�1
    public Texture[] ChangeTextureGroup2;   //�ϲ�2

    public float[] ChangeTime;              //�洫�ɶ����j
    public int AttackIndex;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������

    private int currentTextureIndex { get; set; }         //��e���b�ϥ�Texture��index
    private int currentGroupIndex { get; set; }           //��e���b�ϥ�Texture Group��index

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
    /// �ƭȦ^�_���w�]��
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

                if (this.currentTextureIndex == this.AttackIndex)   //�����P�w
                {
                    if (this.obj != null)      //�P�w�l�ܪ�����O�_�٦s�b
                        if (this.obj.transform.position.y == this.transform.position.y)     //�P�w�l�ܪ�����O�_�b�P�@����(�����]�����i��|�����D!!)
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

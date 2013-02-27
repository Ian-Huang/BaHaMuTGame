using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ���⪺��������
/// </summary>
public class AttackController : MonoBehaviour
{
    public enum AttackMode
    { Near , Far }
    public AttackMode attackMode;           //�����Ҧ�

    public GameObject ShootObject;
    
    public Texture[] ChangeTextureGroup1;   //�ϲ�1
    public Texture[] ChangeTextureGroup2;   //�ϲ�2

    public float[] ChangeTimeList;              //�洫�ɶ����j
    public int AttackIndex;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������

    private int currentTextureIndex { get; set; }         //��e���b�ϥ�Texture��index
    private int currentGroupIndex { get; set; }           //��e���b�ϥ�Texture Group��index

    private bool isAttacking { get; set; }
    private GameObject detectedEnemyObject { get; set; }        //�ثe�l�ܪ��ĤH
    private float addValue { get; set; }   
    private List<Texture[]> ChangeTextureList { get; set; }   

    void OnTriggerStay(Collider other)
    {
        if (!this.isAttacking)
        {
            this.isAttacking = true;
            this.detectedEnemyObject = other.gameObject;                        //����i�J�d�򤺪��ĤH
            this.GetComponent<RegularChangePictures>().ChangeState(false);      //�N�@�벾�ʪ����ϼȰ�
            this.renderer.material.mainTexture = this.ChangeTextureList[this.currentGroupIndex][this.currentTextureIndex];
        }
    }

    // Use this for initialization
    void Start()
    {
        //List��J�����ʧ@���ϲ�
        this.ChangeTextureList = new List<Texture[]>();
        this.ChangeTextureList.Add(this.ChangeTextureGroup1);
        this.ChangeTextureList.Add(this.ChangeTextureGroup2);

        this.currentGroupIndex = Random.Range(0, this.ChangeTextureList.Count); //�H����ܱ����񪺧����ʧ@�ϲ�
        this.currentTextureIndex = 0;

        //���p�O��Z�������A�R���g������(���Z��¾�~��)
        if (this.attackMode == AttackMode.Near)
            this.ShootObject = null;        
        
        this.addValue = 0;  
        this.isAttacking = false;
    }

    /// <summary>
    /// �N�ƭȦ^�_���w�]��
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

                if (this.currentTextureIndex == this.AttackIndex)   //�����P�w
                {
                    if (this.detectedEnemyObject != null)      //�P�w�l�ܪ�����O�_�٦s�b
                    {
                        //�P�_�O��/���Z������
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ���𨤦⪺�������� (������Z�������B���Z������) (�ݭץ�)
/// </summary>
public class FarJobAttackController : MonoBehaviour
{
    public GameDefinition.AttackMode attackMode;           //�����Ҧ�
    public GameObject ShootObject;

    public Texture[] ChangeTextureGroup1;   //�ϲ�1
    public Texture[] ChangeTextureGroup2;   //�ϲ�2

    public float[] ChangeTimeGroup1;              //�洫�ɶ����j  (�����ϲ�1)
    public float[] ChangeTimeGroup2;              //�洫�ɶ����j  (�����ϲ�2)

    public int AttackIndex1;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������  (�����ϲ�1)
    public int AttackIndex2;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������  (�����ϲ�2)

    private int currentTextureIndex { get; set; }         //��e���b�ϥ�Texture��index
    private int currentGroupIndex { get; set; }           //��e���b�ϥ�Texture Group��index

    private bool isAttacking { get; set; }
    private GameObject detectedEnemyObject { get; set; }        //�ثe�l�ܪ��ĤH
    private float addValue { get; set; }

    private List<Texture[]> ChangeTextureList { get; set; }
    private List<float[]> ChangeTimeList { get; set; }
    private List<int> AttackIndexList { get; set; }

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
        if (this.ChangeTextureGroup1.Length != 0)
            this.ChangeTextureList.Add(this.ChangeTextureGroup1);
        if (this.ChangeTextureGroup2.Length != 0)
            this.ChangeTextureList.Add(this.ChangeTextureGroup2);

        //List��J�����ʧ@���ɶ�
        this.ChangeTimeList = new List<float[]>();
        this.ChangeTimeList.Add(this.ChangeTimeGroup1);
        this.ChangeTimeList.Add(this.ChangeTimeGroup2);

        //List��J�P�w����������
        this.AttackIndexList = new List<int>();
        this.AttackIndexList.Add(this.AttackIndex1);
        this.AttackIndexList.Add(this.AttackIndex2);

        this.currentGroupIndex = Random.Range(0, this.ChangeTextureList.Count); //�H����ܱ����񪺧����ʧ@�ϲ�
        this.currentTextureIndex = 0;

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
    void Update()
    {
        if (this.isAttacking)
        {
            if (this.addValue >= this.ChangeTimeList[this.currentGroupIndex][this.currentTextureIndex])
            {
                this.addValue = 0;

                if (this.currentTextureIndex == this.AttackIndexList[this.currentGroupIndex])   //�����P�w
                {
                    if (this.detectedEnemyObject != null)      //�P�w�l�ܪ�����O�_�٦s�b
                    {
                        //�P�_�O��/���Z������
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
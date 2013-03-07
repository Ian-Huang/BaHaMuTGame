using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ��Ԩ��⪺��������
/// </summary>
public class NearJobAttackController : MonoBehaviour
{
    public float AttackDistance = 2;

    public Texture[] ChangeTextureGroup1;   //�ϲ�1
    public Texture[] ChangeTextureGroup2;   //�ϲ�2

    public float[] ChangeTextureTimeGroup1;              //�洫�ɶ����j  (�����ϲ�1)
    public float[] ChangeTextureTimeGroup2;              //�洫�ɶ����j  (�����ϲ�2)

    public int AttackIndex1;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������  (�����ϲ�1)
    public int AttackIndex2;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������  (�����ϲ�2)

    public LayerMask AttackLayer;

    private int currentTextureIndex { get; set; }         //��e���b�ϥ�Texture��index
    private int currentGroupIndex { get; set; }           //��e���b�ϥ�Texture Group��index

    private bool isAttacking { get; set; }
    private GameObject detectedEnemyObject { get; set; }        //�ثe�l�ܪ��ĤH
    private float addValue { get; set; }

    private List<Texture[]> ChangeTextureList { get; set; }
    private List<float[]> ChangeTextureTimeList { get; set; }
    private List<int> AttackIndexList { get; set; }

    void OnTriggerStay(Collider other)
    {if ((this.AttackLayer.value & (int)Mathf.Pow(2, other.gameObject.layer)) != 0)      //�P�w������Layer
        {
            if (!this.isAttacking)
            {
                if (!other.gameObject.GetComponent<EnemyLife>().isDead)
                {
                    if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.AttackDistance)
                    {
                        this.isAttacking = true;
                        this.detectedEnemyObject = other.gameObject;                        //����i�J�d�򤺪��ĤH
                        this.GetComponent<RegularChangePictures>().ChangeState(false);      //�N�@�벾�ʪ����ϼȰ�
                        this.renderer.material.mainTexture = this.ChangeTextureList[this.currentGroupIndex][this.currentTextureIndex];
                    }
                }
            }
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
        this.ChangeTextureTimeList = new List<float[]>();
        this.ChangeTextureTimeList.Add(this.ChangeTextureTimeGroup1);
        this.ChangeTextureTimeList.Add(this.ChangeTextureTimeGroup2);

        //List��J�P�w����������
        this.AttackIndexList = new List<int>();
        this.AttackIndexList.Add(this.AttackIndex1);
        this.AttackIndexList.Add(this.AttackIndex2);

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

                if (this.currentTextureIndex == this.AttackIndexList[this.currentGroupIndex])   //�����P�w
                {
                    if (this.detectedEnemyObject != null)      //�P�w�l�ܪ�����O�_�٦s�b
                    {
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

    void OnDrawGizmos()
    {
        //�e�X�����u
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.AttackDistance);
    }

    /// <summary>
    /// �N�ƭȦ^�_���w�]��
    /// </summary>
    void Reset()
    {
        this.currentGroupIndex = Random.Range(0, this.ChangeTextureList.Count); //�H����ܱ����񪺧����ʧ@�ϲ�
        this.currentTextureIndex = 0;
        this.addValue = 0;
        this.isAttacking = false;
    }
}
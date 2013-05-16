using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ��Ԩ��⪺��������
/// </summary>
public class KnightJobAttackController : MonoBehaviour
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
    private List<GameObject> detectedObjectList { get; set; }
    private float addValue { get; set; }

    private List<Texture[]> ChangeTextureList { get; set; }
    private List<float[]> ChangeTextureTimeList { get; set; }
    private List<int> AttackIndexList { get; set; }

    void OnTriggerStay(Collider other)
    {
        if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.AttackDistance)
        {
            if (other.gameObject.layer == GameDefinition.Enemy_Layer)           //�P�w�ĤH��Layer
            {
                if (!other.gameObject.GetComponent<EnemyLife>().isDead)
                {
                    if (!this.isAttacking)
                    {
                        this.isAttacking = true;
                        this.GetComponent<RegularChangePictures>().ChangeState(false);  //�N�@�벾�ʪ����ϼȰ�
                        this.renderer.material.mainTexture = this.ChangeTextureList[this.currentGroupIndex][this.currentTextureIndex];
                    }
                    if (!this.detectedObjectList.Contains(other.gameObject))
                        this.detectedObjectList.Add(other.gameObject);                  //����i�J�d�򤺪��ĤH
                }
            }
            //else if (other.gameObject.layer == GameDefinition.Obstacle)      //�P�w��ê����Layer
            //{
            //    if (other.tag == this.tag)  //�p�G��ê��Tag���󨤦�Tag�A�i���ê��....(do something)
            //    {
            //        this.detectedObject = other.gameObject;                        //����i�J�d�򤺪��ĤH
            //    }
            //}
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

        //��l�ư�������M��
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

                if (this.currentTextureIndex == this.AttackIndexList[this.currentGroupIndex])   //�����P�w
                {
                    foreach (var obj in this.detectedObjectList)
                    {
                        if (obj.layer == GameDefinition.Enemy_Layer)      //�P�w�ĤH��Layer
                        {
                            if (obj != null)
                                obj.GetComponent<EnemyLife>().DecreaseLife(1);
                        }
                        //else if (obj.layer == GameDefinition.Obstacle)      //�P�w��ê����Layer
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
        this.detectedObjectList.Clear();
    }
}
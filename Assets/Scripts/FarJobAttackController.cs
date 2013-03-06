using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ���𨤦⪺�������� (������Z�������B���Z������) (�ݭץ�)
/// </summary>
public class FarJobAttackController : MonoBehaviour
{
    public float FarAttackDistance = 13;
    public float NearAttackDistance = 3;

    public GameDefinition.AttackMode attackMode;           //�����Ҧ�
    public GameObject ShootObject;

    public Texture[] FarAttackChangeTextures;   //���Z�������Ҧ��ϲ�
    public Texture[] NearAttackChangeTextures;   //��Z�������Ҧ��ϲ�

    public float[] FarAttackChangeTime;              //�洫�ɶ����j  (�������Z�������Ҧ��ϲ�)
    public float[] NearAttackChangeTime;              //�洫�ɶ����j  (������Z�������Ҧ��ϲ�)

    public int FarAttackIndex;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������  (�������Z�������Ҧ��ϲ�)
    public int NearAttackIndex;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������  (������Z�������Ҧ��ϲ�)

    private int currentTextureIndex { get; set; }         //��e���b�ϥ�Texture��index
    //private int currentGroupIndex { get; set; }           //��e���b�ϥ�Texture Group��index

    private bool isAttacking { get; set; }
    private GameObject detectedEnemyObject { get; set; }        //�ثe�l�ܪ��ĤH
    private float addValue { get; set; }

    //private List<Texture[]> ChangeTextureList { get; set; }
    //private List<float[]> ChangeTimeList { get; set; }
    //private List<int> AttackIndexList { get; set; }

    void OnTriggerStay(Collider other)
    {
        if (!this.isAttacking)
        {
            if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.FarAttackDistance)
            {
                if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.NearAttackDistance)
                {
                    this.attackMode = GameDefinition.AttackMode.Near;
                    this.renderer.material.mainTexture = this.NearAttackChangeTextures[this.currentTextureIndex];
                }
                else
                {
                    this.attackMode = GameDefinition.AttackMode.Far;
                    this.renderer.material.mainTexture = this.FarAttackChangeTextures[this.currentTextureIndex];
                }
                this.isAttacking = true;
                this.detectedEnemyObject = other.gameObject;                        //����i�J�d�򤺪��ĤH
                this.GetComponent<RegularChangePictures>().ChangeState(false);      //�N�@�벾�ʪ����ϼȰ�
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        ////List��J�����ʧ@���ϲ�
        //this.ChangeTextureList = new List<Texture[]>();
        //if (this.FarAttackChangeTextures.Length != 0)
        //    this.ChangeTextureList.Add(this.FarAttackChangeTextures);
        //if (this.NearAttackChangeTextures.Length != 0)
        //    this.ChangeTextureList.Add(this.NearAttackChangeTextures);

        ////List��J�����ʧ@���ɶ�
        //this.ChangeTimeList = new List<float[]>();
        //this.ChangeTimeList.Add(this.FarAttackChangeTime);
        //this.ChangeTimeList.Add(this.NearAttackChangeTime);

        ////List��J�P�w����������
        //this.AttackIndexList = new List<int>();
        //this.AttackIndexList.Add(this.FarAttackIndex);
        //this.AttackIndexList.Add(this.NearAttackIndex);

        this.Reset();
    }

    /// <summary>
    /// �N�ƭȦ^�_���w�]��
    /// </summary>
    void Reset()
    {
        this.currentTextureIndex = 0;
        this.addValue = 0;
        this.isAttacking = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (this.isAttacking)
        {
            //���Z�������Ҧ��]�w
            if (this.attackMode == GameDefinition.AttackMode.Far)
            {
                if (this.addValue >= this.FarAttackChangeTime[this.currentTextureIndex])
                {
                    this.addValue = 0;

                    if (this.currentTextureIndex == this.FarAttackIndex)   //�����P�w
                    {
                        if (this.detectedEnemyObject != null)      //�P�w�l�ܪ�����O�_�٦s�b
                        {
                            Instantiate(this.ShootObject, this.transform.position, this.ShootObject.transform.rotation);
                        }
                    }

                    this.currentTextureIndex++;
                    if (this.currentTextureIndex >= this.FarAttackChangeTextures.Length)
                    {
                        this.GetComponent<RegularChangePictures>().ChangeState(true);
                        this.Reset();
                        return;
                    }
                    renderer.material.mainTexture = this.FarAttackChangeTextures[this.currentTextureIndex];
                }
            }
            //��Z�������Ҧ��]�w
            else
            {
                if (this.addValue >= this.NearAttackChangeTime[this.currentTextureIndex])
                {
                    this.addValue = 0;

                    if (this.currentTextureIndex == this.NearAttackIndex)   //�����P�w
                    {
                        if (this.detectedEnemyObject != null)      //�P�w�l�ܪ�����O�_�٦s�b
                        {
                            this.detectedEnemyObject.GetComponent<EnemyLife>().DecreaseLife(1);
                        }
                    }

                    this.currentTextureIndex++;
                    if (this.currentTextureIndex >= this.NearAttackChangeTextures.Length)
                    {
                        this.GetComponent<RegularChangePictures>().ChangeState(true);
                        this.Reset();
                        return;
                    }
                    renderer.material.mainTexture = this.NearAttackChangeTextures[this.currentTextureIndex];
                }
            }

            this.addValue += Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        //�e�X�����u
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.FarAttackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, Vector3.right * this.NearAttackDistance);
    }
}
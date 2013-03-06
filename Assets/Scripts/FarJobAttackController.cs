using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ���𨤦⪺�������� (������Z�������B���Z������) 
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
    
    private bool isAttacking { get; set; }
    private GameObject detectedEnemyObject { get; set; }        //�ثe�l�ܪ��ĤH
    private float addValue { get; set; }

    void OnTriggerStay(Collider other)
    {
        if (!this.isAttacking)
        {
            if (!other.gameObject.GetComponent<EnemyLife>().isDead)
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
    }

    // Use this for initialization
    void Start()
    {        
        this.Reset();
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

    /// <summary>
    /// �N�ƭȦ^�_���w�]��
    /// </summary>
    void Reset()
    {
        this.currentTextureIndex = 0;
        this.addValue = 0;
        this.isAttacking = false;
    }
}
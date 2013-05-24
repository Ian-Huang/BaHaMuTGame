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

    private GameDefinition.AttackMode attackMode;        //�����Ҧ�
    public GameDefinition.AttackType AttackType;        //��������

    public GameObject ShootObject;

    public Texture[] FarAttackChangeTextures;   //���Z�������Ҧ��ϲ�
    public Texture[] NearAttackChangeTextures;   //��Z�������Ҧ��ϲ�

    public float[] FarAttackChangeTextureTime;              //�洫�ɶ����j  (�������Z�������Ҧ��ϲ�)
    public float[] NearAttackChangeTextureTime;              //�洫�ɶ����j  (������Z�������Ҧ��ϲ�)

    public int FarAttackIndex;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������  (�������Z�������Ҧ��ϲ�)
    public int NearAttackIndex;                 //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������  (������Z�������Ҧ��ϲ�)

    public LayerMask AttackLayer;

    private int currentTextureIndex { get; set; }         //��e���b�ϥ�Texture��index

    private bool isAttacking { get; set; }
    private GameObject detectedEnemyObject { get; set; }        //�ثe�l�ܪ��ĤH
    private float addValue { get; set; }

    private RolePropertyInfo roleInfo { get; set; }

    void OnTriggerStay(Collider other)
    {
        if ((this.AttackLayer.value & (int)Mathf.Pow(2, other.gameObject.layer)) != 0)      //�P�w������Layer
        {
            if (!this.isAttacking)
            {
                if (!other.gameObject.GetComponent<EnemyPropertyInfo>().isDead)
                {
                    if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.FarAttackDistance)
                    {
                        if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.NearAttackDistance)
                        {
                            this.attackMode = GameDefinition.AttackMode.NearAttack;
                            this.renderer.material.mainTexture = this.NearAttackChangeTextures[this.currentTextureIndex];
                        }
                        else
                        {
                            this.attackMode = GameDefinition.AttackMode.FarAttck;
                            this.renderer.material.mainTexture = this.FarAttackChangeTextures[this.currentTextureIndex];
                        }
                        this.isAttacking = true;
                        this.detectedEnemyObject = other.gameObject;                        //����i�J�d�򤺪��ĤH
                        this.GetComponent<RegularChangePictures>().ChangeState(false);      //�N�@�벾�ʪ����ϼȰ�
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //���J�����T
        this.roleInfo = this.GetComponent<RolePropertyInfo>();

        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isAttacking)
        {
            //���Z�������Ҧ��]�w
            if (this.attackMode == GameDefinition.AttackMode.FarAttck)
            {
                if (this.addValue >= this.FarAttackChangeTextureTime[this.currentTextureIndex])
                {
                    this.addValue = 0;

                    if (this.currentTextureIndex == this.FarAttackIndex)   //�����P�w
                    {
                        if (this.detectedEnemyObject != null)      //�P�w�l�ܪ�����O�_�٦s�b
                        {
                            GameObject obj = (GameObject)Instantiate(this.ShootObject,
                                new Vector3(this.transform.position.x, this.transform.position.y, GameDefinition.ShootObject_ZIndex),
                                this.ShootObject.transform.rotation
                            );
                            ShootObjectInfo info = obj.GetComponent<ShootObjectInfo>();
                            info.Damage = this.roleInfo.farDamage;
                            info.AttackType = this.AttackType;
                        }
                    }

                    this.currentTextureIndex++;
                    if (this.currentTextureIndex >= this.FarAttackChangeTextures.Length)
                    {
                        this.GetComponent<RegularChangePictures>().ChangeState(true);
                        this.Reset();
                        return;
                    }
                    this.renderer.material.mainTexture = this.FarAttackChangeTextures[this.currentTextureIndex];
                }
            }
            //��Z�������Ҧ��]�w
            else
            {
                if (this.addValue >= this.NearAttackChangeTextureTime[this.currentTextureIndex])
                {
                    this.addValue = 0;

                    if (this.currentTextureIndex == this.NearAttackIndex)   //�����P�w
                    {
                        if (this.detectedEnemyObject != null)      //�P�w�l�ܪ�����O�_�٦s�b
                        {
                            this.detectedEnemyObject.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.roleInfo.nearDamage);
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
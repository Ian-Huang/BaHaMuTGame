using UnityEngine;
using System.Collections;

/// <summary>
/// �B�z�ĤH���������� (������)
/// </summary>
public class EnemyAttackController : MonoBehaviour
{
    public float AttackDistance = 2;            //�����Z��
    public Texture[] AttackChangeTextures;      //�������洫�ϲ�
    public float ChangeTextureTime = 0.1f;      //�洫�ɶ����j
    public int AttackIndex;                     //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������
    public float AttackMoveSpeed = 0;           //�����ɪ����ʳt��
    public GameDefinition.AttackMode attackMode;
    public GameObject ShootObject;
    public LayerMask AttackLayer;

    private int currentTextureIndex { get; set; }
    private float addValue { get; set; }
    private bool isAttacking { get; set; }

    private GameObject detectedRoleObject { get; set; }        //�ثe�l�ܪ����a

    private EnemyPropertyInfo enemyInfo { get; set; }

    void OnTriggerStay(Collider other)
    {
        if ((this.AttackLayer.value & (int)Mathf.Pow(2, other.gameObject.layer)) != 0)      //�P�w������Layer
        {
            if (!this.isAttacking)
            {
                if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.AttackDistance)
                {
                    this.isAttacking = true;
                    this.renderer.material.mainTexture = this.AttackChangeTextures[this.currentTextureIndex];
                    if (!this.GetComponent<EnemyPropertyInfo>().isDead)      //�P�w�l�ܪ�����O�_�٦s�b
                    {
                        this.GetComponent<RegularChangePictures>().ChangeState(false);          //�N�@�벾�ʪ����ϼȰ�
                        this.GetComponent<MoveController>().ChangeSpeed(this.AttackMoveSpeed);  //���ܧ����ɲ��ʪ��t��
                        this.detectedRoleObject = other.gameObject;                        //����i�J�d�򤺪����a
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //���J�ĤH��T
        this.enemyInfo = this.GetComponent<EnemyPropertyInfo>();

        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isAttacking)
        {
            if (this.addValue >= this.ChangeTextureTime)
            {
                this.addValue = 0;

                if (this.currentTextureIndex == this.AttackIndex)   //�����P�w
                {
                    //�ݸ�(�ĤH�����ᵹ�󨤦⪺�^�X)
                    if (this.attackMode == GameDefinition.AttackMode.FarAttck)
                    {
                        GameObject obj = (GameObject)Instantiate(this.ShootObject,
                            new Vector3(this.transform.position.x, this.transform.position.y, GameDefinition.ShootObject_ZIndex),
                            this.ShootObject.transform.rotation);

                        ShootObjectInfo info = obj.GetComponent<ShootObjectInfo>();
                        info.Damage = this.enemyInfo.farDamage;
                    }
                    else
                    {
                        if (this.detectedRoleObject != null)      //�P�w�l�ܪ�����O�_�٦s�b
                        {
                            this.detectedRoleObject.GetComponent<RolePropertyInfo>().DecreaseLife(this.enemyInfo.nearDamage);
                        }
                    }
                }

                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.AttackChangeTextures.Length)
                {
                    this.GetComponent<RegularChangePictures>().ChangeState(true);
                    this.Reset();
                    return;
                }
                renderer.material.mainTexture = this.AttackChangeTextures[this.currentTextureIndex];
            }

            this.addValue += Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        //�e�X�����u
        Gizmos.color = Color.black;
        Gizmos.DrawRay(this.transform.position, Vector3.left * this.AttackDistance);
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
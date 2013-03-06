using UnityEngine;
using System.Collections;

/// <summary>
/// �B�z�ĤH���������� (������)
/// </summary>
public class EnemyAttackController : MonoBehaviour
{
    public float AttackDistance = 2;
    public Texture[] AttackChangeTextures;      //�������洫�ϲ�
    public float ChangeTime = 0.1f;             //�洫�ɶ����j
    public int AttackIndex;                    //�T�{��N�i�Ϫ��ɶ��]����A�P�w����������
   
    private int currentTextureIndex { get; set; }
    private float addValue { get; set; }
    private bool isAttacking { get; set; }
    
    void OnTriggerStay(Collider other)
    {

        if (!this.isAttacking)
        {
            if (Mathf.Abs(this.transform.position.x - other.transform.position.x) < this.AttackDistance)
            {
                this.isAttacking = true;
                this.renderer.material.mainTexture = this.AttackChangeTextures[this.currentTextureIndex];
                if (!this.GetComponent<EnemyLife>().isDead)      //�P�w�l�ܪ�����O�_�٦s�b
                {
                    this.GetComponent<RegularChangePictures>().ChangeState(false);  //�N�@�벾�ʪ����ϼȰ�
                    this.GetComponent<MoveController>().MovingState(false);         //�N�@�벾�ʪ�����Ȱ�
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
            if (this.addValue >= this.ChangeTime)
            {
                this.addValue = 0;

                if (this.currentTextureIndex == this.AttackIndex)   //�����P�w
                {
                    //�ݸ�(�ĤH�����ᵹ�󨤦⪺�^�X)
                }

                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.AttackChangeTextures.Length)
                {

                    this.GetComponent<RegularChangePictures>().ChangeState(true);
                    this.GetComponent<MoveController>().MovingState(true);
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
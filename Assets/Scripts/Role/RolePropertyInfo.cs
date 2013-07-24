using UnityEngine;
using System.Collections;
/// <summary>
/// ���⪺�ݩʸ�T
/// </summary>
public class RolePropertyInfo : MonoBehaviour
{
    public GameDefinition.Role Role;

    public int currentLife; //��e�ͩR��
    public int maxLife;     //�̤j�ͩR��
    public int cureRate;    //�C��^�_�ͩR�t�v
    public int defence;     //���m�O
    public int nearDamage;  //��Z�������ˮ`��
    public int farDamage;   //���Z�������ˮ`��

    public bool isWeak { get; private set; }
    public int WeakCureScale = 10;              //Weak���A �^�_�t�v

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isWeak = false;

        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();

        GameDefinition.RoleData getData = GameDefinition.RoleList.Find((GameDefinition.RoleData data) => { return data.RoleName == Role; });
        this.maxLife = getData.Life;
        this.currentLife = getData.Life;
        this.cureRate = getData.CureRate;
        this.defence = getData.Defence;
        this.nearDamage = getData.NearDamage;
        this.farDamage = getData.FarDamage;

        InvokeRepeating("RestoreLifePersecond", 0.1f, 1);
    }

    /// <summary>
    /// ��֨����q�禡
    /// </summary>
    /// <param name="deLife">��֪��ƭ�</param>
    public void DecreaseLife(int deLife)
    {
        //�������m�O
        deLife -= this.defence;
        if (deLife <= 0)
            deLife = 1;

        if (!this.isWeak)
        {
            //�����e����z�A�����⪺�ͩR��
            this.currentLife -= deLife;
        }
        else
        {
            //�����e��z�A���`�h��(������)
            GameManager.script.CurrentMorale -= deLife;
            if (GameManager.script.CurrentMorale <= 0)
                GameManager.script.CurrentMorale = 0;
        }

        if (!this.isWeak)
        {
            //��ͩR�p��0
            if (this.currentLife <= 0)
            {
                this.currentLife = 0;
                this.isWeak = true;
                this.boneAnimation.Play("weak");
            }
        }
    }

    /// <summary>
    /// �C��T�w�^�_�ͩR
    /// </summary>
    void RestoreLifePersecond()
    {
        //����z�A�^�_�t�v���`
        if (!this.isWeak)
        {
            this.currentLife += this.cureRate;
            if (this.currentLife >= this.maxLife)
                this.currentLife = this.maxLife;
        }
        else
        {
            //��z���A�A�^�_�t�v * WeakCureScale
            this.currentLife += (this.cureRate * this.WeakCureScale);
            if (this.currentLife >= this.maxLife)
            {
                this.currentLife = this.maxLife;

                this.isWeak = false;
            }
        }
    }
}

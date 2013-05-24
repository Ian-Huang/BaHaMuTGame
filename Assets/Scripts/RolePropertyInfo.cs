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

    // Use this for initialization
    void Start()
    {
        GameDefinition.RoleData getData = GameDefinition.RoleList.Find((GameDefinition.RoleData data) => { return data.RoleName == Role; });
        this.maxLife = getData.Life;
        this.currentLife = getData.Life;
        this.cureRate = getData.CureRate;
        this.defence = getData.Defence;
        this.nearDamage = getData.NearDamage;
        this.farDamage = getData.FarDamage;

        InvokeRepeating("RestoreLifePersecond", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ��֨����q�禡
    /// </summary>
    /// <param name="deLife">��֪��ƭ�</param>
    public void DecreaseLife(int deLife)
    {
        deLife -= this.defence; //�������m�O
        if (deLife <= 0)
            deLife = 1;

        this.currentLife -= deLife;

        //��ͩR�p��0
        if (this.currentLife <= 0)
        {
            print(this.name + " ���`�F�I");
        }
    }

    /// <summary>
    /// �C��T�w�^�_�ͩR
    /// </summary>
    void RestoreLifePersecond()
    {
        this.currentLife += this.cureRate;
        if (this.currentLife >= this.maxLife)
            this.currentLife = this.maxLife;
    }
}

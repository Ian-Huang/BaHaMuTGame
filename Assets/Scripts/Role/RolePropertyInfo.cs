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

    public int WeakCureScale = 10;              //Weak���A �^�_�t�v
    public Texture[] WeakChangeTextures;        //�������洫�ϲ�
    public float ChangeTextureTime = 0.1f;      //�洫�ɶ����j    

    public bool isWeak { get; private set; }
    private int currentTextureIndex { get; set; }

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

        this.isWeak = false;
        InvokeRepeating("RestoreLifePersecond", 0.1f, 1);
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

        //��ͩR�p��0
        if (!this.isWeak)
        {
            if (this.currentLife <= 0)
            {
                this.currentLife = 0;
                switch (this.Role)
                {
                    case GameDefinition.Role.�k�v:
                    case GameDefinition.Role.�y�H:
                        this.GetComponent<FarJobAttackController>().ChangeState(false); //�N�������ʧ@�Ȱ�                        
                        break;

                    case GameDefinition.Role.�g�Ԥh:
                    case GameDefinition.Role.�M�h:
                        this.GetComponent<NearJobAttackController>().ChangeState(false);   //�N�������ʧ@�Ȱ�                        
                        break;
                }
                this.GetComponent<RegularChangePictures>().ChangeState(false);  //�N�@�벾�ʪ����ϼȰ�
                this.isWeak = true;
                InvokeRepeating("ChangeWeakTexture", 0.1f, this.ChangeTextureTime);
            }
        }
    }

    void ChangeWeakTexture()
    {
        if ((this.currentTextureIndex + 1) >= this.WeakChangeTextures.Length)       //�k0�A�`��
            this.currentTextureIndex = 0;
        else
            this.currentTextureIndex++;

        this.renderer.material.mainTexture = this.WeakChangeTextures[this.currentTextureIndex];
    }

    /// <summary>
    /// �C��T�w�^�_�ͩR
    /// </summary>
    void RestoreLifePersecond()
    {
        if (!this.isWeak)
        {
            this.currentLife += this.cureRate;
            if (this.currentLife >= this.maxLife)
                this.currentLife = this.maxLife;
        }
        else
        {
            this.currentLife += (this.cureRate * this.WeakCureScale);
            if (this.currentLife >= this.maxLife)
            {
                this.currentLife = this.maxLife;

                this.isWeak = false;
                CancelInvoke("ChangeWeakTexture");
                this.currentTextureIndex = 0;
                this.GetComponent<RegularChangePictures>().ChangeState(true);  //�N�@�벾�ʪ����ϫ�_�B�@
                switch (this.Role)
                {
                    case GameDefinition.Role.�k�v:
                    case GameDefinition.Role.�y�H:
                        this.GetComponent<FarJobAttackController>().ChangeState(true); //�N�������ʧ@�Ȱ�                        
                        break;

                    case GameDefinition.Role.�g�Ԥh:
                    case GameDefinition.Role.�M�h:
                        this.GetComponent<NearJobAttackController>().ChangeState(true);   //�N�������ʧ@�Ȱ�                        
                        break;
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;

/// <summary>
/// �ĤH���ݩʸ�T
/// </summary>
public class EnemyPropertyInfo : MonoBehaviour
{
    public GameDefinition.Enemy Enemy;
    public int currentLife; //��e�ͩR��
    public int maxLife;     //�̤j�ͩR��
    public int cureRate;    //�C��^�_�ͩR�t�v
    public int defence;     //���m�O
    public int nearDamage;  //��Z�������ˮ`��
    public int farDamage;   //���Z�������ˮ`��

    public Texture[] DeadChangeTextures;        //�������洫�ϲ�
    public float ChangeTextureTime = 0.1f;             //�洫�ɶ����j

    public bool isDead { get; private set; }
    private int currentTextureIndex { get; set; }
    private float addValue { get; set; }


    // Use this for initialization
    void Start()
    {
        this.isDead = false;
        GameDefinition.EnemyData getData = GameDefinition.EnemyList.Find((GameDefinition.EnemyData data) => { return data.EnemyName == Enemy; });
        this.maxLife = getData.Life;
        this.currentLife = getData.Life;
        this.cureRate = getData.CureRate;
        this.defence = getData.Defence;
        this.nearDamage = getData.NearDamage;
        this.farDamage = getData.FarDamage;

        InvokeRepeating("RestoreLifePersecond", 0.1f, 1);
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

    /// <summary>
    /// ��ּĤH��q�禡
    /// </summary>
    /// <param name="deLife">��֪��ƭ�</param>
    public void DecreaseLife(int deLife)
    {
        deLife -= this.defence; //�������m�O
        if (deLife <= 0)
            deLife = 0;

        this.currentLife -= deLife;

        //��ͩR�p��0�A�R������
        if (!this.isDead && this.currentLife <= 0)
        {
            this.isDead = true;
            Destroy(this.GetComponent<MoveController>());
            Destroy(this.GetComponent<RegularChangePictures>());
            Destroy(this.GetComponent<EnemyAttackController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isDead)
        {
            if (this.addValue >= this.ChangeTextureTime)
            {
                this.addValue = 0;
                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.DeadChangeTextures.Length)
                {
                    //�����z���Ϥ���A�R������
                    if (this.transform.parent.name.Contains("Clone"))
                        Destroy(this.transform.parent.gameObject);
                    else
                        Destroy(this.gameObject);
                    return;
                }
                this.renderer.material.mainTexture = this.DeadChangeTextures[this.currentTextureIndex];
            }
            this.addValue += Time.deltaTime;
        }
    }
}
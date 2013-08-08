using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-24
/// Modify Date�G2013-08-08
/// Author�GIan
/// Description�G
///     �ĤH���ݩʸ�T
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

    public Material CurrentMaterial;    //�]�w����Material (null => �ϥιw�]Material)

    public bool isDead { get; private set; }

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isDead = false;

        //�]�wBoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(DeadDestroy);
        //�pCurrentMaterial���ȡA�h�]�w��e����Material
        if (this.CurrentMaterial != null)
        {
            this.boneAnimation.RestoreOriginalMaterials();
            this.boneAnimation.SwapMaterial(this.boneAnimation.mMaterialSource[0], this.CurrentMaterial);
        }

        //Ū���t���x�s���Ǫ��ݩʸ��
        if (this.Enemy != GameDefinition.Enemy.�ۭq)  //�p�O"�ۭq"�Ǫ��A�h��Ū���t�θ��
        {
            GameDefinition.EnemyData getData = GameDefinition.EnemyList.Find((GameDefinition.EnemyData data) => { return data.EnemyName == Enemy; });
            this.maxLife = getData.Life;
            this.currentLife = getData.Life;
            this.cureRate = getData.CureRate;
            this.defence = getData.Defence;
            this.nearDamage = getData.NearDamage;
            this.farDamage = getData.FarDamage;
        }

        InvokeRepeating("RestoreLifePersecond", 0.1f, 1);
    }

    /// <summary>
    /// SmoothMove UserTrigger(�������`�ʵe��R���ۤv)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void DeadDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //�T�{�w�i�J���`���A�B���񦺤`�ʵe��A�~�i�R��
        if (this.isDead && triggerEvent.animationName.CompareTo("defeat") == 0)
            Destroy(this.gameObject);
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
            //�S�wenemy���`(���Ǧ��`�A�s�P�p�L�@�_���`)
            if (this.Enemy == GameDefinition.Enemy.�ۭq)
                foreach (var script in this.transform.parent.gameObject.GetComponentsInChildren<EnemyPropertyInfo>())
                    script.EnemyDead();
            //�@��enemy���`
            else
                this.EnemyDead();
        }
    }

    public void EnemyDead()
    {
        this.currentLife = 0;
        this.isDead = true;
        this.boneAnimation.Play("defeat");
        Destroy(this.GetComponent<MoveController>());   //���`:����ĤH����
        CancelInvoke("RestoreLifePersecond");           //���`:����ĤH�^�_�ͩR
    }
}
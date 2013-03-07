using UnityEngine;
using System.Collections;

/// <summary>
/// ����ĤH����q
/// </summary>
public class EnemyLife : MonoBehaviour
{
    public int TotalLife = 1;                   //����ͩR�`��

    public Texture[] DeadChangeTextures;        //�������洫�ϲ�
    public float ChangeTextureTime = 0.1f;             //�洫�ɶ����j

    public bool isDead { get; private set; }
    private int currentTextureIndex { get; set; }    
    private float addValue { get; set; }


    // Use this for initialization
    void Start()
    {
        this.isDead = false;
    }

    /// <summary>
    /// ��֪����q�禡
    /// </summary>
    /// <param name="deLife">��֪��ƭ�</param>
    public void DecreaseLife(int deLife)
    {
        this.TotalLife -= deLife;
        
        //��ͩR�p��0�A�R������
        if (!this.isDead && this.TotalLife <= 0)
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
                    Destroy(this.gameObject);
                    return;
                }
                this.renderer.material.mainTexture = this.DeadChangeTextures[this.currentTextureIndex];
            }
            this.addValue += Time.deltaTime;
        }
    }
}
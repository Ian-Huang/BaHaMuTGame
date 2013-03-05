using UnityEngine;
using System.Collections;

/// <summary>
/// ����ĤH����q
/// </summary>
public class EnemyLife : MonoBehaviour
{
    public int TotalLife = 1;           //����ͩR�`��

    public Texture[] ChangeTextures;
    public float ChangeTime = 0.1f;             //�洫�ɶ����j

    private int currentTextureIndex { get; set; }
    private bool isDestroy { get; set; }
    private float addValue { get; set; }


    // Use this for initialization
    void Start()
    {
        this.isDestroy = false;
    }

    /// <summary>
    /// ��֪����q�禡
    /// </summary>
    /// <param name="deLife">��֪��ƭ�</param>
    public void DecreaseLife(int deLife)
    {
        this.TotalLife -= deLife;
        
        //��ͩR�p��0�A�R������
        if (!this.isDestroy && this.TotalLife <= 0)
        {
            this.isDestroy = true;
            Destroy(this.GetComponent<MoveController>());
            Destroy(this.GetComponent<RegularChangePictures>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isDestroy)
        {
            if (this.addValue >= this.ChangeTime)
            {
                this.addValue = 0;
                this.currentTextureIndex++;
                if (this.currentTextureIndex >= this.ChangeTextures.Length)
                {
                    //�����z���Ϥ���A�R������
                    Destroy(this.gameObject);
                    return;
                }
                this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
            }
            this.addValue += Time.deltaTime;
        }
    }
}
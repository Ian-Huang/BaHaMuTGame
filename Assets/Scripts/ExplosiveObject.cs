using UnityEngine;
using System.Collections;

/// <summary>
/// �z������(�k�v���y�����ĤH�Უ��)
/// </summary>
public class ExplosiveObject : MonoBehaviour
{
    public Texture[] ChangeTextures;
    public float ChangeTime = 0.1f;             //�洫�ɶ����j

    private int currentTextureIndex { get; set; }
    private bool isExplsion { get; set; }
    private float addValue { get; set; }

    void OnTriggerEnter(Collider other)
    {        
        this.isExplsion = true;
        this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];
        
        //����Script�A���z����m�T�w�B���ϥ��`
        Destroy(this.GetComponent<MoveController>());
        Destroy(this.GetComponent<RegularChangePictures>());

        other.GetComponent<EnemyLife>().DecreaseLife(1);
    }

    // Use this for initialization
    void Start()
    {
        this.addValue = 0;
        this.currentTextureIndex = 0;
        this.isExplsion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isExplsion)
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
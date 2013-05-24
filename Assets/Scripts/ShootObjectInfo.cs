using UnityEngine;
using System.Collections;

/// <summary>
/// ���Z��(����/�ĤH)�o�g�������T
/// </summary>
public class ShootObjectInfo : MonoBehaviour
{
    public int Damage;
    public GameDefinition.AttackType AttackType;

    public Texture[] ChangeTextures;        //���Ϫ��ϲ�
    public float ChangeTextureTime = 0.1f;  //�K�ϥ洫�ɶ����j
    public LayerMask ExplosiveLayer;

    private int currentTextureIndex { get; set; }
    private bool isExplsion { get; set; }
    private float addValue { get; set; }

    void OnTriggerStay(Collider other)
    {
        if (!this.isExplsion)
        {
            if ((this.ExplosiveLayer.value & (int)Mathf.Pow(2, other.gameObject.layer)) != 0)      //�P�w���z����Layer
            {
                if (Mathf.Abs(other.transform.position.x - this.transform.position.x) < 1)
                {
                    this.isExplsion = true;
                    this.renderer.material.mainTexture = this.ChangeTextures[this.currentTextureIndex];

                    //����Script�A���z����m�T�w�B���ϥ��`
                    Destroy(this.GetComponent<MoveController>());
                    Destroy(this.GetComponent<RegularChangePictures>());

                    //�B�z���P�I����������(�ĤH�B�D��)
                    if (other.gameObject.layer == (int)GameDefinition.GameLayout.Enemy)
                        other.GetComponent<EnemyPropertyInfo>().DecreaseLife(this.Damage);
                    else if (other.gameObject.layer == (int)GameDefinition.GameLayout.Role)
                        other.GetComponent<RolePropertyInfo>().DecreaseLife(this.Damage);
                }
            }
        }
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
            if (this.addValue >= this.ChangeTextureTime)
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
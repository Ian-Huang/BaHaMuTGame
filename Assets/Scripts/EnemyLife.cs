using UnityEngine;
using System.Collections;

/// <summary>
/// ����ĤH����q
/// </summary>
public class EnemyLife : MonoBehaviour
{
    public int TotalLife = 1;           //����ͩR�`��

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// ��֪����q�禡
    /// </summary>
    /// <param name="deLife">��֪��ƭ�</param>
    public void DecreaseLife(int deLife)
    {
        this.TotalLife -= deLife;
        
        //��ͩR�p��0�A�R������
        if (this.TotalLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
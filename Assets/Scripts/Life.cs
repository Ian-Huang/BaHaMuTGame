using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        if (this.TotalLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
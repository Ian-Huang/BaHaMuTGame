using UnityEngine;
using System.Collections;

/// <summary>
/// 控制敵人的血量
/// </summary>
public class EnemyLife : MonoBehaviour
{
    public int TotalLife = 1;           //物體生命總值

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// 減少物體血量函式
    /// </summary>
    /// <param name="deLife">減少的數值</param>
    public void DecreaseLife(int deLife)
    {
        this.TotalLife -= deLife;
        
        //當生命小於0，刪除物件
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
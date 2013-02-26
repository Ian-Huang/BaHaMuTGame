using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
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
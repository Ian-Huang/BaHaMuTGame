using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-23
/// Modify Date：2013-07-25
/// Author：Ian
/// Description：
///     控制物體的移動(速度、方向)
/// </summary>
public class MoveController : MonoBehaviour
{
    public float MoveSpeed = 5;                 //物體移動速度
    public Vector3 Direction = Vector3.zero;    //物體移動方向(使用Unity世界座標)

    public bool isRunning = true;               //控制是否運作

    // Update is called once per frame
    void Update()
    {
        if (this.isRunning)
            this.transform.position += this.Direction * Time.deltaTime * this.MoveSpeed;
    }

    /// <summary>
    /// 改變物件的移動速度
    /// </summary>
    /// <param name="speed">改變後的速度值</param>
    public void ChangeSpeed(float speed)
    {
        this.MoveSpeed = speed;
    }
}
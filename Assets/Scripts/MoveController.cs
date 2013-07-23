using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-23
/// Author：Ian
/// Description：
///     控制物體的移動(速度、方向)
/// </summary>
public class MoveController : MonoBehaviour
{
    public float MoveSpeed = 5;                 //物體移動速度
    public Vector3 Direction = Vector3.zero;    //物體移動方向(使用Unity世界座標)

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
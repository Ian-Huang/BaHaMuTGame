using UnityEngine;
using System.Collections;

/// <summary>
/// 控制物體的移動(速度、方向)
/// </summary>
public class MoveController : MonoBehaviour
{
    public float speed = 5;                         //物體移動速度
    public Vector3 Direction = Vector3.zero;        //物體移動方向(使用Unity世界座標)

    private bool isMoving { get; set; }

    // Use this for initialization
    void Start()
    {
        this.isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isMoving)
            this.transform.position += this.Direction * Time.deltaTime * this.speed;
    }

    /// <summary>
    /// 改變物件是否移動的狀態
    /// </summary>
    /// <param name="isChange">是或否</param>
    public void MovingState(bool isChange)
    {
        if (isChange)
            this.isMoving = true;
        else
            this.isMoving = false;
    }
}
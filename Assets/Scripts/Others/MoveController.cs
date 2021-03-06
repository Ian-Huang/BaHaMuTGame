﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-28
/// Author：Ian
/// Description：
///     控制物體的移動(速度、方向)
///     0828新增：可自行選擇Local、World座標
/// </summary>
public class MoveController : MonoBehaviour
{
    public float MoveSpeed = 5;                 //物體移動速度
    public bool isLocal = false;
    public Vector3 Direction = Vector3.zero;    //物體移動方向(使用Unity世界座標)

    public bool isRunning = true;               //控制是否運作

    // Update is called once per frame
    void Update()
    {
        if (this.isRunning)
        {
            if (this.isLocal)
                this.transform.position += transform.TransformDirection(this.Direction * Time.deltaTime * this.MoveSpeed);
            else
                this.transform.position += this.Direction * Time.deltaTime * this.MoveSpeed;
        }
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
using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-22
/// Modify Date：2013-07-25
/// Author：Ian
/// Description：
///     背景控制器(控制背景移動、判斷背景移動的狀態)
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public static BackgroundController script;

    public float BackgroundMoveSpeed;   //背景移動的速度
    public bool isRunning;              //控制是否運作

    void Awake()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        //判斷是否要進行移動
        if (this.isRunning)
            this.transform.Translate(this.BackgroundMoveSpeed * Time.deltaTime, 0, 0);
    }
}

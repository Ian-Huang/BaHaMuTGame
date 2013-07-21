using UnityEngine;
using System.Collections;

/// <summary>
/// 事件控制器(綁在人物上，用來偵測觸發事件)
/// </summary>
public class EventController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("EndPosition") == 0)
        {
            //抵達終點，做以下處理(停止背景移動、腳色切換Idle狀態)
            BackgroundController.script.isRunning = false;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

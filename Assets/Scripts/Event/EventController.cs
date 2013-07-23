using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-23
/// Author：Ian
/// Description：
///     事件控制器(用來偵測觸發事件)
/// </summary>
public class EventController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EventTriggerType>().Type == GameDefinition.EventTriggerType.出怪點)
        {
            //觸發出怪事件，做以下處理
            other.GetComponent<EnemyCreatePoint>().CreateEnemy();
        }
        else if (other.GetComponent<EventTriggerType>().Type == GameDefinition.EventTriggerType.終點)
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

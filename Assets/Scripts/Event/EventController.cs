using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-23
/// Author�GIan
/// Description�G
///     �ƥ󱱨(�ΨӰ���Ĳ�o�ƥ�)
/// </summary>
public class EventController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EventTriggerType>().Type == GameDefinition.EventTriggerType.�X���I)
        {
            //Ĳ�o�X�Ǩƥ�A���H�U�B�z
            other.GetComponent<EnemyCreatePoint>().CreateEnemy();
        }
        else if (other.GetComponent<EventTriggerType>().Type == GameDefinition.EventTriggerType.���I)
        {
            //��F���I�A���H�U�B�z(����I�����ʡB�}�����Idle���A)
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

using UnityEngine;
using System.Collections;

/// <summary>
/// �ƥ󱱨(�j�b�H���W�A�ΨӰ���Ĳ�o�ƥ�)
/// </summary>
public class EventController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("EndPosition") == 0)
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

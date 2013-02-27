using UnityEngine;
using System.Collections;

/// <summary>
/// ����骺����(�t�סB��V)
/// </summary>
public class MoveController : MonoBehaviour
{
    public float speed = 5;                         //���鲾�ʳt��
    public Vector3 Direction = Vector3.zero;        //���鲾�ʤ�V(�ϥ�Unity�@�ɮy��)

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.Direction * Time.deltaTime * this.speed;
    }
}
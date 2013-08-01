using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-23
/// Modify Date�G2013-07-25
/// Author�GIan
/// Description�G
///     ����骺����(�t�סB��V)
/// </summary>
public class MoveController : MonoBehaviour
{
    public float MoveSpeed = 5;                 //���鲾�ʳt��
    public Vector3 Direction = Vector3.zero;    //���鲾�ʤ�V(�ϥ�Unity�@�ɮy��)

    public bool isRunning = true;               //����O�_�B�@

    // Update is called once per frame
    void Update()
    {
        if (this.isRunning)
            this.transform.position += this.Direction * Time.deltaTime * this.MoveSpeed;
    }

    /// <summary>
    /// ���ܪ��󪺲��ʳt��
    /// </summary>
    /// <param name="speed">���ܫ᪺�t�׭�</param>
    public void ChangeSpeed(float speed)
    {
        this.MoveSpeed = speed;
    }
}
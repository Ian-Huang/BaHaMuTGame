using UnityEngine;
using System.Collections;

/// <summary>
/// ����骺����(�t�סB��V)
/// </summary>
public class MoveController : MonoBehaviour
{
    public float speed = 5;                         //���鲾�ʳt��
    public Vector3 Direction = Vector3.zero;        //���鲾�ʤ�V(�ϥ�Unity�@�ɮy��)

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
    /// ���ܪ���O�_���ʪ����A
    /// </summary>
    /// <param name="isChange">�O�Χ_</param>
    public void MovingState(bool isChange)
    {
        if (isChange)
            this.isMoving = true;
        else
            this.isMoving = false;
    }
}
using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-22
/// Modify Date�G2013-07-25
/// Author�GIan
/// Description�G
///     �I�����(����I�����ʡB�P�_�I�����ʪ����A)
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public static BackgroundController script;

    public float BackgroundMoveSpeed;   //�I�����ʪ��t��
    public bool isRunning;              //����O�_�B�@

    void Awake()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        //�P�_�O�_�n�i�沾��
        if (this.isRunning)
            this.transform.Translate(this.BackgroundMoveSpeed * Time.deltaTime, 0, 0);
    }
}

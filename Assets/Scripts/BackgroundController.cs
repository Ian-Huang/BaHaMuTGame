using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date�G2013-07-22
/// Modify Date�G2013-07-30
/// Author�GIan
/// Description�G
///     �I�����(����I�����ʡB�P�_�I�����ʪ����A)
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public static BackgroundController script;

    /// <summary>
    /// �I����ƶ��X
    /// </summary>
    [System.Serializable]
    public class BackgroundData
    {
        public string Name;
        public GameObject Background;
        public float MoveSpeed;
    }
    public List<BackgroundData> BackgroundList = new List<BackgroundData>();    //�I����ƶ��X�M��

    public bool isRunning;              //����O�_�B�@

    void Awake()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        //�P�_�O�_�n���ʭI��
        if (this.isRunning)
        {
            //Ū���I���M�椺��T�A�̾ڤ��P�ƭȱ���ʳt��
            foreach (var data in this.BackgroundList)
                data.Background.transform.Translate(-data.MoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}

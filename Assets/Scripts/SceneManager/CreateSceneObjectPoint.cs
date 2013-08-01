using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date�G2013-07-31
/// Modify Date�G2013-07-31
/// Author�GIan
/// Description�G
///     �H�����ͳ��������˹�����
/// </summary>
public class CreateSceneObjectPoint : MonoBehaviour
{
    public float CreateTimeMin;         //�ͦ�����ɶ��̤p��(��)
    public float CreateTimeMax;         //�ͦ�����ɶ��̤j��(��)
    public float CreateOffsetY;         //�ͦ�����Y�b�t��(�H���ͦ��� -Y ~ Y ��)
    public GameObject ParentObject;     //�ͦ����󪺤�����
    public GameObject[] CreateObjects;  //�ͦ�����M��(�H���ͦ�)

    /// <summary>
    /// ���ͳ������˹�����
    /// </summary>
    void CreateSceneObject()
    {
        int random = Random.Range(0, this.CreateObjects.Length);
        GameObject obj = (GameObject)Instantiate
            (this.CreateObjects[random],
            new Vector3(this.transform.position.x, this.transform.position.y + Random.Range(-this.CreateOffsetY, this.CreateOffsetY),
            this.transform.position.z), this.CreateObjects[random].transform.rotation);
        obj.transform.parent = this.ParentObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //�T�{�����O�_�A���ʡA�p�G�S�����i�沣�ͪ���
        if (BackgroundController.script.isRunning)
            if (!IsInvoking("CreateSceneObject"))
                Invoke("CreateSceneObject", Random.Range(this.CreateTimeMin, this.CreateTimeMax));
    }
}
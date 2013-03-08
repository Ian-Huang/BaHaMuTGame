using UnityEngine;
using System.Collections;

/// <summary>
/// 隨機產生場景中的裝飾物件
/// </summary>
public class CreateSceneObjectPoint : MonoBehaviour
{
    public GameObject CreateObject;
    public float CreateTimeMin;
    public float CreateTimeMax;
    public float CreateOffsetY = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// 產生物件
    /// </summary>
    void Create()
    {
        GameObject obj = (GameObject)Instantiate
            (
            this.CreateObject, 
            new Vector3(this.transform.position.x, this.transform.position.y + Random.Range(-this.CreateOffsetY, this.CreateOffsetY), 
            this.transform.position.z), this.CreateObject.transform.rotation
            );
        obj.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInvoking("Create"))
            Invoke("Create", Random.Range(this.CreateTimeMin, this.CreateTimeMax));
    }
}
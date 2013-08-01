using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-31
/// Modify Date：2013-07-31
/// Author：Ian
/// Description：
///     隨機產生場景中的裝飾物件
/// </summary>
public class CreateSceneObjectPoint : MonoBehaviour
{
    public float CreateTimeMin;         //生成物件時間最小值(秒)
    public float CreateTimeMax;         //生成物件時間最大值(秒)
    public float CreateOffsetY;         //生成物件Y軸差值(隨機生成於 -Y ~ Y 值)
    public GameObject ParentObject;     //生成物件的父物件
    public GameObject[] CreateObjects;  //生成物件清單(隨機生成)

    /// <summary>
    /// 產生場景的裝飾物件
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
        //確認場景是否再移動，如果沒有不進行產生物件
        if (BackgroundController.script.isRunning)
            if (!IsInvoking("CreateSceneObject"))
                Invoke("CreateSceneObject", Random.Range(this.CreateTimeMin, this.CreateTimeMax));
    }
}
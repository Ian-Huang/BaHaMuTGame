using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCreateController : MonoBehaviour
{
    public List<GameObject> GroupList = new List<GameObject>();
    private bool CreateFinish = false;

    // Use this for initialization
    void Start()
    {
        this.RunCreateEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CreateFinish)
        {
            if (GroupList[0].transform.childCount == 0)
            {
                Destroy(this.GroupList[0]);
                this.GroupList.RemoveAt(0);

                if (this.GroupList.Count == 0)
                {
                    //所有怪出完後....
                }
                else
                {
                    this.RunCreateEnemy();
                }
            }
        }
    }

    void RunCreateEnemy()
    {
        this.CreateFinish = false;
        foreach (var script in this.GroupList[0].GetComponentsInChildren<EnemyCreateInfo>())
        {
            //StartCoroutine("CreateEnemy", new CreateData(temp.delayTime, temp.CreateObject, temp.CreatePosition));
            StartCoroutine("CreateEnemy", script);
        }
    }

    IEnumerator CreateEnemy(EnemyCreateInfo script)
    {
        yield return new WaitForSeconds(script.delayTime);

        int random = Random.Range(0, script.CreateObject.Length);
        GameObject obj = (GameObject)Instantiate(
            script.CreateObject[random],
            script.CreatePosition[Random.Range(0, script.CreatePosition.Length)].position,
            script.CreateObject[random].transform.rotation
            );
        obj.transform.parent = this.GroupList[0].transform;
        Destroy(script.gameObject);
        this.CreateFinish = true;
    }

    //IEnumerator CreateEnemy(CreateData createObj)
    //{
    //    yield return new WaitForSeconds(createObj.delayTime);

    //    GameObject obj = (GameObject)Instantiate(createObj.CreateObject, createObj.CreatePosition.position, createObj.CreateObject.transform.rotation);
    //    obj.transform.parent = this.GroupList[0].transform;
    //}

    //public struct CreateData
    //{
    //    public float delayTime;
    //    public GameObject CreateObject;
    //    public Transform CreatePosition;


    //    /// <summary>
    //    /// 建構式
    //    /// </summary>
    //    /// <param name="time">延遲時間</param>
    //    /// <param name="obj">產生物件</param>
    //    /// <param name="position">產生位置</param>
    //    public CreateData(float time, GameObject obj, Transform position)
    //    {
    //        this.delayTime = time;
    //        this.CreateObject = obj;
    //        this.CreatePosition = position;
    //    }
    //}
}

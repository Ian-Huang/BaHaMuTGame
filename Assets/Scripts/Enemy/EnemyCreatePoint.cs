using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date：2013-07-23
/// Author：Ian
/// Description：
///     敵人產生點(當玩家觸發，開始製造敵人)
/// </summary>
public class EnemyCreatePoint : MonoBehaviour
{
    public int CreateCount;     //一次性產生多少隻怪( if CreateCount = 0 => Random 1 ~ RandomCreatePositionList.Count )
    public GameObject[] RandomCreateEnemyKindList;      //隨機產生怪物的清單
    public List<Transform> RandomCreatePositionList;    //產生點

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 開始製造敵人
    /// </summary>
    public void CreateEnemy()
    {
        // Set CreateCount
        if (this.CreateCount == 0)
            this.CreateCount = Random.Range(0, this.RandomCreatePositionList.Count) + 1;

        for (int i = 0; i < this.CreateCount; i++)
        {
            int enemyIndex = Random.Range(0, RandomCreateEnemyKindList.Length);
            int positionIndex = Random.Range(0, RandomCreatePositionList.Count);

            //Create Enemy
            GameObject newObj = (GameObject)Instantiate(
                this.RandomCreateEnemyKindList[enemyIndex],
                this.RandomCreatePositionList[positionIndex].position,
                this.RandomCreateEnemyKindList[enemyIndex].transform.rotation);
            this.RandomCreatePositionList.RemoveAt(positionIndex);

            newObj.transform.parent = this.transform;
        }
    }
}

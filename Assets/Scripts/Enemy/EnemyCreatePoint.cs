using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date：2013-07-23
/// Modify Date：2013-08-08
/// Author：Ian
/// Description：
///     敵人產生點(當玩家觸發，開始製造敵人)
///     新增王點設定：一段時間間隔出小兵
/// </summary>
public class EnemyCreatePoint : MonoBehaviour
{
    //-------Boss 專用參數-------
    public bool isBossPoint = false;    //確認是否為出王點
    public GameObject Boss;             //魔王物件
    public float AutocreateMintime;     //自動出小兵最小間隔秒數
    public float AutocreateMaxtime;     //自動出小兵最大間隔秒數
    public float DelayTimeStartCreate;  //產生魔王後，幾秒後開始出第一批小兵
    //-------Boss 專用參數-------

    public int CreateCount;     //一次性產生多少隻怪(假如 CreateCount = 0 => Random (1 ~ RandomCreatePositionList.Count) )
    public GameObject[] RandomCreateEnemyKindList;      //隨機產生怪物的清單
    public List<Transform> RandomCreatePositionList;    //隨機產生怪物的位置
    [HideInInspector]
    public List<Transform> originRandomCreatePositionList;

    void Start()
    {
        this.originRandomCreatePositionList.AddRange(this.RandomCreatePositionList);
    }

    /// <summary>
    /// 開始製造敵人
    /// </summary>
    public void CreateEnemy()
    {
        // 確認是否為持續生產怪物點
        if (!this.isBossPoint)
        {
            //  CreateCount = 0 , 產生敵人為隨機數
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
                //因產生位置不重複，所以移除已被產生的位置
                this.RandomCreatePositionList.RemoveAt(positionIndex);
                //設定物件的parent
                newObj.transform.parent = this.transform;
            }
        }
        else
        {
            GameObject newObj = (GameObject)Instantiate(
                    this.Boss,
                    this.RandomCreatePositionList[Random.Range(0, this.RandomCreatePositionList.Count)].position,
                    this.Boss.transform.rotation);

            //設定物件的parent
            newObj.transform.parent = this.transform;
            StartCoroutine(AutoCreateEnemy(this.DelayTimeStartCreate));
        }
    }

    /// <summary>
    /// 一段時間自動產生敵人
    /// </summary>
    /// <param name="time">上次與下次產生的時間間隔</param>
    /// <returns></returns>
    IEnumerator AutoCreateEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        //重新複製原始位置清單
        this.RandomCreatePositionList.Clear();
        this.RandomCreatePositionList.AddRange(this.originRandomCreatePositionList);

        //  CreateCount = 0 , 產生敵人為隨機數
        int count;
        if (this.CreateCount == 0)
            count = Random.Range(0, this.RandomCreatePositionList.Count) + 1;
        else
            count = this.CreateCount;

        for (int i = 0; i < count; i++)
        {
            int enemyIndex = Random.Range(0, RandomCreateEnemyKindList.Length);
            int positionIndex = Random.Range(0, RandomCreatePositionList.Count);

            //Create Enemy
            GameObject newObj = (GameObject)Instantiate(
                this.RandomCreateEnemyKindList[enemyIndex],
                this.RandomCreatePositionList[positionIndex].position,
                this.RandomCreateEnemyKindList[enemyIndex].transform.rotation);
            //因產生位置不重複，所以移除已被產生的位置
            this.RandomCreatePositionList.RemoveAt(positionIndex);
            //設定物件的parent
            newObj.transform.parent = this.transform;
        }

        StartCoroutine(AutoCreateEnemy(Random.Range(this.AutocreateMintime, this.AutocreateMaxtime)));
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modify Date：2013-08-29
/// Author：Ian
/// Description：
///     敵人產生點(當玩家觸發，開始製造敵人)
///     0808：新增王點設定，一段時間間隔出小兵
///     0810：修正生怪數的設定方式(CreateCountMin、CreateCountMax)
///     0829：新增絕招系統呼叫使用函式
/// </summary>
public class EnemyCreatePoint : MonoBehaviour
{
    public int CreateCountMin;  //最小生怪數
    public int CreateCountMax;  //最大生怪數

    public GameObject[] RandomCreateEnemyKindList;      //隨機產生怪物的清單
    public List<Transform> RandomCreatePositionList;    //隨機產生怪物的位置
    [HideInInspector]
    public List<Transform> originRandomCreatePositionList;

    //-------Boss 專用參數-------
    public bool isBossPoint = false;    //確認是否為出王點
    public GameObject Boss;             //魔王物件
    public float AutocreateMintime;     //自動出小兵最小間隔秒數
    public float AutocreateMaxtime;     //自動出小兵最大間隔秒數
    public float DelayTimeStartCreate;  //產生魔王後，幾秒後開始出第一批小兵
    //-------Boss 專用參數-------

    void Start()
    {
        this.originRandomCreatePositionList.AddRange(this.RandomCreatePositionList);

        //防呆用，避免CreateCountMin > CreateCountMax
        if (this.CreateCountMin > this.CreateCountMax)
        {
            int temp = this.CreateCountMin;
            this.CreateCountMin = this.CreateCountMax;
            this.CreateCountMax = temp;
        }
    }

    /// <summary>
    /// 開始製造敵人
    /// </summary>
    public void CreateEnemy()
    {
        // 確認是否為持續生產怪物點(Boss專用)
        if (this.isBossPoint)
        {
            if (this.Boss != null)
            {
                GameObject newObj = (GameObject)Instantiate(
                        this.Boss,
                        this.RandomCreatePositionList[Random.Range(0, this.RandomCreatePositionList.Count)].position,
                        this.Boss.transform.rotation);

                //設定物件的parent
                newObj.transform.parent = this.transform;
            }

            StartCoroutine(AutoCreateEnemy(this.DelayTimeStartCreate));
        }
        else
        {
            //隨機生怪 CreateCountMin~CreateCountMax 之間，假如CreateCountMax大於生怪位置數，則等於生怪位置數
            int createCount;
            if (this.CreateCountMax > this.RandomCreatePositionList.Count)
                createCount = Random.Range(this.CreateCountMin, this.RandomCreatePositionList.Count + 1);
            else
                createCount = Random.Range(this.CreateCountMin, this.CreateCountMax + 1);

            for (int i = 0; i < createCount; i++)
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
    }

    /// <summary>
    /// 暫停生怪狀態
    /// </summary>
    public void PauseCreate()
    {
        this.StopAllCoroutines();
    }
    /// <summary>
    /// 恢復生怪狀態
    /// </summary>
    public void ResumeCreate()
    {
        this.StartCoroutine(AutoCreateEnemy(Random.Range(this.AutocreateMintime, this.AutocreateMaxtime) / 2));
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

        //隨機生怪 CreateCountMin~CreateCountMax 之間，假如CreateCountMax大於生怪位置數，則等於生怪位置數
        int createCount;
        if (this.CreateCountMax > this.RandomCreatePositionList.Count)
            createCount = Random.Range(this.CreateCountMin, this.RandomCreatePositionList.Count + 1);
        else
            createCount = Random.Range(this.CreateCountMin, this.CreateCountMax + 1);

        for (int i = 0; i < createCount; i++)
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
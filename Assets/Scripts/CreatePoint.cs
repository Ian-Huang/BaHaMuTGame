using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 敵人生產點(固定時間、位置隨機、物件隨機)
/// </summary>
public class CreatePoint : MonoBehaviour
{
    public GameObject[] CreateObjects;      //待Create的物件陣列
    public Transform[] CreatePositions;     //待Create的位置陣列
    public float CreateTime = 2;

    private float addValue { get; set; }
    private List<int> tempPositionIndexList { get; set; }   //儲存已產生敵人的位置(因不重複生產)
    private List<int> tempObjectIndexList { get; set; }

    // Use this for initialization
    void Start()
    {
        this.addValue = 0;
        this.tempPositionIndexList = new List<int>();
        this.tempObjectIndexList = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        this.addValue += Time.deltaTime;
        if (this.addValue > this.CreateTime)
        {
            this.addValue = 0;
            this.tempPositionIndexList.Clear();
            this.tempObjectIndexList.Clear();
            for (int i = 0; i < Random.Range(1, this.CreatePositions.Length); i++)
            {
                int tempPos;
                do
                {
                    tempPos = Random.Range(0, this.CreatePositions.Length);
                } while (this.tempPositionIndexList.Contains(tempPos));
                this.tempPositionIndexList.Add(tempPos);

                int tempObj;
                do
                {
                    tempObj = Random.Range(0, this.CreateObjects.Length);
                } while (this.tempObjectIndexList.Contains(tempObj));
                this.tempObjectIndexList.Add(tempObj);

                Instantiate(this.CreateObjects[tempObj], this.CreatePositions[tempPos].position,this.CreateObjects[tempObj].transform.rotation);
            }
        }
    }
}
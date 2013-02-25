using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatePoint : MonoBehaviour
{
    public GameObject[] CreateObjects;
    public Transform[] CreatePositions;
    public float CreateTime = 2;

    private float addValue { get; set; }
    public List<int> tempPositionList { get; set; }
    public List<int> tempObjectList { get; set; }

    // Use this for initialization
    void Start()
    {
        this.addValue = 0;
        this.tempPositionList = new List<int>();
        this.tempObjectList = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        this.addValue += Time.deltaTime;
        if (this.addValue > this.CreateTime)
        {
            this.addValue = 0;
            this.tempPositionList.Clear();
            this.tempObjectList.Clear();
            for (int i = 0; i < Random.Range(1, this.CreatePositions.Length); i++)
            {
                int tempPos;
                do
                {
                    tempPos = Random.Range(0, this.CreatePositions.Length);
                } while (this.tempPositionList.Contains(tempPos));
                this.tempPositionList.Add(tempPos);

                int tempObj;
                do
                {
                    tempObj = Random.Range(0, this.CreateObjects.Length);
                } while (this.tempObjectList.Contains(tempObj));
                this.tempObjectList.Add(tempObj);

                Instantiate(this.CreateObjects[tempObj], this.CreatePositions[tempPos].position,this.CreateObjects[tempObj].transform.rotation);
            }
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �ĤH�Ͳ��I(�T�w�ɶ��B��m�H���B�����H��)
/// </summary>
public class CreatePoint : MonoBehaviour
{
    public GameObject[] CreateObjects;      //��Create������}�C
    public Transform[] CreatePositions;     //��Create����m�}�C
    public float CreateTime = 2;

    private float addValue { get; set; }
    private List<int> tempPositionIndexList { get; set; }   //�x�s�w���ͼĤH����m(�]�����ƥͲ�)
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
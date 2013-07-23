using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date�G2013-07-23
/// Author�GIan
/// Description�G
///     �ĤH�����I(���aĲ�o�A�}�l�s�y�ĤH)
/// </summary>
public class EnemyCreatePoint : MonoBehaviour
{
    public int CreateCount;     //�@���ʲ��ͦh�ְ���( if CreateCount = 0 => Random 1 ~ RandomCreatePositionList.Count )
    public GameObject[] RandomCreateEnemyKindList;      //�H�����ͩǪ����M��
    public List<Transform> RandomCreatePositionList;    //�����I

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// �}�l�s�y�ĤH
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

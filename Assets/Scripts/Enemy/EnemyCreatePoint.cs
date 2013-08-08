using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date�G2013-07-23
/// Modify Date�G2013-08-08
/// Author�GIan
/// Description�G
///     �ĤH�����I(���aĲ�o�A�}�l�s�y�ĤH)
///     �s�W���I�]�w�G�@�q�ɶ����j�X�p�L
/// </summary>
public class EnemyCreatePoint : MonoBehaviour
{
    //-------Boss �M�ΰѼ�-------
    public bool isBossPoint = false;    //�T�{�O�_���X���I
    public GameObject Boss;             //�]������
    public float AutocreateMintime;     //�۰ʥX�p�L�̤p���j���
    public float AutocreateMaxtime;     //�۰ʥX�p�L�̤j���j���
    public float DelayTimeStartCreate;  //�����]����A�X���}�l�X�Ĥ@��p�L
    //-------Boss �M�ΰѼ�-------

    public int CreateCount;     //�@���ʲ��ͦh�ְ���(���p CreateCount = 0 => Random (1 ~ RandomCreatePositionList.Count) )
    public GameObject[] RandomCreateEnemyKindList;      //�H�����ͩǪ����M��
    public List<Transform> RandomCreatePositionList;    //�H�����ͩǪ�����m
    [HideInInspector]
    public List<Transform> originRandomCreatePositionList;

    void Start()
    {
        this.originRandomCreatePositionList.AddRange(this.RandomCreatePositionList);
    }

    /// <summary>
    /// �}�l�s�y�ĤH
    /// </summary>
    public void CreateEnemy()
    {
        // �T�{�O�_������Ͳ��Ǫ��I
        if (!this.isBossPoint)
        {
            //  CreateCount = 0 , ���ͼĤH���H����
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
                //�]���ͦ�m�����ơA�ҥH�����w�Q���ͪ���m
                this.RandomCreatePositionList.RemoveAt(positionIndex);
                //�]�w����parent
                newObj.transform.parent = this.transform;
            }
        }
        else
        {
            GameObject newObj = (GameObject)Instantiate(
                    this.Boss,
                    this.RandomCreatePositionList[Random.Range(0, this.RandomCreatePositionList.Count)].position,
                    this.Boss.transform.rotation);

            //�]�w����parent
            newObj.transform.parent = this.transform;
            StartCoroutine(AutoCreateEnemy(this.DelayTimeStartCreate));
        }
    }

    /// <summary>
    /// �@�q�ɶ��۰ʲ��ͼĤH
    /// </summary>
    /// <param name="time">�W���P�U�����ͪ��ɶ����j</param>
    /// <returns></returns>
    IEnumerator AutoCreateEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        //���s�ƻs��l��m�M��
        this.RandomCreatePositionList.Clear();
        this.RandomCreatePositionList.AddRange(this.originRandomCreatePositionList);

        //  CreateCount = 0 , ���ͼĤH���H����
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
            //�]���ͦ�m�����ơA�ҥH�����w�Q���ͪ���m
            this.RandomCreatePositionList.RemoveAt(positionIndex);
            //�]�w����parent
            newObj.transform.parent = this.transform;
        }

        StartCoroutine(AutoCreateEnemy(Random.Range(this.AutocreateMintime, this.AutocreateMaxtime)));
    }

}

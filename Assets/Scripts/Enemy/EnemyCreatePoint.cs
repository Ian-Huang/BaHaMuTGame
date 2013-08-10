using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date�G2013-07-23
/// Modify Date�G2013-08-10
/// Author�GIan
/// Description�G
///     �ĤH�����I(���aĲ�o�A�}�l�s�y�ĤH)
///     0808�G�s�W���I�]�w�A�@�q�ɶ����j�X�p�L
///     0810�G�ץ��ͩǼƪ��]�w�覡(CreateCountMin�BCreateCountMax)
/// </summary>
public class EnemyCreatePoint : MonoBehaviour
{
    public int CreateCountMin;  //�̤p�ͩǼ�
    public int CreateCountMax;  //�̤j�ͩǼ�

    public GameObject[] RandomCreateEnemyKindList;      //�H�����ͩǪ����M��
    public List<Transform> RandomCreatePositionList;    //�H�����ͩǪ�����m
    [HideInInspector]
    public List<Transform> originRandomCreatePositionList;

    //-------Boss �M�ΰѼ�-------
    public bool isBossPoint = false;    //�T�{�O�_���X���I
    public GameObject Boss;             //�]������
    public float AutocreateMintime;     //�۰ʥX�p�L�̤p���j���
    public float AutocreateMaxtime;     //�۰ʥX�p�L�̤j���j���
    public float DelayTimeStartCreate;  //�����]����A�X���}�l�X�Ĥ@��p�L
    //-------Boss �M�ΰѼ�-------

    void Start()
    {
        this.originRandomCreatePositionList.AddRange(this.RandomCreatePositionList);

        //���b�ΡA�קKCreateCountMin > CreateCountMax
        if (this.CreateCountMin > this.CreateCountMax)
        {
            int temp = this.CreateCountMin;
            this.CreateCountMin = this.CreateCountMax;
            this.CreateCountMax = temp;
        }
    }

    /// <summary>
    /// �}�l�s�y�ĤH
    /// </summary>
    public void CreateEnemy()
    {
        // �T�{�O�_������Ͳ��Ǫ��I(Boss�M��)
        if (this.isBossPoint)
        {
            GameObject newObj = (GameObject)Instantiate(
                    this.Boss,
                    this.RandomCreatePositionList[Random.Range(0, this.RandomCreatePositionList.Count)].position,
                    this.Boss.transform.rotation);

            //�]�w����parent
            newObj.transform.parent = this.transform;
            StartCoroutine(AutoCreateEnemy(this.DelayTimeStartCreate));
        }
        else
        {
            //�H���ͩ� CreateCountMin~CreateCountMax �����A���pCreateCountMax�j��ͩǦ�m�ơA�h����ͩǦ�m��
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
                //�]���ͦ�m�����ơA�ҥH�����w�Q���ͪ���m
                this.RandomCreatePositionList.RemoveAt(positionIndex);
                //�]�w����parent
                newObj.transform.parent = this.transform;
            }
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

        //�H���ͩ� CreateCountMin~CreateCountMax �����A���pCreateCountMax�j��ͩǦ�m�ơA�h����ͩǦ�m��
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
            //�]���ͦ�m�����ơA�ҥH�����w�Q���ͪ���m
            this.RandomCreatePositionList.RemoveAt(positionIndex);
            //�]�w����parent
            newObj.transform.parent = this.transform;
        }

        StartCoroutine(AutoCreateEnemy(Random.Range(this.AutocreateMintime, this.AutocreateMaxtime)));
    }
}
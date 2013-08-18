using UnityEngine;
using System.Collections;

#region ＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃修正紀錄＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃
/// 13/07/28    建置

#endregion
/// <summary>
/// 教學系統
/// </summary>
public class TeachingSystem : MonoBehaviour
{
    public static TeachingSystem script;
    //目前階段編號
    public static int currentPartNumber;
    //教學的段落陣列
    public GameObject[] TechingParts;
    //教學模式等待開始時間
    public float delayStartTime;

    // Use this for initialization
    void Start()
    {
        script = this;
        StartCoroutine(NextPart(delayStartTime));
    }

    // Update is called once per frame
    void Update()
    {
        //測試用
        if (Input.GetKeyDown(KeyCode.C))
            StartCoroutine(NextPart(0));


      
    }

    /// <summary>
    /// 下一個段落
    /// </summary>
    public IEnumerator NextPart(float time)
    {  
        yield return new WaitForSeconds(time);

        //關閉上一個段落
        if (currentPartNumber - 1 >= 0)
            TechingParts[currentPartNumber - 1].SetActive(false);

        //開啟下一個段落
        if (currentPartNumber < TechingParts.Length)
        {
            TechingParts[currentPartNumber].SetActive(true);
            currentPartNumber++;
        }

      
    }
}

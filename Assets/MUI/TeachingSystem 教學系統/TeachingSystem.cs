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
    private int currentPartNumber;
    //教學的段落陣列
    public GameObject[] TechingParts;

    // Use this for initialization
    void Start()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        //測試用
        if (Input.GetKeyDown(KeyCode.C))
            NextPart();
    }

    /// <summary>
    /// 下一個段落
    /// </summary>
    public void NextPart()
    {
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date：2013-07-22
/// Modify Date：2013-07-30
/// Author：Ian
/// Description：
///     背景控制器(控制背景移動、判斷背景移動的狀態)
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public static BackgroundController script;

    /// <summary>
    /// 背景資料集合
    /// </summary>
    [System.Serializable]
    public class BackgroundData
    {
        public string Name;
        public GameObject Background;
        public float MoveSpeed;
    }
    public List<BackgroundData> BackgroundList = new List<BackgroundData>();    //背景資料集合清單

    public bool isRunning;              //控制是否運作

    void Awake()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        //判斷是否要移動背景
        if (this.isRunning)
        {
            //讀取背景清單內資訊，依據不同數值控制移動速度
            foreach (var data in this.BackgroundList)
                data.Background.transform.Translate(-data.MoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}

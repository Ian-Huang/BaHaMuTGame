using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modify Date：2013-08-27
/// Author：Ian
/// Description：
///     背景控制器(控制背景移動、判斷背景移動的狀態)
///     0827：修改isRunning 修改方式→ 呼叫SetRunBackgroundState()，並判斷角色動作的切換
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

    /// <summary>
    /// 設定背景控制器是否運作
    /// </summary>
    /// <param name="state">切換狀態</param>
    public void SetRunBackgroundState(bool state)
    {
        this.isRunning = state;

        //判斷當前背景移動狀況，如果無移動角色則使用"idleweak"
        RolePropertyInfo[] rolePropertyInfos = RolesCollection.script.gameObject.GetComponentsInChildren<RolePropertyInfo>();
        foreach (RolePropertyInfo script in rolePropertyInfos)
        {
            if (script.boneAnimation.IsPlaying("walkweak") || script.boneAnimation.IsPlaying("idleweak"))
            {
                if (state)
                    script.boneAnimation.Play("walkweak");
                else
                    script.boneAnimation.Play("idleweak");
            }
        }
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

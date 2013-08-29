using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Modify Date：2013-08-29
/// Description：
///     全域遊戲管理系統
///     0809新增：新增角色、怪物BoneAnimation管理系統，以控制Animation
///     0814新增：金幣物件Prefab
///     0821新增：效果動畫物件Prefab
///     0827新增：魔王回饋
///     0828新增：動畫註冊系統更新
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager script;

    public int CurrentCoinCount;
    public float CurrentMorale;
    [HideInInspector]
    public float MaxMorale;
    [HideInInspector]
    public int MoraleRestoreRate;

    public GameObject StartPosition;
    public GameObject EndPosition;
    [HideInInspector]
    public float TotalDistance;

    [HideInInspector]
    public GameObject ChangeButtonUIObject; //交換位置按鈕UI的物件(使用絕技時要將UI隱藏)

    public GameObject CoinObject;               //金幣物件Prefab
    public SmoothMoves.BoneAnimation EffectAnimationObject;    //效果動畫物件Prefab

    public Dictionary<SmoothMoves.BoneAnimation, bool> AllBoneAnimationList = new Dictionary<SmoothMoves.BoneAnimation, bool>();

    /// <summary>
    /// 註冊BoneAnimation(角色、敵人必須註冊，方便系統控制)
    /// </summary>
    /// <param name="boneAnimation">角色/敵人的BoneAnimation</param>
    public void RegisterBoneAnimation(SmoothMoves.BoneAnimation boneAnimation)
    {
        this.AllBoneAnimationList.Add(boneAnimation, true);
    }

    /// <summary>
    /// 得到目前BoneAnimation的狀態(True為正在使用、False為暫停使用)
    /// </summary>
    /// <param name="boneAnimation">角色/敵人的BoneAnimation</param>
    /// <returns>目前BoneAnimation的狀態</returns>
    public bool GetBoneAnimationState(SmoothMoves.BoneAnimation boneAnimation)
    {
        return this.AllBoneAnimationList[boneAnimation];
    }

    /// <summary>
    /// 暫停所有註冊的BoneAnimation (去除參數的BoneAnimation)
    /// </summary>
    /// <param name="dontStopAnimation">不暫停的BoneAnimation</param>
    public void StopAllBoneAnimation(SmoothMoves.BoneAnimation dontStopAnimation = null)
    {
        lock (this.AllBoneAnimationList)
        {
            //從List 複製到local arrays
            SmoothMoves.BoneAnimation[] allAnimations = new SmoothMoves.BoneAnimation[this.AllBoneAnimationList.Keys.Count];
            this.AllBoneAnimationList.Keys.CopyTo(allAnimations, 0);
            List<SmoothMoves.BoneAnimation> animationList = new List<SmoothMoves.BoneAnimation>(allAnimations);

            //去除不必暫停物件
            if (dontStopAnimation != null)
                animationList.Remove(dontStopAnimation);

            for (int i = 0; i < animationList.Count; i++)
            {
                animationList[i].Stop();    //暫停BoneAnimation的運作
                this.AllBoneAnimationList[animationList[i]] = false;    //將狀態改為停止
            }
        }
    }

    /// <summary>
    /// 恢復所有註冊的BoneAnimation
    /// </summary>
    public void ResumeAllBoneAnimation()
    {
        lock (this.AllBoneAnimationList)
        {
            //從AllBoneAnimation 複製到local arrays
            SmoothMoves.BoneAnimation[] allAnimations = new SmoothMoves.BoneAnimation[this.AllBoneAnimationList.Keys.Count];
            this.AllBoneAnimationList.Keys.CopyTo(allAnimations, 0);

            for (int i = 0; i < allAnimations.Length; i++)
            {
                this.AllBoneAnimationList[allAnimations[i]] = true;    //將狀態改為正在使用
            }
        }
    }

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        this.ChangeButtonUIObject = GameObject.Find("ChangeButtons 隊伍位置交換按鈕");

        this.MaxMorale = GameDefinition.MaxMorale;
        this.CurrentMorale = GameDefinition.MaxMorale;
        this.MoraleRestoreRate = GameDefinition.MoraleRestoreRate;

        if (Application.loadedLevelName != "TeachScene")
            this.TotalDistance = Mathf.Abs(this.StartPosition.transform.position.x - this.EndPosition.transform.position.x);

        InvokeRepeating("RestoreMoralePersecond", 0.1f, 1);
    }

    /// <summary>
    /// 取得當前士氣值，已轉換為0~100
    /// </summary>
    /// <returns></returns>
    public float GetCurrentMorale()
    {
        return (this.CurrentMorale / this.MaxMorale) * 100;
    }

    // Update is called once per frame
    void Update()
    {
        //提供介面監控數值

        //f進度條For教學系統

        if (Application.loadedLevelName == "TeachScene")
            MUI_Monitor.script.SetValue("進度條x", (TeachingSystem.script.currentPartNumber / 15F) * 100);
        else
        {
            //進度條
            MUI_Monitor.script.SetValue("進度條x", (1 - Mathf.Abs(this.StartPosition.transform.position.x - this.EndPosition.transform.position.x) / this.TotalDistance) * 100);
        }
        //士氣條
        MUI_Monitor.script.SetValue("士氣條" + "x", (this.CurrentMorale / this.MaxMorale) * 100);


        //魔王血條        
        BossPropertyInfo script = (BossPropertyInfo)GameObject.FindObjectOfType(typeof(BossPropertyInfo));
        if (script)
            MUI_Monitor.script.SetValue("魔王血條" + "x", (script.currentLife / script.maxLife) * 100);
    }

    /// <summary>
    /// 每秒固定回復士氣值
    /// </summary>
    void RestoreMoralePersecond()
    {
        this.CurrentMorale += this.MoraleRestoreRate;
        if (this.CurrentMorale >= this.MaxMorale)
            this.CurrentMorale = this.MaxMorale;
    }
}

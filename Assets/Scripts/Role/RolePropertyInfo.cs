﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Create Date：2013-07-24
/// Modify Date：2013-08-03
/// Author：Ian
/// Description：
///     角色的屬性資訊
/// </summary>
public class RolePropertyInfo : MonoBehaviour
{
    public GameDefinition.Role Role;
    public int currentLife; //當前生命值
    public int maxLife;     //最大生命值
    public int cureRate;    //每秒回復生命速率
    public int defence;     //防禦力
    public int nearDamage;  //近距離攻擊傷害值
    public int farDamage;   //遠距離攻擊傷害值

    public bool isWeak { get; private set; }
    public int WeakCureScale = 10;              //Weak狀態 回復速率

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isWeak = false;

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();

        //讀取系統儲存的角色屬性資料
        GameDefinition.RoleData getData = GameDefinition.RoleList.Find((GameDefinition.RoleData data) => { return data.RoleName == Role; });
        this.maxLife = getData.Life;
        this.currentLife = getData.Life;
        this.cureRate = getData.CureRate;
        this.defence = getData.Defence;
        this.nearDamage = getData.NearDamage;
        this.farDamage = getData.FarDamage;

        InvokeRepeating("RestoreLifePersecond", 0.1f, 1);
    }

    /// <summary>
    /// 減少角色血量函式
    /// </summary>
    /// <param name="deLife">減少的數值</param>
    public void DecreaseLife(int deLife)
    {
        //扣除防禦力
        deLife -= this.defence;
        if (deLife <= 0)
            deLife = 1;

        if (!this.isWeak)
        {
            //角色當前未虛弱，扣角色的生命值
            this.currentLife -= deLife;
        }
        else
        {
            //角色當前虛弱，扣總士氣(未完成)
            GameManager.script.CurrentMorale -= deLife;
            if (GameManager.script.CurrentMorale <= 0)
                GameManager.script.CurrentMorale = 0;
        }

        if (!this.isWeak)
        {
            //當生命小於0
            if (this.currentLife <= 0)
            {
                this.currentLife = 0;
                this.isWeak = true;
                //判斷當前背景移動狀況，如果無移動則使用"idleweak"
                if (BackgroundController.script.isRunning)
                    this.boneAnimation.Play("walkweak");
                else
                    this.boneAnimation.Play("idleweak");
            }
        }
    }

    /// <summary>
    /// 每秒固定回復生命
    /// </summary>
    void RestoreLifePersecond()
    {
        //未虛弱，回復速率正常
        if (!this.isWeak)
        {
            this.currentLife += this.cureRate;
            if (this.currentLife >= this.maxLife)
                this.currentLife = this.maxLife;
        }
        else
        {
            //虛弱狀態，回復速率 * WeakCureScale
            this.currentLife += (this.cureRate * this.WeakCureScale);
            if (this.currentLife >= this.maxLife)
            {
                this.currentLife = this.maxLife;

                this.isWeak = false;
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date：2013-08-04
/// Modify Date：2013-08-07
/// Author：Ian
/// Description：
///     魔王控制器(測試用)
/// </summary>
public class BossController : MonoBehaviour
{
    public GameObject FarShootObject;   //遠距離射擊物件

    private Vector3 originScale;        //原始尺寸

    private MoveController moveController { get; set; }
    private EnemyPropertyInfo enemyInfo { get; set; }
    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        //載入敵人資訊
        this.enemyInfo = this.GetComponent<EnemyPropertyInfo>();
        this.moveController = this.GetComponent<MoveController>();

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(UserTrigger);

        //紀錄原始Scale
        this.originScale = this.transform.localScale;
    }

    private List<int> saveShootIndexList = new List<int>(); //紀錄射擊目標的清單
    private bool isShooting = false;        //確認現在是否為射擊狀態
    /// <summary>
    /// 魔王準備進行遠距離攻擊
    /// </summary>
    /// <param name="time">等待秒數後開始攻擊</param>
    /// <returns></returns>
    IEnumerator ReadyFarAttack(float time)
    {
        this.isShooting = true;

        int shootCount = Random.Range(2, 4);    //攻擊目標隨機為2~3人
        //紀錄準備被攻擊的目標，並顯示提示提醒玩家
        for (int i = 0; i < shootCount; i++)
        {
            int temp;
            do
            {
                //選擇的目標，必須不重複
                temp = Random.Range(0, 4);
            } while (saveShootIndexList.Contains(temp));

            this.saveShootIndexList.Add(temp);
            Instantiate(EffectCreator.script.道路危險提示[temp]);
        }

        yield return new WaitForSeconds(time);      //等待n秒

        this.boneAnimation.Play("發射");
    }

    // Update is called once per frame
    void Update()
    {
        #region TEST

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (!this.isShooting)
                StartCoroutine(ReadyFarAttack(1));  //等待n秒後，進行遠距離攻擊
        }

        this.transform.localScale = this.originScale;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.boneAnimation.Play("run");
            this.moveController.ChangeSpeed(15);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.boneAnimation.Play("run");
            Vector3 v3Scale = this.originScale;
            v3Scale.x = -Mathf.Abs(v3Scale.x);
            this.boneAnimation.mLocalTransform.localScale = v3Scale;
            this.moveController.ChangeSpeed(-15);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            this.boneAnimation.Play("walk");
            this.transform.Translate(0, Time.deltaTime, 0);
            this.moveController.ChangeSpeed(0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.boneAnimation.Play("walk");
            this.transform.Translate(0, -Time.deltaTime, 0);
            this.moveController.ChangeSpeed(0);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            this.boneAnimation.Play("出現");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            this.boneAnimation.Play("突刺");
        }
        else
        {
            if (!this.animation.isPlaying)
                this.boneAnimation.Play("idle");
            this.moveController.ChangeSpeed(0);
        }

        #endregion

    }

    /// <summary>
    /// UserTrigger，觸發判定
    /// </summary>
    /// <param name="triggerEvent">觸發相關資訊</param>
    public void UserTrigger(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //確認是由"出現"觸發的UserTrigger
        if (triggerEvent.animationName == "出現")
        {
            //鏡頭震動
            iTween.ShakePosition(Camera.main.gameObject, new Vector3(1, 1, 0), 0.15f);
        }

        //確認是由"發射"觸發的UserTrigger
        else if (triggerEvent.animationName == "發射")
        {
            //計算Boss與角色的距離
            float distance = Mathf.Abs(RolesCollection.script.Roles[0].transform.position.x - this.transform.position.x);

            //發射史萊姆砲
            foreach (var temp in this.saveShootIndexList)
            {
                Vector3 Posv3 = RolesCollection.script.Roles[temp].transform.position + new Vector3(distance, 0, 0);
                GameObject newObj = (GameObject)Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
                newObj.GetComponent<ShootObjectInfo>().Damage = this.enemyInfo.farDamage;
            }
            //清空紀錄標記的清單
            this.saveShootIndexList.Clear();
            this.isShooting = false;
        }
    }
}

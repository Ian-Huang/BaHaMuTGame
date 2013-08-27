using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-27
/// Author：Ian
/// Description：
///     金幣系統
///     0818新增：一定時間後金幣自動開始移動到指定位置
///     0819新增：新增四種錢幣種類，依照金錢數決定種類
///     0827新增：金幣系統註冊
/// </summary>
public class CoinPropertyInfo : MonoBehaviour
{
    public int coinAmount;                  //金幣數量
    public float AutoMoveTime;              //一段時間未被玩家撿取，則開始自動移動
    public float MoveToUITime;              //移動到"金幣總數UI"處的時間
    public Vector2 MoveToScreenUIPosition;  //"金幣總數UI"處的位置
    public LayerMask TargerLayer;           //被觸發的目標

    private bool isDisappear = false;
    private int[] CoinLevelList = new int[] { 100, 500, 1000 }; //錢幣等級的清單 (銅<銀<金<錢袋)

    void Start()
    {
        Invoke("StartMoveCoin", this.AutoMoveTime);
    }

    /// <summary>
    /// 開始移動金幣到UI處
    /// </summary>
    void StartMoveCoin()
    {
        // 金幣必須未消失
        if (!this.isDisappear)
        {
            this.isDisappear = true;
            this.GetComponent<SmoothMoves.BoneAnimation>().Stop();

            //計算移動到"金幣總數UI"處的位置，並轉換為unity世界座標
            Vector3 moveToPos = Camera.main.ScreenToWorldPoint(new Vector3(
                    Camera.main.pixelWidth * MoveToScreenUIPosition.x,
                    Camera.main.pixelHeight * (1 - MoveToScreenUIPosition.y),
                    Camera.main.nearClipPlane));

            //ITween , 控制金幣被角色碰到後移動至"金幣總數UI"處
            iTween.MoveTo(this.gameObject, iTween.Hash(
                "position", moveToPos,
                "time", this.MoveToUITime,
                "oncomplete", "MoveCoinComplete"
                ));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 金幣必須未消失
        if (!this.isDisappear)
        {
            //必須為角色層
            if (((1 << other.gameObject.layer) & this.TargerLayer.value) > 0)
            {
                //tag = MainBody
                if (other.collider.tag.CompareTo("MainBody") == 0)
                    this.StartMoveCoin();
            }
        }
    }

    /// <summary>
    /// 設定金幣的數量
    /// </summary>
    /// <param name="count">金幣數</param>
    public void SetCoinAmount(int count)
    {
        this.coinAmount = count;

        //判定要用哪種錢幣種類
        SmoothMoves.BoneAnimation boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        boneAnimation.playAutomatically = false;
        if (this.coinAmount < this.CoinLevelList[0])
            boneAnimation.Play("銅幣");

        else if (this.coinAmount < this.CoinLevelList[1])
            boneAnimation.Play("銀幣");

        else if (this.coinAmount < this.CoinLevelList[2])
            boneAnimation.Play("金幣");

        else
            boneAnimation.Play("錢袋");
    }

    /// <summary>
    /// 移動完成後，刪除自己
    /// </summary>
    void MoveCoinComplete()
    {
        //待補---增加金幣數
        //GameManager.script.CurrentCoinCount += this.coinAmount;
        PlayerPrefsDictionary.script.SetValue("Money", PlayerPrefsDictionary.script.GetValue("Money") + this.coinAmount);

        //刪除自己
        Destroy(this.gameObject);
    }
}
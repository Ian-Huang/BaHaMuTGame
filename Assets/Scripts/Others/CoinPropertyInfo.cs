using UnityEngine;
using System.Collections;

/// <summary>
/// 金幣系統
/// </summary>
public class CoinPropertyInfo : MonoBehaviour
{
    public int coinAmount;                  //金幣數量
    public float MoveToUITime;              //移動到"金幣總數UI"處的時間
    public Vector2 MoveToScreenUIPosition;  //"金幣總數UI"處的位置
    public LayerMask TargerLayer;           //被觸發的目標

    private bool isDisappear = false;

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
                        "oncomplete", "MoveComplete"
                        ));
                }
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
    }

    /// <summary>
    /// 移動完成後，刪除自己
    /// </summary>
    void MoveComplete()
    {
        //待補---增加金幣數
        //this.coinAmount

        //刪除自己
        Destroy(this.gameObject);
    }
}
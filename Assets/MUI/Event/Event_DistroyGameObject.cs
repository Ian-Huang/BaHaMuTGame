using UnityEngine;
using System.Collections;

#region ＃＃【事件類】相關資訊＃＃
//  MUI   事件：刪除物件
//指定刪除物件
//  【Bool】isMySelf  是否刪除自己？
//  【GameObject[]】Distroy_gameObjects　欲刪除的物件
//  【float】delayTime　刪除等待時間    
#endregion

public class Event_DistroyGameObject : MonoBehaviour
{
    //是否刪除自己？
    [SerializeField]
    private static bool isMySelf;

    //欲刪除的物件
    [SerializeField]
    private GameObject[] Distroy_gameObjects;

    //刪除等待時間
    public float delayTime;

    void Start()
    {
        foreach (GameObject gameobject in Distroy_gameObjects)
            Destroy(gameobject, delayTime);
        if (isMySelf)
            Destroy(this.gameObject, delayTime);
    }
}

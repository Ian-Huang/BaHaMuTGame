using UnityEngine;
using System.Collections;

#region ＃＃【事件類】相關資訊＃＃
//  MUI   事件：活化／不活化 物件
//指定活化／不活化物件
//  【Bool】isSetActive  是否刪除自己？
//  【GameObject[]】Distroy_gameObjects　欲刪除的物件
//  【float】delayTime　刪除等待時間    
#endregion

public class Event_GameObjectsSetActive : MonoBehaviour
{
    //Active的狀態
    public bool isSetActive;
    public GameObject[] gameObjects;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject gameobject in gameObjects)
            gameobject.SetActive(isSetActive);

        //Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

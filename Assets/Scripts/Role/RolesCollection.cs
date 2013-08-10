using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Create Date：2013-07-22
/// Modify Date：2013-08-06
/// Author：Ian
/// Description：
///     角色控制器(角色位置交換)
/// </summary>
public class RolesCollection : MonoBehaviour
{
    public static RolesCollection script;

    public GameObject[] Roles = new GameObject[4];

    public float changeTime;
    public bool isChanging = false;

    void Awake()
    {
        script = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (!this.isChanging)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                this.SetChange(ref this.Roles[0], ref this.Roles[1]);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                this.SetChange(ref this.Roles[1], ref this.Roles[2]);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                this.SetChange(ref this.Roles[2], ref this.Roles[3]);
            }
        }
    }

    /// <summary>
    /// Touch使用交換人物位置的方法
    /// </summary>
    /// <param name="mode">交換的位置</param>
    public void ChangeRolePosition(GameDefinition.ChangeRoleMode mode)
    {
        if (!this.isChanging)
        {
            switch (mode)
            {
                case GameDefinition.ChangeRoleMode.OneAndTwo:
                    this.SetChange(ref this.Roles[0], ref this.Roles[1]);
                    break;
                case GameDefinition.ChangeRoleMode.TwoAndThree:
                    this.SetChange(ref this.Roles[1], ref this.Roles[2]);
                    break;
                case GameDefinition.ChangeRoleMode.ThreeAndFour:
                    this.SetChange(ref this.Roles[2], ref this.Roles[3]);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 交換物體位置
    /// </summary>
    /// <param name="obj1">物體1</param>
    /// <param name="obj2">物體2</param>
    void SetChange(ref GameObject obj1, ref GameObject obj2)
    {
        this.isChanging = true;

        GameObject tempGameObject;
        Vector3 tempPosition;

        tempGameObject = obj1;
        tempPosition = obj1.transform.position;
        //obj1.transform.position = obj2.transform.position;
        //obj2.transform.position = tempPosition;

        iTween.MoveTo(obj1, iTween.Hash(
            "position", obj2.transform.position,
            "time", this.changeTime,
            "oncompletetarget", this.gameObject,
            "oncomplete", "ChangeComplete"
            ));

        iTween.MoveTo(obj2, iTween.Hash(
            "position", tempPosition,
            "time", this.changeTime
            ));

        obj1 = obj2;
        obj2 = tempGameObject;
    }

    /// <summary>
    /// 角色交換完成後觸發
    /// </summary>
    void ChangeComplete()
    {
        this.isChanging = false;
    }
}
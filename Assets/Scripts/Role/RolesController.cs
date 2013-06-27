using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// à︹北竟(à︹竚ユ传)
/// </summary>
public class RolesController : MonoBehaviour
{
    public static RolesController script;

    public GameObject Role1;
    public GameObject Role2;
    public GameObject Role3;
    public GameObject Role4;

    public float changeTime = 1;
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
                this.SetChange(ref this.Role1, ref this.Role2);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                this.SetChange(ref this.Role2, ref this.Role3);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                this.SetChange(ref this.Role3, ref this.Role4);
            }
        }
    }

    /// <summary>
    /// Touchㄏノユ传竚よ猭
    /// </summary>
    /// <param name="mode">ユ传竚</param>
    public void ChangeRolePosition(GameDefinition.ChangeRoleMode mode)
    {
        if (!this.isChanging)
        {
            switch (mode)
            {
                case GameDefinition.ChangeRoleMode.OneAndTwo:
                    this.SetChange(ref this.Role1, ref this.Role2);
                    break;
                case GameDefinition.ChangeRoleMode.TwoAndThree:
                    this.SetChange(ref this.Role2, ref this.Role3);
                    break;
                case GameDefinition.ChangeRoleMode.ThreeAndFour:
                    this.SetChange(ref this.Role3, ref this.Role4);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// ユ传砰竚
    /// </summary>
    /// <param name="obj1">砰1</param>
    /// <param name="obj2">砰2</param>
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

    void ChangeComplete()
    {
        this.isChanging = false;
    }
}
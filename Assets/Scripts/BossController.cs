using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public GameObject FarShootObject;

    private Vector3 originScale;

    private MoveController moveController;
    private SmoothMoves.BoneAnimation boneAnimation;


    // Use this for initialization
    void Start()
    {
        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(UserTrigger);

        this.moveController = this.GetComponent<MoveController>();

        this.originScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        #region TEST

        if (Input.GetKeyDown(KeyCode.Y))
        {
            this.boneAnimation.Play("發射");
            Instantiate(EffectCreator.script.道路危險提示[0]);
            //Instantiate(EffectCreator.script.道路危險提示[0]);
            //Vector3 Posv3 = RolesCollection.script.Role1.transform.position;
            //Posv3.x = this.transform.position.x;
            //Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(EffectCreator.script.道路危險提示[1]);
            Vector3 Posv3 = RolesCollection.script.Role2.transform.position;
            Posv3.x = this.transform.position.x;
            Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(EffectCreator.script.道路危險提示[2]);
            Vector3 Posv3 = RolesCollection.script.Role3.transform.position;
            Posv3.x = this.transform.position.x;
            Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Instantiate(EffectCreator.script.道路危險提示[3]);
            Vector3 Posv3 = RolesCollection.script.Role4.transform.position;
            Posv3.x = this.transform.position.x;
            Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
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
            //發射史萊姆砲

            Vector3 Posv3 = RolesCollection.script.Role1.transform.position;
            Posv3.x = this.transform.position.x;
            Instantiate(this.FarShootObject, Posv3, this.FarShootObject.transform.rotation);
        }
    }
}

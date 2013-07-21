using UnityEngine;
using System.Collections;

/// <summary>
/// 腳色動畫控制器(使用SmoothMove)
/// </summary>
public class RoleAnimationController : MonoBehaviour
{
    public SmoothMoves.BoneAnimation boneAnimation; //SmoothMove BoneAnimation

    // Use this for initialization
    void Start()
    {
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();    //get role's SmoothMove BoneAnimation
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.boneAnimation.Play("walk");
            //this.boneAnimation.CrossFade("walk", 0.2f, PlayMode.StopSameLayer);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            this.boneAnimation.Play("weak");
            //this.boneAnimation.CrossFade("weak", 0.2f, PlayMode.StopSameLayer);
        }
    }
}

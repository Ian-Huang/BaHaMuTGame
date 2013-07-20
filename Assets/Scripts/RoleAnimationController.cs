using UnityEngine;
using System.Collections;

public class RoleAnimationController : MonoBehaviour
{
    public SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.boneAnimation.CrossFade("Walk");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            this.boneAnimation.CrossFade("weak");
        }
    }
}

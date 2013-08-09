using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modify Date�G2013-08-09
/// Description�G
///     �ĤH�������
///     0809�s�W�G�s�W����B�Ǫ�BoneAnimation�޲z�t�ΡA�H����Animation
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager script;

    public int CurrentMorale;
    public int MoraleRestoreRate;
    public int MaxMorale;

    public Dictionary<SmoothMoves.BoneAnimation, bool> AllBoneAnimationList = new Dictionary<SmoothMoves.BoneAnimation, bool>();

    /// <summary>
    /// ���UBoneAnimation(����B�ĤH�������U�A��K�t�α���)
    /// </summary>
    /// <param name="boneAnimation">����/�ĤH��BoneAnimation</param>
    public void RegisterBoneAnimation(SmoothMoves.BoneAnimation boneAnimation)
    {
        this.AllBoneAnimationList.Add(boneAnimation, true);
    }

    /// <summary>
    /// �o��ثeBoneAnimation�����A(True�����b�ϥΡBFalse���Ȱ��ϥ�)
    /// </summary>
    /// <param name="boneAnimation">����/�ĤH��BoneAnimation</param>
    /// <returns>�ثeBoneAnimation�����A</returns>
    public bool GetBoneAnimationState(SmoothMoves.BoneAnimation boneAnimation)
    {
        return this.AllBoneAnimationList[boneAnimation];
    }

    /// <summary>
    /// �Ȱ��Ҧ����U��BoneAnimation
    /// </summary>
    public void StopAllBoneAnimation()
    {
        lock (this.AllBoneAnimationList)
        {
            //�qAllBoneAnimation �ƻs��local arrays
            SmoothMoves.BoneAnimation[] allAnimations = new SmoothMoves.BoneAnimation[this.AllBoneAnimationList.Keys.Count];
            this.AllBoneAnimationList.Keys.CopyTo(allAnimations, 0);

            for (int i = 0; i < allAnimations.Length; i++)
            {
                allAnimations[i].Stop();    //�Ȱ�BoneAnimation���B�@
                this.AllBoneAnimationList[allAnimations[i]] = false;    //�N���A�אּ����
            }
        }
    }

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        this.MaxMorale = GameDefinition.MaxMorale;
        this.CurrentMorale = GameDefinition.MaxMorale;
        this.MoraleRestoreRate = GameDefinition.MoraleRestoreRate;

        InvokeRepeating("RestoreMoralePersecond", 0.1f, 1);
    }

    /// <summary>
    /// ���o��e�h��ȡA�w�ഫ��0~100
    /// </summary>
    /// <returns></returns>
    public float GetCurrentMorale()
    {
        return ((float)this.CurrentMorale / this.MaxMorale) * 100;
    }

    // Update is called once per frame
    void Update()
    {
        //���Ѥ����ʱ��ƭ�
        //�h��
        MUI_Monitor.script.SetValue("�h���" + "x", ((float)this.CurrentMorale / this.MaxMorale) * 100);

        //���եΡA�Ȱ��Ҧ����U��BoneAnimation
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameManager.script.StopAllBoneAnimation();
        }
    }

    /// <summary>
    /// �C��T�w�^�_�h���
    /// </summary>
    void RestoreMoralePersecond()
    {
        this.CurrentMorale += this.MoraleRestoreRate;
        if (this.CurrentMorale >= this.MaxMorale)
            this.CurrentMorale = this.MaxMorale;
    }
}
